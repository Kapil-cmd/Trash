using Core;
namespace Infrastructure
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();
        public SpResponse RegisterUser(RegisterUserViewModel user);
    }
}
