public interface IClassCategoryRepository
{
    Task CreateClassCategory(ClassCategory classCategory);
    Task UpdateClassCategory(ClassCategory classCategory);
    Task<List<ClassCategory>> GetClassCategory(string classId);
    
}