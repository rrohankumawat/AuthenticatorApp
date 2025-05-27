using AuthenticatorApp.Models;
using AuthenticatorApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticatorApp.Controllers
{
    public class AuthenticatorController : Controller
    {
        private readonly TotpService _totpService;

        public AuthenticatorController(TotpService totpService)
        {
            _totpService = totpService;
        }

        public IActionResult Index()
        {
            var model = new AuthenticatorViewModel
            {
                CurrentCode = _totpService.GenerateCode(),
                RemainingSeconds = _totpService.GetRemainingSeconds(),
                SecretKey = _totpService.GetSecretKeyBase32(),
                IsAlertVisible = false
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Verify(AuthenticatorViewModel model)
        {
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            var isValid = _totpService.VerifyCode(model!.InputCode);

            var newModel = new AuthenticatorViewModel
            {
                CurrentCode = _totpService.GenerateCode(),
                RemainingSeconds = _totpService.GetRemainingSeconds(),
                SecretKey = _totpService.GetSecretKeyBase32(),
                IsValid = isValid,
                IsAlertVisible = true
            };

            return View("Index", newModel);
        }

    }
}
