using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

    [HttpPost]
    [Route("Reserve")]
    [Authorize]
    public async Task<IActionResult> ReserveClassCategory( string classCategoryId)
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        return await _classCategoryRepository.ReserveClass(user?.Id.ToString()??"", classCategoryId);
    }

    [HttpGet]
    [Route("Detail")]
    public async Task<CustomActionResult> GetClassCategoryDetail([Required(ErrorMessage = "کلاس را باید وارد کنید")] string classCategoryId)
    {
        return await _classCategoryRepository.  GetClassCategoryDetail(classCategoryId);
    }

}