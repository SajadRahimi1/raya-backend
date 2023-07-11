public interface IMessageRepository
{
    Task<CustomActionResult> SendMessage(Message message,IFormFile? formFile);
    Task<CustomActionResult> GetAllMessages(Guid userId);
}