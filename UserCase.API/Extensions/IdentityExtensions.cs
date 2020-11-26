using System;
using System.Linq;
using System.Security.Claims;

namespace UserCase.Extensions
{
    public static class IdentityExtensions
    {
        public static int GetId(this ClaimsPrincipal claimsPrincipal)
        {
            var value = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
                ?.Value;
            if (value == null)
            {
                throw new InvalidOperationException();
            }
            return int.Parse(value);
        }
    }
}