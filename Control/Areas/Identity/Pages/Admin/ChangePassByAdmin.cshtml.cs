using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Control.Areas.Identity.Pages.Account;
using Microsoft.Extensions.Logging;
using IdentityDataAccess;
using System.ComponentModel.DataAnnotations;

namespace Control.Areas.Identity.Pages.Admin
{
    public class ChangePassByAdminModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IDBContext _iDBContext;


        public class InputModel
        {
            [DataType(DataType.Text)]
            [Display(Name = "User Id")]
            public string idUser { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public ChangePassByAdminModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager,
            IDBContext _iDBContext )
        {
            this._signInManager = signInManager;
            this._logger = logger;
            this._userManager = userManager;
            this._iDBContext = _iDBContext;
        }



        private bool  ChangePassword(IdentityUser user, string _passwordHash)
        {
            try
            {
                using (var db = new IDBContext())
                {
                    user.PasswordHash = _passwordHash;
                    db.Users.Update(user);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception g ) 
            {

                Console.WriteLine($"Error {g.Message}");
                return false;
            }  
        }


        private IdentityUser GetUser(string _id = "")
        {
            try
            {
                using (var db = new IDBContext())
                {
                    var query = (from us in db.Users select us).FirstOrDefault(U => U.Id == _id);
                    return query;
                }
            }
            catch (Exception f)
            {
                Console.WriteLine($"Error: {f.Message}");
                return null;
            }
        }

      
        public IActionResult OnGetAsync(string id="")
        {
            IdentityUser user = new IdentityUser();
            user = GetUser(id);
            ViewData["User"] = user;
            return Page();
        }

        public IActionResult OnPostAsync(string id = "", string password="",string cpassword = "")
        {
            bool BandSuccess = false ;
            var _user = GetUser(id);
            string error = "";

            if( (password == null) || (cpassword == null))
            {

                error = "Verifica que hayas introducido información";
            }
            else
            {
                if (!(password.Equals(cpassword)))
                {
                    error = "La Contraseña no es identica, verifica los datos";
                }
                else
                {

                    var tmp = _userManager.PasswordHasher;
                    var tmpHasher = tmp.HashPassword(_user, password);
                    BandSuccess = ChangePassword(_user, tmpHasher);
                    if (BandSuccess)
                    {
                        error = "Contraseña cambiada";
                    }
                }
            }                               
            ViewData["User"] = _user;
            ViewData["error"] = error;
            return Page();
        }
    }
}
