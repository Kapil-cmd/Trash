
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Application
{
    public static class StaticMethods
    {
        private const string Key ="traSHFindEr@123"
        public static string EncryptUserData(UserJwtViewModel model,string issuedDate)
        {
            DateTime issueDate = DateTime.Parse(issuedDate);

            string jsonData = JsonConvert.SerializeObject(model);

            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(jsonData);
            TripleDES tripleDES = TripleDES.Create();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(Key + issueDate.ToString("ssmmhhddMMyy"));
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray,0,inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray,0,resultArray.Length);
        }
    }
}
