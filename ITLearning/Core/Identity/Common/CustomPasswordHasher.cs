using ITLearning.Frontend.Web.DAL.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Text;

namespace ITLearning.Frontend.Web.Core.Identity.Common
{
    public class CustomPasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return ToBase64(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            return string.Equals(hashedPassword, ToBase64(providedPassword)) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }

        private string ToBase64(string textToEncode)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(textToEncode));
        }
    }
}