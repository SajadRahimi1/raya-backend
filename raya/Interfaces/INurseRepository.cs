public interface INurseRepository{
    Task<List<Nurse>> GetAllNurse();
    Task<CustomActionResult> ReserveNurse(ReserveNurse reserveNurse);
    Task<CustomActionResult>  CreateNurse(Nurse nurse);
    Task<CustomActionResult> GetNursesReserved(string userId);
    CustomActionResult GetUsersReserved(string nurseId);
    Task<CustomActionResult> NurseUpdateUploads(NurseUploadsDto nurseUploadsDto);
    Task<CustomActionResult> UpdateNurseFamily(UpdateNurseFamilyDto dto);
    
}