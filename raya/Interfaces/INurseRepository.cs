public interface INurseRepository{
    Task<List<Nurse>> GetAllNurse();
    Task<CustomActionResult> ReserveNurse(ReserveNurse reserveNurse);
    Task<CustomActionResult>  CreateNurse(Nurse nurse);
    Task<CustomActionResult> GetNurseReserved(string nurseId);
    
}