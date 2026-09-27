using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Authenticate_User_Web_Service.Controllers
{
    public class BasicAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check that the request has an Authorization header.
            var authHeader = context.HttpContext.Request.Headers.Authorization.ToString();
            if (string.IsNullOrEmpty(authHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // The value should be "Basic <base64-encoded-credentials>".
            var authHeaderParts = authHeader.Split(' ');
            if (authHeaderParts.Length != 2 ||
                !string.Equals(authHeaderParts[0], "Basic", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Decode the credentials; malformed Base64 is unauthorized too.
            string credentials;
            try
            {
                credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderParts[1]));
            }
            catch (FormatException)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Credentials should be "username:password".
            var parts = credentials.Split(':');

            // Match the username without case sensitivity and the password exactly.
            if (parts.Length != 2 ||
                !string.Equals(parts[0], "instructor01", StringComparison.OrdinalIgnoreCase) ||
                parts[1] != "Password01")
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Correct credentials allow the request to continue.
            base.OnActionExecuting(context);
        }
    }
}
