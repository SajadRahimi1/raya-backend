using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController:ControllerBase{
    private readonly IUserRepository _iuserRepository;
    public UserController(IUserRepository iuserRepository)
    {
        _iuserRepository=iuserRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(){
        var user  = await _iuserRepository.GetAllAsync();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewUser(User newUser){
        await _iuserRepository.CreateNewUser(newUser);
        return  CreatedAtAction(nameof(Get),newUser);
    }

}