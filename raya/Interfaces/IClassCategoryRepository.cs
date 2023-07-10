public interface IClassCategoryRepository
{
    Task CreateClassCategory(ClassCategory classCategory);
    Task UpdateClassCategory(ClassCategory classCategory);
    Task<CustomActionResult> GetClassCategory(string classId);
    Task<CustomActionResult> ReserveClass(User? user,string classCategoryId);
    Task<CustomActionResult> GetClassCategoryDetail(string classCategoryId);
    
}