public class ZarinpalRepository : IZarinpalRepository
{
    private readonly AppDbContext appDbContext;

    public ZarinpalRepository(AppDbContext appDbContext)
    {
        this.appDbContext=appDbContext;
    }
    public Task<CustomActionResult> payCourse(string classCatgoryId, string userId, bool IsInstallment)
    {
        throw new NotImplementedException();
    }

    public Task<CustomActionResult> payHiringNurse(string nurseId)
    {
        throw new NotImplementedException();
    }
}