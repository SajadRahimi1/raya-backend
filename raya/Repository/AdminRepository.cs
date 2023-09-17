using Microsoft.EntityFrameworkCore;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _appDbContext;

    public AdminRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<CustomActionResult> addAdmin(Admin admin)
    {
        var addedAdmin = await _appDbContext.Admins.AddAsync(admin);
        return new CustomActionResult(new Result { Data = addedAdmin.Entity });
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
}