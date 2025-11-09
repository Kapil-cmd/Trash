using Core;
using Infrastructure;
using Microsoft.IdentityModel.Tokens.Experimental;
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
                user.IsVerified = "N";
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
        public BaseResponseModel<UserDetailsViewModel> GetUserDetails(long userId)
        {
            var response = new BaseResponseModel<UserDetailsViewModel>();
            try
            {
                   if(userId == 0)
                {
                    response.Status = "1";
                    response.Message = "INVALID USER!!!";
                    return response;
                }
                   var datas = _userRepository.GetUserDetails(userId);
                if(datas.ErrorCode == "000")
                {
                    response.Status = datas.ErrorCode;
                    response.Message = datas.Message;
                    response.Data = datas.Data;
                    return response;
                }
                else
                {
                    response.Status = "1";
                    response.Message = "INVALID USER!!!";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = ex.Message;
                return response;
                return response;
            }
        }
    }
}