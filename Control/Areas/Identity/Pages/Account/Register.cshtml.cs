using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using IdentityDataAccess;

namespace Control.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IDBContext _IDBContext;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,            
            IDBContext _context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _IDBContext = _context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            _ = (ExistDB()) ? ViewData["FlagDB"] = 0 : ViewData["FlagDB"] = 1;
      
            ViewData["FistUse"] = NotExistsUsers();
         
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
        }


        private bool ExistDB()
        {
            var sample = _IDBContext.Database.CanConnect();
            return sample;
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var x = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var bandExistsUsers = CheckQuantityOfUser();
                    if (bandExistsUsers == 1)
                    {
                        addAdministratorRole(user);
                    }
                    


                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page( "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        /// <summary>
        /// Check if exist users in the database 
        /// </summary>
        /// <returns>If not exists = 2, if exits 1 , error = 0</returns>

        private int NotExistsUsers()
        {
            try
            {
                var query = (from user in _IDBContext.Users select user).ToList();
                if(query.Count== 0)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception f)
            {
                Console.WriteLine($"Error {f.Message}");
                return 0;
                
            }
        }

        private int CheckQuantityOfUser()
        {
            try
            {
                var query = (from user in _IDBContext.Users select user).ToList();
                if (query.Count == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception f)
            {
                Console.WriteLine($"Error {f.Message}");
                return 404;

            }
        }

        private void addAdministratorRole(IdentityUser user)
        {
            try
            {
                using( var db = _IDBContext)
                {
                    var query = (from rol in db.Roles select rol).Where(R => R.Id.Equals("01")).First();

                    var row = new IdentityUserRole<string>();
                    row.RoleId = query.Id;
                    row.UserId = user.Id;
                    db.UserRoles.Add(row);
                    db.SaveChanges();
                }
            }
            catch (Exception er)
            {
                Console.WriteLine($"Error en asignacion de rol {er.Message}");               
            }
        }

    }
}
