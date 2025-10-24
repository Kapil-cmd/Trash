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

                IEnumerable<User> users = data.ToList();

                response.Status = "00";
                response.Message = "Success";
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
        public BaseResponseModel<SpResponse> RegisterUser(RegisterUserViewModel user)
        {
            var response = new BaseResponseModel<SpResponse>();
            try
            {
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