using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shengtai.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Shengtai
{
    public static class DefaultExtensions
    {
        public static string ToUnixTimeStamp(this DateTime dateTime)
        {
            return ((int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector, TKey low, TKey high) where TKey : IComparable<TKey>
        {
            Expression key = Expression.Invoke(keySelector, keySelector.Parameters.ToArray());
            Expression lowerBound = Expression.GreaterThanOrEqual(key, Expression.Constant(low));
            Expression upperBound = Expression.LessThanOrEqual(key, Expression.Constant(high));

            Expression and = Expression.AndAlso(lowerBound, upperBound);
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);

            return source.Where(lambda);
        }

        public static double Division(this int molecular, int denominator)
        {
            if (molecular == 0 || denominator == 0)
                return 0;

            return molecular * 1.0 / denominator;
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

        public static string GetEnumDescription<TEnum>(this int value) where TEnum : struct
        {
            return GetEnumDescription<TEnum>(value.ToString());
        }

        public static string GetEnumDescription<TEnum>(this string value) where TEnum : struct
        {
            if (Enum.TryParse(value, out TEnum result))
                return GetEnumDescription(result as Enum);

            return null;
        }

        public static IList<KeyValuePair<int, string>> GetEnumDictionary<TEnum>(params Enum[] skipEnums) where TEnum : struct
        {
            IList<KeyValuePair<int, string>> keyValues = new List<KeyValuePair<int, string>>();

            var values = Enum.GetValues(typeof(TEnum));
            foreach (int key in values)
            {
                if (Enum.TryParse(key.ToString(), out TEnum result))
                {
                    var current = result as Enum;
                    if (skipEnums.Contains(current))
                        continue;
                }

                var value = key.GetEnumDescription<TEnum>();
                keyValues.Add(new KeyValuePair<int, string>(key, value));
            }

            return keyValues;
        }

        public static int? Minus(this int? leftValue, int? rightValue)
        {
            if (leftValue.HasValue)
            {
                if (rightValue.HasValue)
                    return leftValue.Value - rightValue.Value;
                else
                    return leftValue.Value;
            }
            else
            {
                if (rightValue.HasValue)
                    return 0 - rightValue.Value;
                else
                    return null;
            }
        }

        public static int? Plus(this int? leftValue, int? rightValue)
        {
            if (leftValue.HasValue)
            {
                if (rightValue.HasValue)
                    return leftValue.Value + rightValue.Value;
                else
                    return leftValue.Value;
            }
            else
            {
                if (rightValue.HasValue)
                    return rightValue.Value;
                else
                    return null;
            }
        }

        public static int Plus(this int? leftValue, int rightValue)
        {
            if (leftValue.HasValue)
                return leftValue.Value + rightValue;
            else
                return rightValue;
        }

        public static int Plus(this int leftValue, int? rightValue)
        {
            if (rightValue.HasValue)
                return leftValue + rightValue.Value;
            else
                return leftValue;
        }

        public static void RunSync(Func<Task> item)
        {
            var oldContext = SynchronizationContext.Current;

            var syncContext = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(syncContext);
            syncContext.Post(async _ =>
            {
                try
                {
                    await item();
                }
                finally
                {
                    syncContext.EndMessageLoop();
                }
            }, null);
            syncContext.BeginMessageLoop();

            SynchronizationContext.SetSynchronizationContext(oldContext);
        }

        public static T RunSync<T>(Func<Task<T>> item)
        {
            var oldContext = SynchronizationContext.Current;

            var syncContext = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(syncContext);
            T result = default(T);
            syncContext.Post(async _ =>
            {
                try
                {
                    result = await item();
                }
                finally
                {
                    syncContext.EndMessageLoop();
                }
            }, null);
            syncContext.BeginMessageLoop();

            SynchronizationContext.SetSynchronizationContext(oldContext);
            return result;
        }

        #region PasswordSignInAsync

        public static async Task<SignInStatus> PasswordSignInAsync<TUser>(this IAccountService<TUser> service, string account,
            string password, bool isPersistent, bool shouldLockout) where TUser : IdentityUser
        {
            if (service.UserManager == null)
                return SignInStatus.Failure;

            var user = await FindByAccountAsync(service, account);
            if (user == null)
                return SignInStatus.Failure;

            bool result = await service.UserManager.IsLockedOutAsync(user.Id);
            if (result)
                return SignInStatus.LockedOut;

            result = await service.UserManager.CheckPasswordAsync(user, password);
            if (result)
            {
                var identityResult = await service.UserManager.ResetAccessFailedCountAsync(user.Id);
                if (identityResult.Succeeded)
                {
                    var signInStatus = await SignInOrTwoFactor(service, user, isPersistent);
                    return signInStatus;
                }
            }

            if (shouldLockout)
            {
                var identityResult = await service.UserManager.AccessFailedAsync(user.Id);
                if (identityResult.Succeeded)
                {
                    result = await service.UserManager.IsLockedOutAsync(user.Id);
                    if (result)
                        return SignInStatus.LockedOut;
                }
            }

            return SignInStatus.Failure;
        }

        public static async Task SignInAsync<TUser>(IAccountService<TUser> service, TUser user, bool isPersistent,
            bool rememberBrowser) where TUser : IdentityUser
        {
            var userIdentity = await service.UserManager.CreateIdentityAsync(user, "ApplicationCookie");
            service.AuthenticationManager.SignOut(new string[2] { "ExternalCookie", "TwoFactorCookie" });
            if (rememberBrowser)
            {
                ClaimsIdentity claimsIdentity = service.AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(user.Id);
                AuthenticationProperties properties = new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                };
                service.AuthenticationManager.SignIn(properties, new ClaimsIdentity[2] { userIdentity, claimsIdentity });
            }
            else
            {
                AuthenticationProperties properties = new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                };
                service.AuthenticationManager.SignIn(properties, new ClaimsIdentity[1] { userIdentity });
            }
        }

        private static async Task<TUser> FindByAccountAsync<TUser>(IAccountService<TUser> service, string account)
                            where TUser : IdentityUser
        {
            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException("account");

            string userId = await service.FindIdByAccountAsync(account);
            var user = await service.UserManager.FindByIdAsync(userId);
            return user;
        }

        private static async Task<SignInStatus> SignInOrTwoFactor<TUser>(IAccountService<TUser> service, TUser user, bool isPersistent)
            where TUser : IdentityUser
        {
            var result = await service.UserManager.GetTwoFactorEnabledAsync(user.Id);
            if (result)
            {
                var providers = await service.UserManager.GetValidTwoFactorProvidersAsync(user.Id);
                if (providers.Count > 0)
                {
                    result = await service.AuthenticationManager.TwoFactorBrowserRememberedAsync(user.Id);
                    if (result)
                    {
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity("TwoFactorCookie");
                        claimsIdentity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", user.Id));
                        service.AuthenticationManager.SignIn(new ClaimsIdentity[1] { claimsIdentity });

                        return SignInStatus.RequiresVerification;
                    }
                }
            }

            await SignInAsync(service, user, isPersistent, false);
            return SignInStatus.Success;
        }

        #endregion PasswordSignInAsync
    }
}