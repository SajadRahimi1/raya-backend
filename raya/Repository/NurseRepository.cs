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

    public Task CreateNurse(Nurse nurse)
    {
        throw new NotImplementedException();
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

    public Task ReserveNurse(string NurseId, List<string> days)
    {
        throw new NotImplementedException();
    }
}