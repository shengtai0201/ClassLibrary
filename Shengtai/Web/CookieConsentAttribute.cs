using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shengtai.Web
{
    public class CookieConsentAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Some government relax their interpretation of the law somewhat:
        /// After the first page with the message, clicking anything other than the cookie refusal link may be interpreted as implicitly allowing cookies. 
        /// </summary>
        public bool ImplicitlyAllowCookies { get; set; }

        public CookieConsentAttribute()
        {
            ImplicitlyAllowCookies = true;
        }

        private const string cookieConsentCookieName = "cookieConsent";
        private const string cookieConsentContextName = "cookieConsentInfo";

        private class CookieConsentInfo
        {
            public bool NeedToAskConsent { get; set; }
            public bool HasConsent { get; set; }

            public CookieConsentInfo()
            {
                NeedToAskConsent = true;
                HasConsent = false;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var consentInfo = new CookieConsentInfo();

            var request = filterContext.HttpContext.Request;

            // Check if the user has a consent cookie
            var consentCookie = request.Cookies[cookieConsentCookieName];

            if (consentCookie == null)
            {
                // No consent cookie. We first check the Do Not Track header value, this can have the value "0" or "1"
                string dnt = request.Headers.Get("DNT");

                // If we receive a DNT header, we accept its value (0 = give consent, 1 = deny) and do not ask the user anymore...
                if (!String.IsNullOrEmpty(dnt))
                {
                    consentInfo.NeedToAskConsent = false;

                    if (dnt == "0")
                    {
                        consentInfo.HasConsent = true;
                    }
                }
                else
                {
                    if (IsSearchCrawler(request.Headers.Get("User-Agent")))
                    {
                        // don't ask consent from search engines, also don't set cookies
                        consentInfo.NeedToAskConsent = false;
                    }
                    else
                    {
                        // first request on the site and no DNT header (we use session cookie, which is allowed by EU cookie law). 
                        consentCookie = new HttpCookie(cookieConsentCookieName)
                        {
                            Value = "asked"
                        };

                        filterContext.HttpContext.Response.Cookies.Add(consentCookie);
                    }
                }
            }
            else
            {
                // we received a consent cookie
                consentInfo.NeedToAskConsent = false;

                if (ImplicitlyAllowCookies && consentCookie.Value == "asked")
                {
                    // consent is implicitly given & stored
                    consentCookie.Value = "true";
                    consentCookie.Expires = DateTime.UtcNow.AddYears(1);
                    filterContext.HttpContext.Response.Cookies.Set(consentCookie);

                    consentInfo.HasConsent = true;
                }
                else if (consentCookie.Value == "true")
                {
                    consentInfo.HasConsent = true;
                }
                else
                {
                    // assume consent denied
                    consentInfo.HasConsent = false;
                }
            }

            HttpContext.Current.Items[cookieConsentContextName] = consentInfo;

            base.OnActionExecuting(filterContext);
        }

        private bool IsSearchCrawler(string userAgent)
        {
            if (!string.IsNullOrEmpty(userAgent))
            {
                string[] crawlers = new string[]
                {
                    "Baiduspider",
                    "Googlebot",
                    "YandexBot",
                    "YandexImages",
                    "bingbot",
                    "msnbot",
                    "Vagabondo",
                    "SeznamBot",
                    "ia_archiver",
                    "AcoonBot",
                    "Yahoo! Slurp",
                    "AhrefsBot"
                };
                foreach (string crawler in crawlers)
                    if (userAgent.Contains(crawler))
                        return true;
            }
            return false;
        }

        public static void SetCookieConsent(bool consent)
        {
            var consentCookie = new HttpCookie(CookieConsentAttribute.cookieConsentCookieName)
            {
                Value = consent ? "true" : "false",
                Expires = DateTime.UtcNow.AddYears(1)
            };
            HttpContext.Current.Response.Cookies.Set(consentCookie);
        }

        public static bool NeedToAskCookieConsent
        {
            get { return (HttpContext.Current.Items[cookieConsentContextName] as CookieConsentInfo ?? new CookieConsentInfo()).NeedToAskConsent; }
        }

        public static bool HasConsent
        {
            get { return (HttpContext.Current.Items[cookieConsentContextName] as CookieConsentInfo ?? new CookieConsentInfo()).HasConsent; }
        }
    }
}
