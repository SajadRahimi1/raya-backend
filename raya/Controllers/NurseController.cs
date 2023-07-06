using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> ReserveNurse(ReserveNurseDto reserveNurseDto)
    {
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
    [Route("reserved")]
    public async Task<IActionResult> GetNurseReserved(string nurseId)
    {
        return await _nurseRepository.GetNurseReserved(nurseId);
    }
}