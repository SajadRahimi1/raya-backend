using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _config;

    private readonly IDistributedCache _cache;

    public UserRepository(AppDbContext appDbContext, IConfiguration config, IDistributedCache cache)
    {
        _appDbContext = appDbContext;
        _config = config;
        _cache = cache;
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
        var classes = await _appDbContext.Users.Select(_ => _.ReservedClasses).ToListAsync();
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

    // private string generateToken()
    // {
    //     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(  _config["jwt:Key"] ?? "jwtsecretkey"));
    //     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

    //     var token = new JwtSecurityToken(_config["jwt:Issuer"], _config["jwt:Audience"], null, expires: DateTime.Now.AddYears(1), signingCredentials: credentials);

    //     return new JwtSecurityTokenHandler().WriteToken(token);
    // }
}