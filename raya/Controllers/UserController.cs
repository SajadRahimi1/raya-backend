using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Consumes("application/x-www-form-urlencoded", "applicaton/json")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {

        _userRepository = userRepository;

    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet]
    [Route("/send-sms/{phoneNumber}")]
    public async Task<IActionResult> SendSms(String phoneNumber)
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
    public async Task<IActionResult> checkSms(String phoneNumber, string code)
    {

        return await _userRepository.CheckSms(phoneNumber, code);
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
        return await _userRepository.GetUserNurseReserved(id);
    }

}