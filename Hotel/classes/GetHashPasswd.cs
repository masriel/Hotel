using System;
using System.Security.Cryptography;
using System.Text;
namespace Hotel.classes
{
    internal class GetHashPasswd
    {
        public static string GetHash(string passwd)
        {
            //SHA256Managed hash = new SHA256Managed();
            string hashpwd = String.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(passwd);
            foreach (byte b in bytes)
            {
                hashpwd += b.ToString("x2");
            }
            return hashpwd;
        }
    }
}
