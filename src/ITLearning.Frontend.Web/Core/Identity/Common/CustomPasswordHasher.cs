using ITLearning.Backend.Database.Entities;
using ITLearning.Shared.Extensions;
using Microsoft.AspNet.Identity;
using System;
using System.Text;

namespace ITLearning.Frontend.Web.Core.Identity.Common
{
    public class CustomPasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return password.ToBase64();
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            return string.Equals(hashedPassword, providedPassword.ToBase64()) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}