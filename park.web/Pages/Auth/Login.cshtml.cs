using chrep.core.park.uof;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;

namespace park.web.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IUnitofworks _unitofworks;
        private readonly IToastNotification _toastNotification;
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public LoginModel(IUnitofworks unitofworks,IToastNotification toastNotification)
        {
            _unitofworks = unitofworks;
            _toastNotification = toastNotification;
        }
        public void OnGet()
        {

        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (Username.IsNullOrEmpty() && Password.IsNullOrEmpty())
            {
                return Page();
            }

            var result = await _unitofworks.userService.Login(Username, Password);
            if(result is null)
            {
                _toastNotification.AddErrorToastMessage("Username or password is incorrect");
                return Page();
            }
            _toastNotification.AddSuccessToastMessage("Login success");
            HttpContext.Session.SetString("_userName", result.UserName);
            return RedirectToPage("/Demande/Demandes");
        }
    }
}
