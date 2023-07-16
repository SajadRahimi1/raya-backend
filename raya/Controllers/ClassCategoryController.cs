using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using raya_back.Migrations;

[ApiController]
[Route("[controller]")]
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
        string days = "";
        if (classCategoryDto.EvenDay) days += "روزهای زوج";
        if (classCategoryDto.OddDay)
        {
            if (classCategoryDto.EvenDay) days += ",";
            days += "روزهای فرد";
        }
        var newClassCategory = _mapper.Map<ClassCategory>(classCategoryDto);
        newClassCategory.Days = days;

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
    public async Task<IActionResult> ReserveClassCategory(ReserveClassDto reserveClassDto)
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        reserveClassDto.UserId = user.Id;
        var reserveClass = _mapper.Map<ReserveClass>(reserveClassDto);
        return await _classCategoryRepository.ReserveClass(reserveClass);
    }

    [HttpGet]
    [Route("Detail")]
    public async Task<CustomActionResult> GetClassCategoryDetail([Required(ErrorMessage = "کلاس را باید وارد کنید")] string classCategoryId)
    {
        return await _classCategoryRepository.GetClassCategoryDetail(classCategoryId);
    }

}