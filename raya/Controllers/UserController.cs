using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {

        _userRepository = userRepository;

    }

    [HttpGet, Route("/id")]
    [Authorize]
    public async Task<IActionResult> GetById()
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        var users = await _userRepository.getSingleUserById(user?.Id.ToString() ?? "");
        return Ok(users);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet]
    [Route("/send-sms")]
    [Consumes("application/json")]
    public async Task<IActionResult> SendSms([FromQuery] String phoneNumber)
    {
        Random random = new Random();
        int randomNumber = random.Next(1000, 10000);
        var user = await _userRepository.getSingleUser(phoneNumber);
        if (user == null)
        {
            await _userRepository.CreateNewUser(new User { PhoneNumber = phoneNumber, code = randomNumber.ToString() });
        }
        else
        {
            user.code = randomNumber.ToString();
            await _userRepository.UpdateUser(user);
        }
        return Ok("کد با موفقیت ارسال شد");
    }

    [HttpPost]
    [Route("/check-sms")]
    public async Task<IActionResult> checkSms([FromBody] CheckSmsDto dto)
    {

        return await _userRepository.CheckSms(dto.phoneNumber, dto.code);
    }

    [HttpGet]
    [Route("get-class")]
    public async Task<IActionResult> GetUserClasses(string id)
    {
        return await _userRepository.GetUserClasses(id);
    }

    [HttpGet]
    [Route("get-reserved")]
    public async Task<IActionResult> GetUserReserved(string id)
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        return await _userRepository.GetUserNurseReserved(user?.Id.ToString() ?? id);
    }

}