using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.App.Security
{
    internal static class AuthenticationManager
    {
        internal static void SignIn( string userName, string role )
        {
            Thread.CurrentPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity( new[] {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role),
            }, "ApplicationCookie" ) );
        }

        internal static void SignOut()
        {
            Thread.CurrentPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity( new[] {
                new Claim(ClaimTypes.Name, string.Empty),
                new Claim(ClaimTypes.Role, string.Empty),
            }, "ApplicationCookie" ) );
        }

        internal static string UserName()
        {
            return Thread.CurrentPrincipal.Identity.Name;
        }
    }
}
