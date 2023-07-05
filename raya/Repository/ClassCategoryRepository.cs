using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

    public async Task<CustomActionResult> GetClassCategory(string classId)
    {
        var classObject = await _appDbContext.Classes.Include(_ => _.ClassCategories).SingleOrDefaultAsync(_ => _.Id.ToString() == classId);
        if (classObject == null)
        {
            return new CustomActionResult(result: new Result()
            {
                ErrorMessage = new ErrorModel() { ErrorMessage = "کلاسی یا این آی دی یافت نشد" },
                statusCodes = StatusCodes.Status404NotFound
            });
        }
        return new CustomActionResult(result: new Result()
        {
            Data = classObject,
        });
    }

    public async Task UpdateClassCategory(ClassCategory classCategory)
    {
        _appDbContext.ClassCategories.Update(classCategory);
        await _appDbContext.SaveChangesAsync();

    }
}