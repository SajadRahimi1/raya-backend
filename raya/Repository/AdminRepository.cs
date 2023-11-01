using Courseproject.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IKavehnegarRespository _kavehnegarRespository;
    private readonly IFileRepository _fileRepository;

    public AdminRepository(AppDbContext appDbContext, IKavehnegarRespository kavehnegarRespository, IFileRepository fileRepository)
    {
        _appDbContext = appDbContext;
        _kavehnegarRespository = kavehnegarRespository;
        _fileRepository = fileRepository;
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

    public async Task<CustomActionResult> deleteRequest(string id)
    {
        var request = await _appDbContext.ReserveNurses.Include(_ => _.UserReserved).SingleOrDefaultAsync(_ => _.Id.ToString() == id);
        if (request == null)
        {
            return new CustomActionResult(new Result { ErrorMessage = new ErrorModel { ErrorMessage = "یافت نشد" }, statusCodes = 404 });
        }
        request.isDeleted = true;
        _appDbContext.ReserveNurses.Update(request);
        await _appDbContext.SaveChangesAsync();
        return new CustomActionResult(new Result { Data = "با موفقیت حذف شد" });
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

    public async Task<CustomActionResult> getAllMessages(int page = 1)
    {
        page = page < 1 ? 1 : page;
        var users = await getUserOrderByMessage(page);
        var selectedUser = users.Select(user => new
        {
            LastMessage = user.Messages.OrderByDescending(message => message.CreatedAt)
        }).ToList();
        return new CustomActionResult(new Result { Data = selectedUser });
    }

    private async Task<List<User>> getUserOrderByMessage(int page)
    {
        // users with message
        var users = await _appDbContext.Users.Include(user => user.Messages).Where(user => user.Messages.Any()).ToListAsync();

        // order user with last message
        var orderedUser = users.OrderByDescending(user => user.Messages.Max(message => message.CreatedAt)).ToList();

        var paginationUser = orderedUser.Skip((page - 1) * 15).Take(15).ToList();

        // var selectedUser = paginationUser.Select(user => new
        // {
        //     Id = user.Id,
        //     Name = user.Name,
        //     LastMessage = user.Messages.OrderByDescending(message => message.CreatedAt).FirstOrDefault()
        // }).ToList();

        return paginationUser;

    }

    public async Task<CustomActionResult> getRequestDetail(string id)
    {
        var request = await _appDbContext.ReserveNurses.Include(_ => _.UserReserved).SingleOrDefaultAsync(_ => _.Id.ToString() == id);
        if (request == null)
        {
            return new CustomActionResult(new Result { ErrorMessage = new ErrorModel { ErrorMessage = "یافت نشد" }, statusCodes = 404 });
        }
        return new CustomActionResult(new Result { Data = request });
    }

    public async Task<CustomActionResult> getRequestedNurse(int page = 1)
    {
        page = page < 1 ? 1 : page;
        List<ReserveNurse> reserveNurses = await _appDbContext.ReserveNurses.OrderByDescending(nurse => nurse.UpdatedAt).Where(_ => _.isDeleted == false).Skip((page - 1) * 15).Take(15).ToListAsync();
        return new CustomActionResult(new Result { Data = reserveNurses });

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

    public async Task<CustomActionResult> sendMessage(Message message, IFormFile? file)
    {
        if (file != null)
        {
            var fileName = await _fileRepository.SaveFileAsync(file);
            message.Content = fileName;
        }

        await _appDbContext.AddAsync(message);
        await _appDbContext.SaveChangesAsync();
        return new CustomActionResult(new Result { Data = message });
    }
}