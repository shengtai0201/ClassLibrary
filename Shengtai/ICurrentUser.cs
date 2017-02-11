using System.Security.Principal;

namespace Shengtai
{
    public interface ICurrentUser
    {
        IPrincipal CurrentUser { set; }
    }
}
