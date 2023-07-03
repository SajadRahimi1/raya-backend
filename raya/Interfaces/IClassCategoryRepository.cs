public interface IClassCategoryRepository
{
    Task<ClassCategory> CreateClassCategory(ClassCategory classCategory);
    Task<ClassCategory> UpdateClassCategory(ClassCategory classCategory);
}