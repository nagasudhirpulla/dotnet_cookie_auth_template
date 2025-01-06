using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public required string? ColorScheme { get; set; }

        public void OnGet()
        {
            // Read the cookie from HTTP request
            ColorScheme = Request.Cookies[nameof(ColorScheme)] ?? "none";
            SetCookieForLastVisited();
        }

        public void OnPost()
        {
            if (ColorScheme == "none")
            {
                // delete the cookie
                Response.Cookies.Delete(nameof(ColorScheme));
            }
            else if (!string.IsNullOrWhiteSpace(ColorScheme))
            {
                // Set a cookie in the HTTP response
                Response.Cookies.Append(nameof(ColorScheme), ColorScheme);
            }
            SetCookieForLastVisited();
        }

        private void SetCookieForLastVisited()
        {
            // set a cookie in the HTTP response with options
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7), // default is "session"
                HttpOnly = true, // Cookie cannot be accessible by client side script if true. default is false
                Secure = true, // Cookie can be transmitted only by HTTPS. default is false
                SameSite = SameSiteMode.Strict // Cookies will be sent with requests only initiated by the same site (i.e., the same domain, protocol, and port). default is SameSiteMode.Unspecified
            };
            Response.Cookies.Append("LastVisited", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), options);
        }
    }
}
