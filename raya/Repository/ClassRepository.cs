using Microsoft.EntityFrameworkCore;

public class ClassRepository : IClassRepository
{
    private readonly AppDbContext _appDbContext;
    public ClassRepository(AppDbContext appDbContext)
    {
        _appDbContext=appDbContext;;
    }

    public async Task<Class> CreateClass(Class NewClass)
    {
         await _appDbContext.Classes.AddAsync(NewClass);
        await _appDbContext.SaveChangesAsync();
        return NewClass;
    }

    public async Task<List<Class>> GetAllClasses()
    {
        return await _appDbContext.Classes.ToListAsync();
    }

    public async Task<Class?> GetSingleClass(string id)
    {
       return await _appDbContext.Classes.SingleOrDefaultAsync(_=>_.Id.ToString()==id);
    }
}