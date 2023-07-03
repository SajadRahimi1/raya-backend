
public interface IClassRepository
{
    Task<List<Class>> GetAllClasses();
    Task<Class> CreateClass(Class NewClass);
    Task<Class?> GetSingleClass(string id);
    
}