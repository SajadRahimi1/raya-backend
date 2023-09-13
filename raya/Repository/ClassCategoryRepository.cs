using Microsoft.EntityFrameworkCore;

public class ClassCategoryRepository : IClassCategoryRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IZarinpalRepository zarinpalRepository;

    public ClassCategoryRepository(AppDbContext appDbContext, IZarinpalRepository zarinpalRepository)
    {
        _appDbContext = appDbContext;
        this.zarinpalRepository = zarinpalRepository;
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

    public async Task<CustomActionResult> GetClassCategoryDetail(string classCategoryId)
    {
        var classCategory = await _appDbContext.ClassCategories.Include(_ => _.ReserveClasses).SingleOrDefaultAsync(_ => _.Id.ToString() == classCategoryId);
        return new CustomActionResult(new Result
        {
            Data = classCategory
        });
    }

    public async Task<CustomActionResult> ReserveClass(ReserveClass reserveClass)
    {
        var classCategory = await _appDbContext.ClassCategories.SingleOrDefaultAsync(_ => _.Id.ToString() == reserveClass.ClassCategoryId.ToString());
        if (classCategory == null)
        {
            return new CustomActionResult(new Result
            {
                ErrorMessage = new ErrorModel { ErrorMessage = "کلاسی یافت نشد" },
                statusCodes = StatusCodes.Status400BadRequest
            });
        }

        return await zarinpalRepository.payCourse(reserveClass);
    }

    public async Task UpdateClassCategory(ClassCategory classCategory)
    {
        _appDbContext.ClassCategories.Update(classCategory);
        await _appDbContext.SaveChangesAsync();

    }
}