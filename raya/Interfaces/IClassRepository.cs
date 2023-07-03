public interface IClassRepository
{
    Task CreateClass(Class NewClass);
    Task<Class> GetSingleClass(string id);
    
}