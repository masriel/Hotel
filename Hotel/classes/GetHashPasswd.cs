using System;
using System.Linq;
using System.Text;

namespace Hotel.classes
{
    internal static class GetHashPasswd
    {
        public static string GetHash(string passwd)
        {
            //SHA256Managed hash = new SHA256Managed();
            string hashpwd = String.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(passwd);
            return bytes.Aggregate(hashpwd, (current, b) => current + b.ToString("x2"));
        }
    }
}
