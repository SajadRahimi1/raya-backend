
public interface IClassRepository
{
    Task<List<Class>> GetAllClasses();
    Task<Class> CreateClass(CreateClassDto createClassDto);
    Task<Class?> GetSingleClass(string id);
    Task<CustomActionResult> GetSingleClassByTitle(string title);
    

}