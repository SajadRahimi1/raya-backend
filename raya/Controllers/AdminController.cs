using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AdminController
{
    private readonly IAdminRepository _adminRepository;
    private readonly IMapper _mapper;

    public AdminController(IAdminRepository adminRepository, IMapper mapper)
    {
        _adminRepository = adminRepository;
        _mapper = mapper;
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
}