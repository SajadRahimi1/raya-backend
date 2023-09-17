using Microsoft.EntityFrameworkCore;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _appDbContext;

    public AdminRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Admin?> getAdminByToken(string token)
    {
        return await _appDbContext.Admins.SingleOrDefaultAsync(_ => _.token == token);

    }
}