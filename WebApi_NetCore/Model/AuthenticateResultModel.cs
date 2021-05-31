using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_NetCore.Model
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public bool ShouldResetPassword { get; set; }

        public string PasswordResetCode { get; set; }

        public long UserId { get; set; }

        public bool RequiresTwoFactorVerification { get; set; }

        public IList<string> TwoFactorAuthProviders { get; set; }

        public string TwoFactorRememberClientToken { get; set; }

        public string ReturnUrl { get; set; }
    }
    public class AuthenticateModel
    {
        public string UserNameOrEmailAddress { get; set; }

        public string Password { get; set; }

        public string TwoFactorVerificationCode { get; set; }

        public bool RememberClient { get; set; }

        public string TwoFactorRememberClientToken { get; set; }

        public bool? SingleSignIn { get; set; }

        public string ReturnUrl { get; set; }
    }
}
