using Microsoft.EntityFrameworkCore;

public class NurseRepository : INurseRepository
{
    private readonly AppDbContext _appDbContext;
    public NurseRepository(AppDbContext appDbContext)
    {
        _appDbContext=appDbContext;
    }

    public Task CreateNurse(Nurse nurse)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Nurse>> GetAllNurse()
    {
        return await _appDbContext.Nurses.ToListAsync();
    }

    public Task ReserveNurse(string NurseId, List<string> days)
    {
        throw new NotImplementedException();
    }
}