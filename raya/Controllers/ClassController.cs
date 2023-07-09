using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ClassController : ControllerBase
{

    private readonly IClassRepository _classRepository;

    public ClassController(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    [HttpGet]
    // [Consumes("application/json")]
    public async Task<IActionResult> GetAllClasses()
    {
        return Ok(await _classRepository.GetAllClasses());
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> InsertClass([FromForm] CreateClassDto createClassDto)
    {

        return Ok(await _classRepository.CreateClass(createClassDto));
    }
}