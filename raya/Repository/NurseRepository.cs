using Courseproject.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

public class NurseRepository : INurseRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IDistributedCache _cache;
    private readonly IFileRepository _fileRepository;
    public NurseRepository(AppDbContext appDbContext, IDistributedCache cache, IFileRepository fileRepository)
    {
        _appDbContext = appDbContext;
        _cache = cache;
        _fileRepository = fileRepository;
    }

    public async Task<CustomActionResult> CreateNurse(Nurse nurse)
    {
        await _appDbContext.Nurses.AddAsync(nurse);
        await _appDbContext.SaveChangesAsync();
        return new CustomActionResult(new Result
        {
            Data = nurse
        });
    }

    public async Task<List<Nurse>> GetAllNurse()
    {
        List<Nurse>? nurses;
        nurses = await _cache.GetRecordAsync<List<Nurse>?>("Nurse");
        if (nurses == null)
        {
            nurses = await _appDbContext.Nurses.ToListAsync();
            await _cache.SetRecordAsync("Nurse", nurses);
        }
        return nurses;
    }

    public async Task<CustomActionResult> GetNursesReserved(string userId)
    {
        var reserved = await _appDbContext.ReserveNurses.Where(_ => _.UserId.ToString() == userId).ToListAsync();
        return new CustomActionResult(new Result { Data = reserved });
    }


    public async Task<CustomActionResult> GetUsersReserved(string nurseId)
    {
        // var reserved = await _appDbContext.ReserveNurses.Include(_ => _.UserReserved).Where(_ => _.NurseId.ToString() == nurseId).ToListAsync();
        return new CustomActionResult(new Result { Data = "reserved" });
    }

    public async Task<CustomActionResult> NurseUpdateUploads(NurseUploadsDto nurseUploadsDto)
    {
        var nurse = await _appDbContext.Nurses.Include(_ => _.NurseImages).SingleOrDefaultAsync(_ => _.Id == nurseUploadsDto.NurseId);

        if (nurse == null)
        {
            return new CustomActionResult(new Result
            {
                ErrorMessage = new ErrorModel { ErrorMessage = "پرستاری یافت نشد" },
                statusCodes = 404
            });
        }
        var picture = await _fileRepository.SaveFileAsync(nurseUploadsDto.Picture);
        var firstPageImage = await _fileRepository.SaveFileAsync(nurseUploadsDto.FirstPageImage);
        var descriptionImage = await _fileRepository.SaveFileAsync(nurseUploadsDto.DescriptionImage);
        string? agreementImage = null;

        if (nurseUploadsDto.AgreementImage != null)
        {
            await _fileRepository.SaveFileAsync(nurseUploadsDto.AgreementImage);
        }

        var nurseImages = nurse.NurseImages;

        if (nurseImages == null)
        {
            nurseImages = new NurseImages
            {
                AgreementImage = agreementImage,
                DescriptionImage = descriptionImage,
                FirstPageImage = firstPageImage,
                Picture = picture,
                NurseId = nurse.Id
            };
            nurse.NurseImages = nurseImages;
        }
        else
        {

            nurseImages.AgreementImage = agreementImage;
            nurseImages.DescriptionImage = descriptionImage;
            nurseImages.FirstPageImage = firstPageImage;
            nurseImages.Picture = picture;
            nurseImages.NurseId = nurse.Id;
            _appDbContext.NurseImages.Update(nurseImages);
            await _appDbContext.SaveChangesAsync();
        }

        _appDbContext.Nurses.Update(nurse);
        await _appDbContext.SaveChangesAsync();

        return new CustomActionResult(new Result { Data = nurse });
    }

    public async Task<CustomActionResult> ReserveNurse(ReserveNurse reserveNurse)
    {
        // var nurse = await _appDbContext.Nurses.SingleOrDefaultAsync(_ => _.Id == reserveNurse.NurseId);
        // if (nurse == null)
        // {
        //     return new CustomActionResult(new Result
        //     {
        //         ErrorMessage = new ErrorModel { ErrorMessage = "پرستاری با این آی دی یافت نشد" },
        //         statusCodes = StatusCodes.Status400BadRequest
        //     });
        // }
        await _appDbContext.ReserveNurses.AddAsync(reserveNurse);
        await _appDbContext.SaveChangesAsync();
        return new CustomActionResult(new Result
        {
            Data = "رزرو با موفقیت ثبت شد"
        });
    }
}