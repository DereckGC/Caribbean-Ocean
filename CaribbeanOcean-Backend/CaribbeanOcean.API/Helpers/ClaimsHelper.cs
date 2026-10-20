using System.Security.Claims;

namespace CaribbeanOcean.API.Helpers
{
    public static class ClaimsHelper
    {
        public static int? GetUserId(HttpContext context)
        {
            var claim = context.User.FindFirst(ClaimTypes.NameIdentifier)
                     ?? context.User.FindFirst("userId")
                     ?? context.User.FindFirst("sub");

            if (claim == null) return null;
            return int.TryParse(claim.Value, out var id) ? id : null;
        }

        public static string GetIpAddress(HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        }

        public static string GetScreen(HttpContext context)
        {
            return context.Request.Headers["X-Screen"].ToString() is string screen && !string.IsNullOrEmpty(screen)
                ? screen
                : "unknown";
        }
    }
}