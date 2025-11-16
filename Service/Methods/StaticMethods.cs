using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            string jsonData = JsonConvert.SerializeObject(model);
            byte[] plainBytes = Encoding.UTF8.GetBytes(jsonData);

            using Aes aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(Key + issuedDate)); // 32-byte key
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.GenerateIV(); // random IV for security

            using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            byte[] combined = new byte[aes.IV.Length + encryptedBytes.Length];
            Array.Copy(aes.IV, 0, combined, 0, aes.IV.Length);
            Array.Copy(encryptedBytes, 0, combined, aes.IV.Length, encryptedBytes.Length);

            return Convert.ToBase64String(combined);
        }

        public static UserJwtViewModel DecryptUserData(string encryptedData, string issuedDate)
        {
            byte[] combined = Convert.FromBase64String(encryptedData);

            using Aes aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(Key + issuedDate));
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] iv = new byte[16];
            Array.Copy(combined, 0, iv, 0, iv.Length);
            aes.IV = iv;

            byte[] cipherText = new byte[combined.Length - iv.Length];
            Array.Copy(combined, iv.Length, cipherText, 0, cipherText.Length);

            using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

            string jsonData = Encoding.UTF8.GetString(decryptedBytes);
            return JsonConvert.DeserializeObject<UserJwtViewModel>(jsonData);
        }

        // Generate JWT
        public static BaseResponseModel<LoginResponseViewModel> GenTokenkey(LoginResponseViewModel model)
        {
            try
            {
                if (model == null) throw new ArgumentException(nameof(model));

                var key = Encoding.ASCII.GetBytes(DefaultConfiguration.StaticConfiguration
                    .GetSection("JsonWebTokenKeys:IssuerSigningKey").Value);

                string issuedDate = DateTime.UtcNow.ToString("s");
                string encryptedUserData = EncryptUserData(new UserJwtViewModel
                {
                    UserId = model.UserId,
                    EmailAddress = model.EmailAddress,
                    UserName = model.UserName,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Status = model.Status
                }, issuedDate);

                var claims = new List<Claim>
                {
                    new Claim("UserToken", encryptedUserData),
                    new Claim("IssuedDate", issuedDate),
                    new Claim(JwtRegisteredClaimNames.Sub, model.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, model.EmailAddress ?? ""),
                    new Claim(JwtRegisteredClaimNames.UniqueName, model.UserName ?? "")
                };

                DateTime expires = DateTime.UtcNow.AddMinutes(
                    Convert.ToInt32(DefaultConfiguration.StaticConfiguration
                        .GetSection("JsonWebTokenKeys:ValidationLifeTimeInMin").Value ?? "5"));

                var JWToken = new JwtSecurityToken(
                    issuer: DefaultConfiguration.StaticConfiguration.GetSection("JsonWebTokenKeys:ValidIssuer").Value,
                    audience: DefaultConfiguration.StaticConfiguration.GetSection("JsonWebTokenKeys:ValidAudience").Value,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: expires,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                string tokenString = new JwtSecurityTokenHandler().WriteToken(JWToken);

                model.Token = tokenString;
                model.ExpiryTimeUtc = expires.ToUniversalTime().ToString("s");

                return new BaseResponseModel<LoginResponseViewModel>
                {
                    Status = "000",
                    Message = "Login Successful",
                    Data = model
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<LoginResponseViewModel>
                {
                    Status = "500",
                    Message = "Error: " + ex.Message
                };
            }
        }

        // Validate JWT
        public static BaseResponseModel<UserJwtViewModel> ParseToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return new BaseResponseModel<UserJwtViewModel>
                {
                    Status = "404",
                    Message = "NO TOKEN FOUND!!!"
                };
            }

            token = token.Replace("Bearer ", "");

            var key = Encoding.ASCII.GetBytes(DefaultConfiguration.StaticConfiguration.GetSection("JsonWebTokenKeys:IssuerSigningKey").Value);
            var jwthandler = new JwtSecurityTokenHandler();

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
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                string userToken = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserToken")?.Value;
                string issuedDate = jwtToken.Claims.FirstOrDefault(c => c.Type == "IssuedDate")?.Value;

                if (string.IsNullOrEmpty(userToken) || string.IsNullOrEmpty(issuedDate))
                    throw new Exception("Required claims missing");

                var userData = DecryptUserData(userToken, issuedDate);

                return new BaseResponseModel<UserJwtViewModel>
                {
                    Status = "000",
                    Message = "Token valid",
                    Data = userData
                };
            }
            catch (SecurityTokenExpiredException)
            {
                return new BaseResponseModel<UserJwtViewModel>
                {
                    Status = "405",
                    Message = "Token has expired"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<UserJwtViewModel>
                {
                    Status = "500",
                    Message = "Token validation failed: " + ex.Message
                };
            }
        }
    }
}
