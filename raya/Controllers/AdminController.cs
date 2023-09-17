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
    [Authorize(AuthenticationSchemes = "admin")]
    public async Task<IActionResult> AddAdmin(AdminDto adminDto)
    {
        var admin = _mapper.Map<Admin>(adminDto);
        return await _adminRepository.addAdmin(admin);
    }

    [HttpGet, Route("all")]
    [Authorize(AuthenticationSchemes = "admin")]
    public async Task<IActionResult> getAllAdmin()
    {
        return await _adminRepository.getAllAdmin();
    }
}