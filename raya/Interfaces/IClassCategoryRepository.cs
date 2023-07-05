public interface IClassCategoryRepository
{
    Task CreateClassCategory(ClassCategory classCategory);
    Task UpdateClassCategory(ClassCategory classCategory);
    Task<CustomActionResult> GetClassCategory(string classId);
    
}