using System.Security.Principal;

namespace Shengtai.Web
{
    public interface ICurrentUser
    {
        IPrincipal CurrentUser { set; }
    }
}