using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Shengtai.Web;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public static class DefaultExtensions
    {
        public static async Task<SignInResult> PasswordSignInAsync<TUser>(this IAccountService<TUser> service, string account, string password, bool isPersistent, bool lockoutOnFailure) where TUser : IdentityUser
        {
            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException("account");

            string userId = await service.FindIdByAccountAsync(account);
            TUser user = await service.UserManager.FindByIdAsync(userId);
            if (user == null)
                return SignInResult.Failed;

            SignInResult result = await service.SignInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
            if (result.Succeeded)
            {
                bool flag = service.UserManager.SupportsUserTwoFactor;
                if (flag)
                {
                    flag = await service.UserManager.GetTwoFactorEnabledAsync(user);
                    if (flag)
                    {
                        flag = (await service.UserManager.GetValidTwoFactorProvidersAsync(user)).Count > 0;
                    }
                }

                if (flag && !(await service.SignInManager.IsTwoFactorClientRememberedAsync(user)))
                {
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(IdentityConstants.TwoFactorUserIdScheme);
                    claimsIdentity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userId));

                    await AuthenticationHttpContextExtensions.SignInAsync(service.SignInManager.Context, IdentityConstants.TwoFactorUserIdScheme, new ClaimsPrincipal(claimsIdentity));

                    return SignInResult.TwoFactorRequired;
                }

                await service.SignInManager.SignInAsync(user, isPersistent, null);
                return SignInResult.Success;
            }

            return result;
        }
    }
}
