using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ClassCategoryController : ControllerBase
{
    private readonly IClassCategoryRepository _classCategoryRepository;

    public ClassCategoryController(IClassCategoryRepository classCategoryRepository)
    {
        _classCategoryRepository = classCategoryRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClassCategory(ClassCategory classCategory)
    {
        await _classCategoryRepository.CreateClassCategory(classCategory);
        
        return Ok(classCategory);
    }

    [HttpGet]
    public async Task<IActionResult> GetClassCategories(string classId)
    {
        return Ok(await _classCategoryRepository.GetClassCategory(classId));
    }
}