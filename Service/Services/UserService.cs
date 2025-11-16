using Core;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens.Experimental;
namespace Application
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository userRepository,IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
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
                if(data.ErrorCode == "000")
                {
                    var token = StaticMethods.GenTokenkey(data.Data);
                    data.Data.Token = token.Data.Token;
                    data.Data.ExpiryTimeUtc = token.Data.ExpiryTimeUtc;
                }
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
        public async Task<BaseResponseModel<UpdateUserDetailViewModel>> UpdateUserInfo(UpdateUserDetailViewModel user)
        {
            var response = new BaseResponseModel<UpdateUserDetailViewModel>();
            try
            {
                if (user.UserId == 0)
                {
                    response.Status = "1";
                    response.Message = "NO USER FOUND!!!";
                    return response;
                }
                if (user.profileImage != null)
                {
                    var path = DefaultConfiguration.StaticConfiguration.GetSection("Images:ProfileImages").Value;

                    var file = user.profileImage;

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    var extension = Path.GetExtension(file.FileName);
                    user.ImageName = $"{Guid.NewGuid().ToString()}{extension}";

                    var imagePath = Path.Combine(path, user.ImageName);
                    
                    using(var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

                    user.ImageUrl = $"{baseUrl}/Images/ProfileImage/{user.ImageName}";
                    
                }

                var request = _userRepository.UpdateUserDetails(user);

                if(request.ErrorCode == "000")
                {
                    response.Status = request.ErrorCode;
                    response.Message = request.Message;
                    return response;
                }
                else
                {
                    response.Status = "1";
                    response.Message = "TECHNICAL ERROR OCCURRED WHILE UPDATING DETAILS!!!";
                    return response;
                }
            }catch(Exception ex) 
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRED WHILE PROCESSING REQUEST!!!";
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