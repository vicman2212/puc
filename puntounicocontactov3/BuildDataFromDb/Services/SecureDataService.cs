using BuildDataFromDb.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuildDataFromDb.Services
{
    public class SecureDataService : IEncrypt<string>
    {
        public string DecryptData(string objeto)
        {
            string hash = "codigo";
            byte[] data = Convert.FromBase64String(objeto);

            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF32Encoding.UTF8.GetString(result);
        }

        public string EncryptData(string objeto)
        {
            string hash = "codigo";
            byte[] data = UTF8Encoding.UTF8.GetBytes(objeto);

            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }
    }
}
