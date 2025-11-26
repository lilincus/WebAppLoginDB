using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
[Authorize]
public class IndexModel : PageModel
{
    public string Username { get; set; }

    public void OnGet()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            Username = User.Identity.Name;
        }
        else
        {
            Username = "Guest";
        }
    }

    public async Task<IActionResult> OnPostLogout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToPage("/Login");
    }
}
