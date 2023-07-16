using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Courseproject.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _config;

    private readonly IDistributedCache _cache;
    private readonly IFileRepository _fileRepository;

    public UserRepository(AppDbContext appDbContext, IConfiguration config, IDistributedCache cache, IFileRepository fileRepository)
    {
        _appDbContext = appDbContext;
        _config = config;
        _cache = cache;
        _fileRepository = fileRepository;
    }

    public async Task CreateNewUser(User user)
    {
        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        List<User>? users;
        users = await _cache.GetRecordAsync<List<User>?>("Users");
        if (users == null)
        {
            users = await _appDbContext.Users.ToListAsync();
            await _cache.SetRecordAsync("Users", users);
        }
        return users;

    }

    public async Task<User?> getSingleUser(string phoneNumber)
    {
        return await _appDbContext.Users.SingleOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
    }

    public async Task<User?> getSingleUserById(string id)
    {
        return await _appDbContext.Users.SingleOrDefaultAsync(_ => _.Id.ToString() == id);
    }

    public async Task UpdateUser(User updatedUser)
    {
        _appDbContext.ChangeTracker.Clear();
        _appDbContext.Users.Update(updatedUser);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<CustomActionResult> CheckSms(string phoneNumber, string code)
    {
        var user = await getSingleUser(phoneNumber);
        if (user == null)
        {
            return new CustomActionResult(new Result
            {
                ErrorMessage = new ErrorModel { ErrorMessage = "کاربری با این شماره یافت نشد" },
                statusCodes = StatusCodes.Status400BadRequest
            });
        }
        if (user.code == code)
        {
            user.Token = Guid.NewGuid();
            user.code = null;
            await UpdateUser(user);
            return new CustomActionResult(new Result
            {
                Data = user,
                // Token = generateToken()
            });
        }
        return new CustomActionResult(new Result
        {
            ErrorMessage = new ErrorModel { ErrorMessage = "کد وارد شده اشتباه است" },
            statusCodes = StatusCodes.Status400BadRequest
        });
    }

    public async Task<CustomActionResult> GetUserClasses(string id)
    {
        var classes = await _appDbContext.Users.Include(_ => _.ReservedClasses).SingleOrDefaultAsync(_ => _.Id.ToString() == id);
        return new CustomActionResult(new Result { Data = classes });
    }

    public async Task<CustomActionResult> GetUserNurseReserved(string id)
    {
        var reserved = await _appDbContext.ReserveNurses.Include(_ => _.Nurse).Where(_ => _.UserId.ToString() == id).ToListAsync();
        return new CustomActionResult(new Result { Data = reserved });
    }

    public async Task<User?> FindUserByToken(string token)
    {
        return await _appDbContext.Users.SingleOrDefaultAsync(_ => _.Token.ToString() == token);
    }

    public async Task<CustomActionResult> CheckAndUpdateUser(User user)
    {
        // var findedUser = await _appDbContext.Users.AsNoTracking().SingleOrDefaultAsync(_ => _.Id == user.Id);
        // if (findedUser == null)
        // {
        //     return new CustomActionResult(new Result { ErrorMessage = new ErrorModel { ErrorMessage = "کاربر یافت نشد" }, statusCodes = StatusCodes.Status404NotFound });
        // }

        _appDbContext.ChangeTracker.Clear();
        _appDbContext.Users.Update(user);
        await _appDbContext.SaveChangesAsync();
        return new CustomActionResult(new Result { Data = user });

    }

    public async Task<CustomActionResult> UpdateUserImage(User user, IFormFile image)
    {
        var imageUrl = await _fileRepository.SaveFileAsync(image);
        user.ImageUrl = imageUrl;
        await UpdateUser(user);
        return new CustomActionResult(new Result
        {
            Data = user
        });
    }

    public async Task<CustomActionResult> GetUserReserved(string id)
    {
        var classes = await _appDbContext.Users.Include(_ => _.ReservedClasses).Include(_ => _.ReserveNurses).SingleOrDefaultAsync(_ => _.Id.ToString() == id);
        await _appDbContext.ReserveNurses.Include(_ => _.Nurse).Where(_ => _.UserId.ToString() == id).ToListAsync();
        await _appDbContext.ReserveClasses.Include(_ => _.ClassCategory).Where(_ => _.UserId.ToString() == id).ToListAsync();
        return new CustomActionResult(new Result { Data = classes });
    }

    // private string generateToken()
    // {
    //     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(  _config["jwt:Key"] ?? "jwtsecretkey"));
    //     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

    //     var token = new JwtSecurityToken(_config["jwt:Issuer"], _config["jwt:Audience"], null, expires: DateTime.Now.AddYears(1), signingCredentials: credentials);

    //     return new JwtSecurityTokenHandler().WriteToken(token);
    // }
}