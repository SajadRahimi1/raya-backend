using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _config;


    public UserRepository(AppDbContext appDbContext, IConfiguration config)
    {
        _appDbContext = appDbContext;
        _config = config;
    }

    public async Task CreateNewUser(User user)
    {
        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _appDbContext.Users.ToListAsync();

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

    // private string generateToken()
    // {
    //     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(  _config["jwt:Key"] ?? "jwtsecretkey"));
    //     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

    //     var token = new JwtSecurityToken(_config["jwt:Issuer"], _config["jwt:Audience"], null, expires: DateTime.Now.AddYears(1), signingCredentials: credentials);

    //     return new JwtSecurityTokenHandler().WriteToken(token);
    // }
}