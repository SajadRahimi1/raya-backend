using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class NurseController:ControllerBase{
    private readonly INurseRepository _nurseRepository;

    public NurseController(INurseRepository nurseRepository)
    {
        _nurseRepository=nurseRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNurse(){
        return Ok(await _nurseRepository.GetAllNurse());
    }
}