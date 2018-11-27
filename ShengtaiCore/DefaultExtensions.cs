using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Shengtai.Web;
using Shengtai.Web.Telerik;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public static class DefaultExtensions
    {
        public static void SetDataCollection<TKey, TViewModel, TEntity>(this IDataSourceResponse<TViewModel> response, IQueryable<TEntity> responseData, Action<TViewModel, TEntity> decorator = null) where TViewModel : ViewModel<TKey, TViewModel, TEntity>
        {
            var dataCollection = responseData.ToList();
            foreach (var data in dataCollection)
            {
                var viewModel = ViewModel<TKey, TViewModel, TEntity>.NewInstance(data);
                decorator?.Invoke(viewModel, data);

                response.DataCollection.Add(viewModel);
            }
        }

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

        public static KeyValuePair<int, string> GetEnumKeyValue(this Enum e)
        {
            int key = Convert.ToInt32(e);
            string value = e.GetEnumDescription();

            return new KeyValuePair<int, string>(key, value);
        }

        public static async Task<SignInResult> PasswordSignInAsync<TUser>(this IAccountService<TUser> service, 
            UserManager<TUser> userManager, SignInManager<TUser> signInManager, 
            string account, string password, bool isPersistent, bool lockoutOnFailure) where TUser : IdentityUser
        {
            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException("account");

            string userId = await service.FindIdByAccountAsync(account);
            TUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return SignInResult.Failed;

            SignInResult result = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
            if (result.Succeeded)
            {
                bool flag = userManager.SupportsUserTwoFactor;
                if (flag)
                {
                    flag = await userManager.GetTwoFactorEnabledAsync(user);
                    if (flag)
                    {
                        flag = (await userManager.GetValidTwoFactorProvidersAsync(user)).Count > 0;
                    }
                }

                if (flag && !(await signInManager.IsTwoFactorClientRememberedAsync(user)))
                {
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(IdentityConstants.TwoFactorUserIdScheme);
                    claimsIdentity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userId));

                    await AuthenticationHttpContextExtensions.SignInAsync(signInManager.Context, IdentityConstants.TwoFactorUserIdScheme, new ClaimsPrincipal(claimsIdentity));

                    return SignInResult.TwoFactorRequired;
                }

                await signInManager.SignInAsync(user, isPersistent, null);
                return SignInResult.Success;
            }

            return result;
        }

        public static void ProcessStart(string fileName, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                Arguments = arguments
            };

            Process process = Process.Start(startInfo);

            StreamReader reader = process.StandardOutput;
            string line = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                if (!string.IsNullOrEmpty(line))
                    Console.WriteLine(line);

                line = reader.ReadLine();
            }
            reader.Close();
            reader.Dispose();

            process.WaitForExit();
            process.Close();
            process.Dispose();
        }
    }
}
