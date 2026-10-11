
using CourseCenterv2.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenterv2.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // Display all Admin accounts
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var admins = new List<ApplicationUser>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    admins.Add(user);
                }
            }

            return View(admins);
        }

        // Display the Create page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new Admin account
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            string email,
            string password)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Email and password are required.");

                return View();
            }

            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "This email is already registered.");

                return View();
            }

            var admin = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(
                admin, password);

            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        error.Description);
                }

                return View();
            }

            var roleResult = await _userManager.AddToRoleAsync(
                admin, "Admin");

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(admin);

                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        error.Description);
                }

                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        // Delete an Admin account
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var admin = await _userManager.FindByIdAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            // Prevent deleting the currently signed-in account
            var currentUserId = _userManager.GetUserId(User);

            if (admin.Id == currentUserId)
            {
                TempData["Error"] =
                    "You cannot delete your own account.";

                return RedirectToAction(nameof(Index));
            }

            // Only Admin accounts can be deleted here
            if (!await _userManager.IsInRoleAsync(admin, "Admin"))
            {
                return Forbid();
            }

            // Never delete an account that has SuperAdmin role
            if (await _userManager.IsInRoleAsync(admin, "SuperAdmin"))
            {
                TempData["Error"] =
                    "A SuperAdmin account cannot be deleted here.";

                return RedirectToAction(nameof(Index));
            }

            var result = await _userManager.DeleteAsync(admin);

            if (!result.Succeeded)
            {
                TempData["Error"] = string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

