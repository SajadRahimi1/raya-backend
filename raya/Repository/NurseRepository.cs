using Courseproject.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

public class NurseRepository : INurseRepository
{
    private readonly AppDbContext _appDbContext;
    // private readonly IDistributedCache _cache;
    private readonly IFileRepository _fileRepository;
    // private readonly IKavehnegarRespository kavehnegarRespository;
    public NurseRepository(AppDbContext appDbContext, IFileRepository fileRepository)
    {
        _appDbContext = appDbContext;
        // _cache = cache;
        _fileRepository = fileRepository;
        // this.kavehnegarRespository = kavehnegarRespository;
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

    public async Task<List<Nurse>> GetAllNurse(int page)
    {
        List<Nurse>? nurses;
        // nurses = await _cache.GetRecordAsync<List<Nurse>?>("Nurse");
        // if (nurses == null)
        // {
        nurses = await _appDbContext.Nurses.Skip((page-1)*15).Take(15).ToListAsync();
        //     await _cache.SetRecordAsync("Nurse", nurses);
        // }
        return nurses;
    }

    public async Task<CustomActionResult> GetNursesReserved(string userId)
    {
        var reserved = await _appDbContext.ReserveNurses.Where(_ => _.UserId.ToString() == userId).ToListAsync();
        return new CustomActionResult(new Result { Data = reserved });
    }


    public CustomActionResult GetUsersReserved(string nurseId)
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
        
        nurse.formCode = _appDbContext.Nurses.Count() + 6000;
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

    public async Task<CustomActionResult> ReserveNurse(ReserveNurse reserveNurse, User user)
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
        reserveNurse.UserId = user.Id;
        await _appDbContext.ReserveNurses.AddAsync(reserveNurse);
        await _appDbContext.SaveChangesAsync();
        // await kavehnegarRespository.sendNurseReserveSms(user.PhoneNumber,user.Name);
        return new CustomActionResult(new Result
        {
            Data = "رزرو با موفقیت ثبت شد"
        });
    }

    public async Task<CustomActionResult> UpdateNurseFamily(UpdateNurseFamilyDto dto)
    {
        var nurse = await _appDbContext.Nurses.Include(_ => _.NurseFamily).SingleOrDefaultAsync(_ => _.Id == dto.NurseId);

        if (nurse == null)
        {
            return new CustomActionResult(new Result
            {
                ErrorMessage = new ErrorModel { ErrorMessage = "پرستاری یافت نشد" },
                statusCodes = 404
            });
        }

        nurse.NurseFamily = dto.nurseFamily.Select(n => new NurseFamily
        {
            Information = n.Information,
            Name = n.Name,
            PhoneNumber = n.PhoneNumber,
            KnowTime = n.KnowTime,
            NurseId = nurse.Id
        }).ToList();
        nurse.HusbandPhoneNumber = dto.husbandPhoneNumber;
        nurse.ChildPhoneNumber = dto.childPhoneNumber;
        nurse.ParentPhoneNumber = dto.parentPhoneNumber;
        if (dto.guarantee == "promissory") nurse.Guarantee = Guarantee.Promissory;
        if (dto.guarantee == "check") nurse.Guarantee = Guarantee.Check;
        if (dto.guarantee == "businessLicense") nurse.Guarantee = Guarantee.BusinessLicense;
        if (dto.guarantee == "representative") nurse.Guarantee = Guarantee.Representative;


        _appDbContext.Nurses.Update(nurse);
        await _appDbContext.SaveChangesAsync();

        return new CustomActionResult(new Result { Data = nurse });
    }

    public async Task<CustomActionResult> uploadNursePdf(string nurseId, IFormFile pdfFile)
    {
        var nurseModel = await _appDbContext.Nurses.SingleOrDefaultAsync(_ => _.Id.ToString() == nurseId);
        if (nurseModel == null)
        {
            return new CustomActionResult(new Result { statusCodes = 404, ErrorMessage = new ErrorModel { ErrorMessage = "پرستاری با این ایدی یافت نشد" } });
        }
        var pdfLink = await _fileRepository.SaveFileAsync(pdfFile);
        nurseModel.pdfLink = pdfLink;
        _appDbContext.Nurses.Update(nurseModel);
        await _appDbContext.SaveChangesAsync();
        return new CustomActionResult(new Result { Data = "با موفقیت اضافه شد" });
    }

    public async Task<CustomActionResult> getSingleNurse(string id)
    {
        var nurse = await _appDbContext.Nurses.Include(_ => _.NurseImages).Include(_ => _.NurseFamily).SingleOrDefaultAsync(_ => _.Id.ToString() == id);
        if (nurse == null)
        {
            return new CustomActionResult(new Result { statusCodes = 404 });
        }
        return new CustomActionResult(new Result { Data = nurse });
    }
}