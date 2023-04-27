using System.Web.Security;
namespace Hotel.classes
{
    public abstract class GenerateRwd
    {
        public static string GeneratePasswd() 
        {
            return Membership.GeneratePassword(8, 2);
        }
    }
}