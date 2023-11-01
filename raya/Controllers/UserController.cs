using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IKavehnegarRespository _kavehnegarRespository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IKavehnegarRespository kavehnegarRespository, IMapper mapper)
    {

        _userRepository = userRepository;
        _kavehnegarRespository = kavehnegarRespository;
        _mapper = mapper;
    }

    [HttpGet("/pay")]
    public IActionResult redirectToApp(String? Status, String? Authority)
    {
        return Redirect(string.Format("asia://salamat?Status={0}", Status));
    }

    // [HttpGet, Route("/id")]
    // [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    // public async Task<IActionResult> GetById()
    // {
    //     var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
    //     var users = await _userRepository.getSingleUserById(user?.Id.ToString() ?? "");
    //     return Ok(users);
    // }

    [HttpPost, Route("update-image")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateUserImage([FromForm] UpdateUserImageDto userImageDto)
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        return await _userRepository.UpdateUserImage(user, userImageDto.image);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public async Task<IActionResult> GetByToken()
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        var users = await _userRepository.getSingleUserById(user?.Id.ToString() ?? "");
        return Ok(users);
    }

    [HttpGet, Route("all")]

    public async Task<IActionResult> GetAsync([DataType(DataType.Password)] string password)
    {
        if (password == "rayanikrayaniyaresh17")
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }
        return Unauthorized();
    }

    [HttpGet]
    [Route("/send-sms")]
    [Consumes("application/json")]
    public async Task<IActionResult> SendSms([FromQuery] String phoneNumber)
    {
        Random random = new Random();
        int randomNumber = random.Next(1000, 10000);
        var user = await _userRepository.getSingleUser(phoneNumber);
        using var client = new HttpClient();
        if (user == null)
        {
            await _userRepository.CreateNewUser(new User { PhoneNumber = phoneNumber, code = randomNumber.ToString() });
            return await _kavehnegarRespository.sendLoginSms(phoneNumber, randomNumber.ToString());
        }
        else
        {
            user.code = randomNumber.ToString();
            await _userRepository.UpdateUser(user);
            // using HttpResponseMessage response = await client.GetAsync(string.Format("https://api.kavenegar.com/v1/76526D486C52682F413330784E3575344E664B58714B5261593175776A5A56564C7A576D4A3168314C78633D/verify/lookup.json?receptor={0}&token={1}&template=verify", phoneNumber, randomNumber.ToString()));
            // if (response.IsSuccessStatusCode)
            // {
            // return Ok("کد با موفقیت ارسال شد");
            return await _kavehnegarRespository.sendLoginSms(phoneNumber, randomNumber.ToString());
            // }
            // else
            // {
            //     return NotFound(await response.Content.ReadAsStringAsync());
            // }
        }
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
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Route("get-reserved")]
    public async Task<IActionResult> GetUserReserved()
    {

        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        return await _userRepository.GetUserReserved(user?.Id.ToString());
    }

    [HttpGet]
    [Route("get-nurse")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public async Task<IActionResult> GetUserNurse(string id)
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        return await _userRepository.GetUserNurseReserved(user?.Id.ToString() ?? id);
    }

    [HttpPut]
    [Route("update")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
    {
        var userToken = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        var user = new User
        {
            Address = updateUserDto.Address,
            Birthday = updateUserDto.Birthday,
            BornCity = updateUserDto.BornCity,
            code = null,
            Education = updateUserDto.Education,
            EmergancyNumber = updateUserDto.EmergancyNumber,
            FatherName = updateUserDto.FatherName,
            Id = userToken?.Id ?? Guid.NewGuid(),
            Name = updateUserDto.Name,
            NationalCode = updateUserDto.NationalCode,
            NationalNumber = updateUserDto.NationalNumber,
            PhoneNumber = updateUserDto.PhoneNumber,
            Token = userToken?.Token ?? Guid.NewGuid(),
            ReserveNurses = userToken?.ReserveNurses ?? new List<ReserveNurse>(),
            ReservedClasses = userToken?.ReservedClasses ?? new List<ReserveClass>(),
        };
        return await _userRepository.CheckAndUpdateUser(user);
    }


    [HttpGet, Route("nurse"), Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public async Task<IActionResult> getNurses()
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        return await _userRepository.getNurses(user.PhoneNumber);
    }

    [HttpGet, Route("single-nurse"), Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public async Task<IActionResult> getNurses([FromQuery] String nurseId)
    {
        return await _userRepository.getSingleNurse(nurseId);
    }

    [HttpPost, Route("edit-nurse"), Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public async Task<IActionResult> updateNurse([FromBody] NurseDto nurseDto)
    {
        var nurse = _mapper.Map<Nurse>(nurseDto);
        nurse.NurseFamily = _mapper.Map<List<NurseFamily>>(nurseDto.NurseFamily);
        return await _userRepository.UpdateNurse(nurse);
    }
}