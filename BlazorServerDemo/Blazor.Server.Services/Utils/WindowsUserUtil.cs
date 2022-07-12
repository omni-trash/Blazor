using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

namespace Blazor.Server.Services.Utils;

public static class WindowsUserUtil
{
    /// <summary>
    /// Returns the authenticated user name
    /// </summary>
    /// <returns></returns>
    public static string GetUserLoginName(IPrincipal? principal)
    {
        return principal?.Identity?.Name?.Split('\\').Last().Split('@').First();
    }

    /// <summary>
    /// Returns the authenticated user displayname
    /// </summary>
    /// <returns></returns>
    public static string GetUserDisplayName(IPrincipal? principal)
    {
        try
        {
            if (principal?.Identity != null)
            {
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    var user = UserPrincipal.FindByIdentity(context, GetUserLoginName(principal));
                    return user.DisplayName;
                }
            }
        }
        catch (Exception error)
        {
            Trace.TraceError(error.Message);
        }

        return GetUserLoginName(principal);
    }
}
