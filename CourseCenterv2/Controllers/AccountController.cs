
using CourseCenterv2.Infrastructure.Identity;
using CourseCenterv2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenterv2.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(
        LoginViewModel model,
        string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                await _signInManager.SignOutAsync();
                ModelState.AddModelError(
                    string.Empty,
                    "تعذر العثور على بيانات الحساب.");

                return View(model);
            }

            // SuperAdmin goes to the admin management page.
            if (await _userManager.IsInRoleAsync(user, "SuperAdmin"))
            {
                return RedirectToAction("Index", "Admin");
            }

            // Regular Admin goes to student management.
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                if (!string.IsNullOrWhiteSpace(returnUrl)
                    && Url.IsLocalUrl(returnUrl)
                    && !returnUrl.StartsWith(
                        "/Admin",
                        StringComparison.OrdinalIgnoreCase))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("Index", "Student");
            }

            // Accounts without an allowed role must not stay signed in.
            await _signInManager.SignOutAsync();

            ModelState.AddModelError(
                string.Empty,
                "هذا الحساب لا يمتلك صلاحية الدخول إلى النظام.");

            return View(model);
        }

        ModelState.AddModelError(
            string.Empty,
            "البريد الإلكتروني أو كلمة المرور غير صحيحة.");

        return View(model);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}

