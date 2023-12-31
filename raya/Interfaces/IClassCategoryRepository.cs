
public interface IClassCategoryRepository
{
    Task CreateClassCategory(ClassCategory classCategory);
    Task<CustomActionResult> UpdateClassCategory(ClassCategory classCategory);
    Task<CustomActionResult> GetClassCategory(string classId);
    Task<CustomActionResult> ReserveClass(ReserveClass reserveClass);
    Task<CustomActionResult> GetClassCategoryDetail(string classCategoryId);
    Task<CustomActionResult> UpdateRoute(string id, string route);

}

