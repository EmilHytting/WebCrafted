using Microsoft.AspNetCore.Mvc;
using WebCrafted.Services;

namespace WebCrafted.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View("LoginView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe)
        {
            var isValidUser = await _loginService.ValidateUser(email, password);

            if (isValidUser)
            {
                return RedirectToAction("HomeView", "Home");
            }
            
            ViewData["ErrorMessage"] = "Invalid login attempt.";
            return View();
        }

        // Eventuelt andre handlinger, som f.eks. CreateUser
    }
}