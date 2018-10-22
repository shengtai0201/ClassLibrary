﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Shengtai.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public static class DefaultExtensions
    {
        public static string ToUnixTimeStamp(this DateTime dateTime)
        {
            return ((int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetEnumDescription<TEnum>(this string value) where TEnum : struct
        {
            if (Enum.TryParse(value, out TEnum result))
                return GetEnumDescription(result as Enum);

            return null;
        }

        public static string GetEnumDescription<TEnum>(this int value) where TEnum : struct
        {
            return GetEnumDescription<TEnum>(value.ToString());
        }

        public static ICollection<KeyValuePair<int, string>> GetEnumDictionary<TEnum>(params Enum[] skips) where TEnum : struct
        {
            ICollection<KeyValuePair<int, string>> keyValues = new List<KeyValuePair<int, string>>();

            var values = Enum.GetValues(typeof(TEnum));
            foreach (int key in values)
            {
                if (Enum.TryParse(key.ToString(), out TEnum result))
                {
                    var current = result as Enum;
                    if (skips.Contains(current))
                        continue;
                }

                var value = key.GetEnumDescription<TEnum>();
                keyValues.Add(new KeyValuePair<int, string>(key, value));
            }

            return keyValues;
        }

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
