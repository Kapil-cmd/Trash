
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application
{
    public static class StaticMethods
    {
        private const string Key = "traSHFindEr@123";
        public static string EncryptUserData(UserJwtViewModel model, string issuedDate)
        {
            DateTime issueDate = DateTime.Parse(issuedDate);

            string jsonData = JsonConvert.SerializeObject(model);

            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(jsonData);
            TripleDES tripleDES = TripleDES.Create();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(Key + issueDate.ToString("ssmmhhddMMyy"));
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static UserJwtViewModel DecryptUserData(string encryptedData, string issuedDate)
        {
            DateTime issueDates = DateTime.Parse(issuedDate);

            byte[] inputArray = Convert.FromBase64String(encryptedData);
            TripleDES tripleDES = TripleDES.Create();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(Key + issueDates.ToString("ssmmhhddMMyy"));
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cryptoTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cryptoTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            string jsonData = UTF8Encoding.UTF8.GetString(resultArray);
            UserJwtViewModel model = JsonConvert.DeserializeObject<UserJwtViewModel>(jsonData);
            return model;
        }
        //public static List<Claim> GetClaims(UserJwtViewModel model)
        //{
        //    string issuedDate = StaticMethods.GetDateTime().ToString("G");
        //    var userData = StaticMethods.EncryptUserData(model, issuedDate);
        //    var claims = new List<Claim>
        //    {
        //        new Claim("UserToken", userData),
        //        new Claim("IssuedDate", issuedDate),
        //        new Claim(ClaimTypes.Name, model.UserName??model.EmailAddress),
        //    };
        //    return claims;
        //}
        public static BaseResponseModel<LoginResponseViewModel> GenTokenkey(LoginResponseViewModel model)
        {
            try
            {
                LoginResponseViewModel response = new LoginResponseViewModel();
                response = new LoginResponseViewModel();
                if (model == null) throw new ArgumentException(nameof(model));
                // Get secret key
                var key = Encoding.ASCII.GetBytes(DefaultConfiguration.StaticConfiguration.GetSection("JsonWebTokenKeys:IssuerSigningKey").Value);
                DateTime notBefore = new DateTimeOffset(DateTime.Now).DateTime;
                DateTime expires = new DateTimeOffset(DateTime.Now.AddMinutes(Convert.ToInt32(DefaultConfiguration.StaticConfiguration.GetSection("JsonWebTokenKeys:ValidationLifeTimeInMin").Value ?? "5"))).DateTime;

                response.ExpiryTimeUtc = expires.ToUniversalTime().ToString("s");
                var JWToken = new JwtSecurityToken(
                    issuer: DefaultConfiguration.StaticConfiguration.GetSection("JsonWebTokenKeys:ValidIssuer").Value,
                    audience: DefaultConfiguration.StaticConfiguration.GetSection("JsonWebTokenKeys:ValidAudience").Value,
                    //claims: GetClaims(model),
                    notBefore: notBefore,
                    expires: expires,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                response.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                response.RefreshToken = model.RefreshToken;
                response.UserId = model.UserId;
                response.EmailAddress = model.EmailAddress;
                response.RefreshToken = model.RefreshToken;
                response.UserName = model.UserName;
                response.UserName = model.UserName;
                response.UserId = model.UserId;

                return new BaseResponseModel<LoginResponseViewModel>()
                {
                    Status = "000",
                    Message = "Login Successfull",
                    Data = response,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<LoginResponseViewModel>()
                {
                    Status = "500",
                    Message = "Error: " + ex.Message,
                };
            }
        }

        public static BaseResponseModel<UserJwtViewModel> ParseToken(string token)
        {
            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            if (string.IsNullOrWhiteSpace(token))
            {
                return new BaseResponseModel<UserJwtViewModel>
                {
                    Message = "NO TOKEN FOUND!!!",
                    Status = "404"
                };

            }
            token = token?.Replace("Bearer", "");

            var jwthandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(DefaultConfiguration.StaticConfiguration.GetSection("JsonWebTokenKeys:IssuerSigningKey").Value);
            try
            {
                jwthandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = DefaultConfiguration.StaticConfiguration.GetSection("JsonWebTokenKeys:ValidIssuer").Value,
                    ValidAudience = DefaultConfiguration.StaticConfiguration.GetSection("JsonWebTokenKeys:ValidAudience").Value,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero,

                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                string userToken = jwtToken.Claims.Where(x => x.Type == "UserToken").FirstOrDefault().Value.ToString();
                string issuedDate = jwtToken.Claims.Where(x => x.Type == "IssuedDate").FirstOrDefault().Value.ToString();
                var userData = StaticMethods.DecryptUserData(userToken, issuedDate);


                return new BaseResponseModel<UserJwtViewModel>()
                {
                    Status = "405",
                    Message = "Token has expired",
                    Data = userData,
                };


            }
            catch (Exception ex)
            {
                return new BaseResponseModel<UserJwtViewModel>()
                {
                    Status = "405",
                    Message = "Token has expired",
                };

            }

        }
    }
}
