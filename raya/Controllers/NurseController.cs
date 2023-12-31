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
    private readonly IZarinpalRepository zarinpalRepository;
    private readonly IMapper _mapper;

    public NurseController(INurseRepository nurseRepository, IMapper mapper, IKavehnegarRespository kavehnegarRespository, IZarinpalRepository zarinpalRepository)
    {
        _nurseRepository = nurseRepository;
        _mapper = mapper;
        this.kavehnegarRespository = kavehnegarRespository;
        this.zarinpalRepository = zarinpalRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNurse(int? page)
    {
        return Ok(await _nurseRepository.GetAllNurse(page: page ?? 1));
    }

    [HttpPost]
    [Route("reserve")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public async Task<IActionResult> ReserveNurse(ReserveNurseDto reserveNurseDto)
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        reserveNurseDto.Ages.RemoveAll(age => age is null);
        var reserveNurse = _mapper.Map<ReserveNurse>(reserveNurseDto);
        return await _nurseRepository.ReserveNurse(reserveNurse, user);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public async Task<IActionResult> CreateNurse(CreateNurseDto createNurseDto)
    {
        var nurse = _mapper.Map<Nurse>(createNurseDto);
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        nurse.userId = user.Id;
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
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
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

    [HttpPost, Route("pdf")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> NursePdf([FromForm] NursePdfDto nursePdfDto)
    {
        return await _nurseRepository.uploadNursePdf(nursePdfDto.nurseId, nursePdfDto.pdf);
    }

    [HttpGet, Route("payment")]
    public async Task<IActionResult> payment([FromQuery] string id)
    {
        return await zarinpalRepository.payHiringNurse(id);
    }

    [HttpGet, Route("verify")]
    public async Task<IActionResult> verifyPayment([FromQuery] string Authority, [FromQuery] string id)
    {
        return await zarinpalRepository.checkPayement(Authority, id);
    }

    [HttpGet, Route("single")]
    public async Task<IActionResult> getSingle(string id, string password)
    {
        if (password == "rayanikrayaniyaresh17")
        {
            return await _nurseRepository.getSingleNurse(id);

        }
        return Unauthorized();
    }

    [HttpPost, Route("check")]
    public async Task<IActionResult> checkPayment([FromBody] string id)
    {
        return await zarinpalRepository.checkNursePayement(id);
    }

    [HttpPost, Route("status")]
    public async Task<IActionResult> editNurseStatus([FromBody] NurseStatusDto nurseStatusDto)
    {
        return await _nurseRepository.editNurseStatus(nurseStatusDto.status, nurseStatusDto.nurseId);
    }

    [HttpGet, Route("get-status")]
    public async Task<IActionResult> getStatusNurse([FromQuery] Status status, [FromQuery] int page)
    {
        return await _nurseRepository.GetStatusNurses(page, status);
    }

    [HttpPut, Route("update")]
    public async Task<IActionResult> updateNurse([FromBody] NurseDto nurseDto)
    {
        return await _nurseRepository.UpdateNurse(_mapper.Map<Nurse>(nurseDto));
    }
}
