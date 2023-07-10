using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("[controller]")]
public class NurseController : ControllerBase
{
    private readonly INurseRepository _nurseRepository;
    private readonly IMapper _mapper;

    public NurseController(INurseRepository nurseRepository, IMapper mapper)
    {
        _nurseRepository = nurseRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNurse()
    {
        return Ok(await _nurseRepository.GetAllNurse());
    }

    [HttpPost]
    [Route("reserve")]
    [Authorize]
    public async Task<IActionResult> ReserveNurse(ReserveNurseDto reserveNurseDto)
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        reserveNurseDto.UserId = user?.Id;
        var reserveNurse = _mapper.Map<ReserveNurse>(reserveNurseDto);
        return await _nurseRepository.ReserveNurse(reserveNurse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNurse(CreateNurseDto createNurseDto)
    {
        var nurse = _mapper.Map<Nurse>(createNurseDto);
        return await _nurseRepository.CreateNurse(nurse);
    }

    [HttpGet]
    [Route("nurses-reserved")]
    [Authorize]
    public async Task<IActionResult> GetNurseReserved()
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        return await _nurseRepository.GetNursesReserved(user?.Id.ToString() ?? "");
    }

    [HttpGet]
    [Route("users-reserved")]
    public async Task<IActionResult> GetNurseReserved(string nurseId)
    {
        return await _nurseRepository.GetUsersReserved(nurseId);
    }
}