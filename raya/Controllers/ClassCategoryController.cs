using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Consumes("application/x-www-form-urlencoded", "applicaton/json")]
public class ClassCategoryController : ControllerBase
{
    private readonly IClassCategoryRepository _classCategoryRepository;
    private readonly IMapper _mapper;

    public ClassCategoryController(IClassCategoryRepository classCategoryRepository, IMapper mapper)
    {
        _classCategoryRepository = classCategoryRepository;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("create")]

    public async Task<IActionResult> CreateClassCategory(ClassCategoryDto classCategoryDto)
    {
        var newClassCategory = _mapper.Map<ClassCategory>(classCategoryDto);

        await _classCategoryRepository.CreateClassCategory(newClassCategory);

        return Ok(newClassCategory);
    }

    [HttpGet]
    public async Task<CustomActionResult> GetClassCategories([Required(ErrorMessage = "کلاس را باید وارد کنید")] string classId)
    {
        return await _classCategoryRepository.GetClassCategory(classId);
    }

}