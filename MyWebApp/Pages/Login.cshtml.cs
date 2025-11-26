using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MyWebApp.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net; // ✅ Add this to use BCrypt

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly ApplicationDbContext _db;

    // ✅ Make non-nullable properties nullable or initialize them
    [BindProperty]
    public string? Username { get; set; }

    [BindProperty]
    public string? Password { get; set; }

    public string? ErrorMessage { get; set; }

    public LoginModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            ErrorMessage = "Please enter both username and password.";
            return Page();
        }

        // Find user by username
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == Username);

        // Check password using BCrypt
        if (user == null || !BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
        {
            ErrorMessage = "Invalid login.";
            return Page();
        }

        // Create authentication claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Username)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        // Redirect to home page after successful login
        return RedirectToPage("/Index");
    }
}
