using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;

    public MessageController(IMessageRepository messageRepository, IMapper mapper)
    {
        _mapper = mapper;
        _messageRepository = messageRepository;
    }

    [HttpPost, Route("send")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> SendMessage([FromForm] SendMessageDto sendMessageDto)
    {
        var message = _mapper.Map<Message>(sendMessageDto);
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        message.UserId = user.Id;
        return await _messageRepository.SendMessage(message, sendMessageDto.File);
    }

    [HttpGet,]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public async Task<IActionResult> GetMessages()
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Headers["user"]);
        return await _messageRepository.GetAllMessages(user.Id);
    }
}