using Core;
using Infrastructure;
namespace Application
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public BaseResponseModel<IEnumerable<User>> GetAllUsers()
        {
            var response = new BaseResponseModel<IEnumerable<User>>();
            try
            {
                var data = _userRepository.GetAllUsers();

                IEnumerable<User> users = data.Data.ToList();

                response.Status = data.ErrorCode;
                response.Message = data.Message;
                response.Data = users;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = ex.Message;
                return response;

            }
        }
        public BaseResponseModel<LoginResponseViewModel> Login(LoginViewModel model)
        {
            var response = new BaseResponseModel<LoginResponseViewModel>();
            try
            {
                var data = _userRepository.Login(model);

                response.Status = data.ErrorCode;
                response.Data = data.Data;
                response.Message = data.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = ex.Message;
                return response;
            }
        }
        public BaseResponseModel<SpResponse<string>> RegisterUser(RegisterUserViewModel user)
        {
            var response = new BaseResponseModel<SpResponse<string>>();
            try
            {
                user.UserName = StaticMethod.GetUserName(user.EmailAddress);
                var data = _userRepository.RegisterUser(user);
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                response.Data = data;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = ex.Message;
                return response;
            }
        }

    }
}