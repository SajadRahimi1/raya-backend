using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

public class UserRepository:IUserRepository{
 private readonly AppDbContext _appDbContext;

 public UserRepository(AppDbContext appDbContext)
 {
  _appDbContext=appDbContext;
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
        return await _appDbContext.Users.SingleOrDefaultAsync(user=>user.PhoneNumber==phoneNumber);
    }

    public async Task UpdateUser(User updatedUser)
    {
       _appDbContext.Users.Update(updatedUser  );
       await _appDbContext.SaveChangesAsync();
    }
}