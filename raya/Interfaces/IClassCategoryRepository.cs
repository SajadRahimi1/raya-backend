public interface IClassCategoryRepository
{
    Task CreateClassCategory(ClassCategory classCategory);
    Task UpdateClassCategory(ClassCategory classCategory);
    Task<CustomActionResult> GetClassCategory(string classId);
    Task<CustomActionResult> ReserveClass(string userId,string classCategoryId);
    Task<CustomActionResult> GetClassCategoryDetail(string classCategoryId);
    
}