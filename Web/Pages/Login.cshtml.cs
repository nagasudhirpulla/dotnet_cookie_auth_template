using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Web.Pages
{
    public class LoginModel : PageModel
    {
        [StringLength(100, MinimumLength = 4)]
        [Required]
        [BindProperty]
        public string? Username { get; set; }

        [StringLength(60, MinimumLength = 8)]
        [BindProperty]
        public string? Password { get; set; }

        public void OnGet()
        {
            // TODO - check if user is already logged in
        }
        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Username == "admin" && Password == "password")
            {
                // login the user
                ClaimsPrincipal user = new(new ClaimsIdentity([
                    new Claim(ClaimTypes.Name, Username),
                    new Claim(ClaimTypes.Role, "user")
                ], CookieAuthenticationDefaults.AuthenticationScheme));
                var authProperties = new AuthenticationProperties { };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, authProperties);
                return RedirectToPage(nameof(Index));
            }
            else
            {
                // throw passoword exception
                ModelState.AddModelError(string.Empty, "Invalid username password combination");
            }
            return Page();
        }
    }
}
