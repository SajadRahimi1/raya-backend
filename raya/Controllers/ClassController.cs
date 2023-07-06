using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Consumes("application/x-www-form-urlencoded", "applicaton/json")]
public class ClassController : ControllerBase
{

    private readonly IClassRepository _classRepository;

    public ClassController(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClasses()
    {
        return Ok(await _classRepository.GetAllClasses());
    }

    [HttpPost]
    public async Task<IActionResult> InsertClass(string title)
    {
        return Ok(await _classRepository.CreateClass(new Class { Title = title }));
    }
}