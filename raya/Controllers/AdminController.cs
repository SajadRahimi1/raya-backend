using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AdminController
{
    private readonly IAdminRepository _adminRepository;
    private readonly IZarinpalRepository zarinpalRepository;
    private readonly IMapper _mapper;

    public AdminController(IAdminRepository adminRepository, IMapper mapper, IZarinpalRepository zarinpalRepository)
    {
        _adminRepository = adminRepository;
        _mapper = mapper;
        this.zarinpalRepository = zarinpalRepository;
    }

    [HttpPost, Route("add")]
    [Authorize(AuthenticationSchemes = "AdminAuthentication")]
    public async Task<IActionResult> AddAdmin(AdminDto adminDto)
    {
        var admin = _mapper.Map<Admin>(adminDto);
        return await _adminRepository.addAdmin(admin);
    }

    [HttpGet, Route("all")]
    [Authorize(AuthenticationSchemes = "AdminAuthentication")]
    public async Task<IActionResult> getAllAdmin() => await _adminRepository.getAllAdmin();


    [HttpPost, Route("check-code")]
    public async Task<IActionResult> checkCode(string phoneNumber, string code) => await _adminRepository.checkCode(phoneNumber, code);

    [HttpPost, Route("send-code")]
    public async Task<IActionResult> sendCode(string phoneNumber) => await _adminRepository.sendCode(phoneNumber);

    [HttpGet, Route("requests-nurse")]
    public async Task<IActionResult> getReqestedNurse([FromQuery] int page) => await _adminRepository.getRequestedNurse(page);

    [HttpGet, Route("request-detail")]
    public async Task<IActionResult> getReqestedDetail([FromQuery] string id) => await _adminRepository.getRequestDetail(id);

    [HttpDelete, Route("delete-request")]
    public async Task<IActionResult> deleteReqest([FromQuery] string id) => await _adminRepository.deleteRequest(id);

    [HttpPost, Route("message/user")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> SendMessage([FromForm] AdminMessageDto MessageDto)
    {
        var message = _mapper.Map<Message>(MessageDto);
        message.IsUserSend = false;
        return await _adminRepository.sendMessage(message, MessageDto.File);
    }
    [HttpGet, Route("message/all")]
    public async Task<IActionResult> getAllMessage(int page) => await _adminRepository.getAllMessages(page);

    [HttpGet, Route("message/get")]
    public async Task<IActionResult> getMessages(string userId, int page) => await _adminRepository.getMessages(userId, page);

    [HttpGet, Route("class/reserved")]
    public async Task<IActionResult> getReservedClass(string classCategoryId, int page) => await _adminRepository.getReservedClass(classCategoryId, page);

    [HttpPost, Route("class/check-payment")]
    public async Task<IActionResult> checkClassPayment(string classCategoryId) => await zarinpalRepository.checkClassPayementApi(classCategoryId);

    [HttpPut, Route("nurse/uploads")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> NurseUploads([FromForm] AdminNurseDto nurseUploadsDto)
    {
        return await _adminRepository.NurseUpdateUploads(nurseUploadsDto);
    }

}