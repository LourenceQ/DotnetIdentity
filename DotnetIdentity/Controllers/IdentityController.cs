using DotnetIdentity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotnetIdentity.Controllers
{
    public class IdentityController : Controller
    {        
        public async Task<IActionResult> Signup()
        {
            var model =  new SignupViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {            
            return View(model);
        }

        public async Task<IActionResult> Signin()
        {
            return View();
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

    }
}
