using Microsoft.EntityFrameworkCore;

public class ClassCategoryRepository : IClassCategoryRepository
{
    private readonly AppDbContext _appDbContext;
    public ClassCategoryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task CreateClassCategory(ClassCategory classCategory)
    {
        await _appDbContext.ClassCategories.AddAsync(classCategory);
        await _appDbContext.SaveChangesAsync();

    }

    public async Task<List<ClassCategory>> GetClassCategory(string classId)
    {
        return await _appDbContext.ClassCategories.ToListAsync();
        // return await _appDbContext.ClassCategories.Where(_=>_.ClassId.ToString()==classId).ToListAsync();
    }

    public async Task UpdateClassCategory(ClassCategory classCategory)
    {
        _appDbContext.ClassCategories.Update(classCategory);
        await _appDbContext.SaveChangesAsync();

    }
}