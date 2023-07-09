using Courseproject.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

public class ClassRepository : IClassRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ImageFileValidator _imageFileValidator;
    private readonly IFileRepository _fileRepository;


    public ClassRepository(AppDbContext appDbContext, ImageFileValidator imageFileValidator, IFileRepository fileRepository)
    {
        _appDbContext = appDbContext;
        _imageFileValidator = imageFileValidator;
        _fileRepository = fileRepository;
    }

    public async Task<Class> CreateClass(CreateClassDto createClassDto)
    {
        await _imageFileValidator.ValidateAndThrowAsync(createClassDto.image);
        var fileName = await _fileRepository.SaveFileAsync(createClassDto.image);
        Class newClass = new Class { Title = createClassDto.title, ImageName = fileName };
        await _appDbContext.Classes.AddAsync(newClass);
        await _appDbContext.SaveChangesAsync();
        return newClass;
    }

    public async Task<List<Class>> GetAllClasses()
    {
        return await _appDbContext.Classes.Include(_ => _.ClassCategories).ToListAsync();
    }

    public async Task<Class?> GetSingleClass(string id)
    {
        return await _appDbContext.Classes.SingleOrDefaultAsync(_ => _.Id.ToString() == id);
    }
}