public interface INurseRepository{
    Task<List<Nurse>> GetAllNurse();
    Task ReserveNurse(string NurseId,List<string> days);
    Task CreateNurse(Nurse nurse);
}