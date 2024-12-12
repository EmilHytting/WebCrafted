using Microsoft.AspNetCore.Mvc;
using WebCrafted.Services;
using WebCrafted.Data;

namespace WebCrafted.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;

        public AccountController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("RegisterView"); // Sørg for, at din view hedder "RegisterView.cshtml"
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string email, string password, string firstName, string lastName)
        {
            // Sørg for, at alle nødvendige felter er udfyldt
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Returner fejl, hvis nogle felter mangler
                ViewData["ErrorMessage"] = "Alle felter skal udfyldes.";
                return View(); // Vis registerformularen igen
            }

            // Opretter bruger med de oplysninger, der er blevet sendt via form (email, password, osv.)
            var isCreated = await _loginService.CreateUser(email, password, firstName, lastName);

            if (isCreated)
            {
                // Hvis brugeren blev oprettet korrekt, omdirigerer vi til login-siden
                return RedirectToAction("Login", "Account");  // Omdirigerer til Login view i AccountController
            }
            else
            {
                // Hvis oprettelsen fejlede (f.eks. hvis emailen allerede findes), viser vi en fejlmeddelelse
                ViewData["ErrorMessage"] = "En bruger med den e-mail findes allerede.";
                return View();  // Returnerer tilbage til Register viewet med fejlmeddelelsen
            }
        }
    }
}