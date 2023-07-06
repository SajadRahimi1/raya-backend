using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

public class NurseRepository : INurseRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IDistributedCache _cache;
    public NurseRepository(AppDbContext appDbContext, IDistributedCache cache)
    {
        _appDbContext = appDbContext;
        _cache = cache;
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

    public async Task<CustomActionResult> GetNurseReserved(string nurseId)
    {
        var reserved = await _appDbContext.ReserveNurses.Include(_ => _.Nurse).Include(_ => _.UserReserved).Where(_ => _.NurseId.ToString() == nurseId).ToListAsync();
        return new CustomActionResult(new Result { Data = reserved });
    }

    public async Task<CustomActionResult> ReserveNurse(ReserveNurse reserveNurse)
    {
        var nurse = await _appDbContext.Nurses.SingleOrDefaultAsync(_ => _.Id == reserveNurse.NurseId);
        if (nurse == null)
        {
            return new CustomActionResult(new Result
            {
                ErrorMessage = new ErrorModel { ErrorMessage = "پرستاری با این آی دی یافت نشد" },
                statusCodes = StatusCodes.Status400BadRequest
            });
        }
        await _appDbContext.ReserveNurses.AddAsync(reserveNurse);
        await _appDbContext.SaveChangesAsync();
        return new CustomActionResult(new Result
        {
            Data = "رزرو با موفقیت ثبت شد"
        });
    }
}