public interface INurseRepository{
    Task<List<Nurse>> GetAllNurse(int page);
    Task<CustomActionResult> GetStatusNurses(int page, Status status);
    Task<CustomActionResult> ReserveNurse(ReserveNurse reserveNurse,User user);
    Task<CustomActionResult> uploadNursePdf(string nurseId,IFormFile pdfFile);
    Task<CustomActionResult>  CreateNurse(Nurse nurse);
    Task<CustomActionResult> GetNursesReserved(string userId);
    CustomActionResult GetUsersReserved(string nurseId);
    Task<CustomActionResult> NurseUpdateUploads(NurseUploadsDto nurseUploadsDto);
    Task<CustomActionResult> UpdateNurseFamily(UpdateNurseFamilyDto dto);
    Task<CustomActionResult> getSingleNurse(string id);
    Task<CustomActionResult> editNurseStatus(Status status,string nurseId);
    
}