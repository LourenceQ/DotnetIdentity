using DotnetIdentity.Models;
using DotnetIdentity.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetIdentity.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityController(UserManager<IdentityUser> userManager, 
                IEmailSender emailSender,
                SignInManager<IdentityUser> signinManager,
                RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _signinManager = signinManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Signup()
        {
            var model =  new SignupViewModel() { Role = "Member"};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {            
            if (ModelState.IsValid)
            {
                if(!await _roleManager.RoleExistsAsync(model.Role))
                {
                    var role = new IdentityRole { Name = model.Role };
                    var roleResult = await _roleManager.CreateAsync(role);
                    if(!roleResult.Succeeded)
                    {
                        var errors = roleResult.Errors.Select(s => s.Description);
                        ModelState.AddModelError("Role", string.Join(",", errors));
                        return View(model);
                    }
                }

                if((await _userManager.FindByEmailAsync(model.Email)) == null)
                {
                    var user = new IdentityUser
                    {
                        Email = model.Email,
                        UserName = model.Email
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    user = await _userManager.FindByEmailAsync(model.Email);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, model.Role);
                        /*var confirmationLink = Url.ActionLink("ConfirmEmail", "Identity", new { userId = user.Id, @token = token });
                        await _emailSender.SendEmailAsync("lawrenceqf@gmail.com", user.Email, "Confirme seu endereço de email", confirmationLink);*/

                        return RedirectToAction("Signin");
                    }

                    ModelState.AddModelError("Signup", string.Join("", result.Errors.Select(x => x.Description)));
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if(result.Succeeded)
            {
                return RedirectToAction("Signin");                
            }

            return new NotFoundResult();
        }

        public async Task<IActionResult> Signin()
        {
            return View(new SigninViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Signin(SigninViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(
                    model.Username,
                    model.Password,
                    model.RememberMe, false);
                
                if (result.Succeeded)
                {
                    /*var user = await _userManager.FindByEmailAsync(model.Username);

                    var userClaims = await _userManager.GetClaimsAsync(user);

                    if(await _userManager.IsInRoleAsync(user, "Member"))
                    {
                        return RedirectToAction("Member", "Home");
                    }
                    else if(await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Admin", "Home");
                    }*/
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Login", "Cannot login.");
                    
                }
            }
            return View(model);

            /*else
            {
                return View(model);
            }*/
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

    }
}
