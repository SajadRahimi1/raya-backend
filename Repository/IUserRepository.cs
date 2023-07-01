public interface IUserRepository{
    Task<List<User>> GetAllAsync();
}