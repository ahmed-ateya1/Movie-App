using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.Domain.IdentityEntites;
using MovieApp.Core.DTO;
using MovieApp.Core.Enumerator;
using MovieApp.Infrastructure.ApplicationDbContext;

namespace MovieApp.UI.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = ModelState.Values.SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage).ToList();
                return View(registerDTO);
            }
            var user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                Address = registerDTO.Address,
                PhoneNumber = registerDTO.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                var roleType = registerDTO.userOption == UserOption.ADMIN ? 
                    nameof(UserOption.ADMIN) : nameof(UserOption.USER);

                if(await _roleManager.FindByNameAsync(roleType)==null)
                {
                    ApplicationRole applicationRole = new ApplicationRole() { Name = roleType };
                    var resultRole = await _roleManager.CreateAsync(applicationRole);
                    if(!resultRole.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Failed Add Error");
                    }
                }
                var resultAddRoleToUser = await _userManager.AddToRoleAsync(user, roleType);
                if (!resultAddRoleToUser.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Failed asign role to user");
                    return View(registerDTO);
                }
                return RedirectToAction("Login");
            }
            else
            {
               foreach(var error in result.Errors)
               {
                    ModelState.AddModelError(string.Empty, error.Description);
               }
            }
            return View(registerDTO);
        }
        [HttpGet]
        public IActionResult Login(string returnURL=null)
        {
            ViewBag.ReturnURL = returnURL;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string returnURL = null)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnURL = returnURL;
                return View(loginDTO);
            }
            var result = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user =
                    await _userManager.FindByNameAsync(loginDTO.UserName);
                if(user !=null && await _userManager.IsInRoleAsync(user,UserOption.ADMIN.ToString()))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                if (!String.IsNullOrEmpty(returnURL)&&Url.IsLocalUrl(returnURL))
                {
                    return LocalRedirect(returnURL);
                }
                return RedirectToAction("Index", "Movie");
            }


            ModelState.AddModelError(string.Empty, "Invaild username or password");
            ViewBag.ReturnURL = returnURL;
            return View(loginDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> UniqueEmail(string Email)
        {
            if (await _userManager.FindByEmailAsync(Email) == null)
                return Json(true);
            return Json(false);
        }
    }
}
