using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("[controller]")]
public class NurseController : ControllerBase
{
    private readonly INurseRepository _nurseRepository;
    private readonly IKavehnegarRespository kavehnegarRespository;
    private readonly IMapper _mapper;

    public NurseController(INurseRepository nurseRepository, IMapper mapper, IKavehnegarRespository kavehnegarRespository)
    {
        _nurseRepository = nurseRepository;
        _mapper = mapper;
        this.kavehnegarRespository = kavehnegarRespository;
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
        user.Name = reserveNurseDto.name;
        user.PhoneNumber=reserveNurseDto.phoneNumber;
        var reserveNurse = _mapper.Map<ReserveNurse>(reserveNurseDto);
        return await _nurseRepository.ReserveNurse(reserveNurse, user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNurse(CreateNurseDto createNurseDto)
    {
        var nurse = _mapper.Map<Nurse>(createNurseDto);
        return await _nurseRepository.CreateNurse(nurse);
    }

    [HttpGet, Route("hiring-sms")]
    public async Task<IActionResult> HiringSms([FromQuery] string phoneNumber, [FromQuery] string name)
    {
        // return await kavehnegarRespository.sendHiringNurseSms(phoneNumber, name);
        return Ok();
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
        return _nurseRepository.GetUsersReserved(nurseId);
    }

    [HttpPut, Route("uploads")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> NurseUploads([FromForm] NurseUploadsDto nurseUploadsDto)
    {
        return await _nurseRepository.NurseUpdateUploads(nurseUploadsDto);
    }

    [HttpPut, Route("family")]
    public async Task<IActionResult> NurseFamily(UpdateNurseFamilyDto dto)
    {
        return await _nurseRepository.UpdateNurseFamily(dto);
    }
}