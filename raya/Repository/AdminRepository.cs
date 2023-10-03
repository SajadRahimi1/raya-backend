using Microsoft.EntityFrameworkCore;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IKavehnegarRespository _kavehnegarRespository;

    public AdminRepository(AppDbContext appDbContext, IKavehnegarRespository kavehnegarRespository)
    {
        _appDbContext = appDbContext;
        _kavehnegarRespository = kavehnegarRespository;
    }

    public async Task<CustomActionResult> addAdmin(Admin admin)
    {
        var addedAdmin = await _appDbContext.Admins.AddAsync(admin);
        await _appDbContext.SaveChangesAsync();
        return new CustomActionResult(new Result { Data = addedAdmin.Entity });
    }


    public async Task<CustomActionResult> checkCode(string phoneNumber, string code)
    {
        var admin = await _appDbContext.Admins.SingleOrDefaultAsync(_ => _.phoneNumber == phoneNumber);
        if (admin == null)
        {
            return new CustomActionResult(new Result
            {
                ErrorMessage = new ErrorModel { ErrorMessage = "ادمین با این شماره یافت نشد" },
                statusCodes = StatusCodes.Status400BadRequest
            });
        }
        if (admin.smsCode != code)
        {
            return new CustomActionResult(new Result
            {
                ErrorMessage = new ErrorModel { ErrorMessage = "کد وارد شده اشتباه است" },
                statusCodes = StatusCodes.Status400BadRequest
            });
        }

        admin.token = Guid.NewGuid().ToString();
        admin.smsCode = null;
        await editAdmin(admin);
        return new CustomActionResult(new Result
        {
            Data = admin,
        });
    }



    public async Task<CustomActionResult> editAdmin(Admin admin)
    {
        var editedAdmin = _appDbContext.Admins.Update(admin);
        await _appDbContext.SaveChangesAsync();
        return new CustomActionResult(new Result { Data = editedAdmin.Entity });

    }

    public async Task<Admin?> getAdminByToken(string token)
    {
        return await _appDbContext.Admins.SingleOrDefaultAsync(_ => _.token == token);

    }

    public async Task<CustomActionResult> getAllAdmin()
    {
        return new CustomActionResult(new Result { Data = await _appDbContext.Admins.ToListAsync() });
    }

    public async Task<CustomActionResult> sendCode(string phoneNumber)
    {
        var admin = await _appDbContext.Admins.SingleOrDefaultAsync(_ => _.phoneNumber == phoneNumber);
        if (admin == null)
        {
            return new CustomActionResult(new Result
            {
                ErrorMessage = new ErrorModel { ErrorMessage = "ادمین با این شماره یافت نشد" },
                statusCodes = StatusCodes.Status400BadRequest
            });
        }
        int randomNumber = new Random().Next(1000, 10000);
        admin.smsCode = randomNumber.ToString();
        await editAdmin(admin);
        return await _kavehnegarRespository.sendLoginSms(phoneNumber, randomNumber.ToString());
    }
}