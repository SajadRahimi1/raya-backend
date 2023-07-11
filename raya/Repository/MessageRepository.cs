using Courseproject.Common.Interfaces;
using Courseproject.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _appContext;
    private readonly IFileRepository _fileRepository;
    public MessageRepository(AppDbContext appContext, IFileRepository fileRepository)
    {
        _appContext = appContext;
        _fileRepository = fileRepository;
    }
    public async Task<CustomActionResult> GetAllMessages(Guid userId)
    {
        var user = await _appContext.Users.Include(_ => _.Messages).SingleOrDefaultAsync(_ => _.Id == userId);
        return new CustomActionResult(new Result { Data = user.Messages });
    }

    public async Task<CustomActionResult> SendMessage(Message message, IFormFile? file)
    {
        if (file != null)
        {
            var fileName = await _fileRepository.SaveFileAsync(file);
            message.Content = fileName;
        }

        await _appContext.AddAsync(message);
        await _appContext.SaveChangesAsync();
        return new CustomActionResult(new Result { Data = message });

    }
}