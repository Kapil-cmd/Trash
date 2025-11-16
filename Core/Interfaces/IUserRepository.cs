using Core;
namespace Infrastructure
{
    public interface IUserRepository
    {
        public SpResponse<List<User>> GetAllUsers();
        public SpResponse<string> RegisterUser(RegisterUserViewModel user);
        public SpResponse<LoginResponseViewModel> Login(LoginViewModel login);
        public SpResponse<UserDetailsViewModel> GetUserDetails(long userId);
        public SpResponse<UpdateUserDetailViewModel> UpdateUserDetails(UpdateUserDetailViewModel userDetails);
        public SpResponse<string> DbSeed(DbSeedModel model);
    }
}
