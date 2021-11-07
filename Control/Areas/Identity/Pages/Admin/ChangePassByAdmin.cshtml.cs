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

namespace Control.Areas.Identity.Pages.Admin
{
    public class ChangePassByAdminModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IDBContext _iDBContext;

        public ChangePassByAdminModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager,
            IDBContext _iDBContext )
        {
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._logger = _logger;
            this._iDBContext = _iDBContext;
        }



        [Route("/Identity/Admin/ChangePassByAdmin/{id}")]
        public async Task<IActionResult> OnGetAsync(string id="")
        {
            IdentityUser user = new IdentityUser();
            using (var db = new IDBContext())
            {
                var query = (from us in db.Users select us).FirstOrDefault(U=>U.Id== id);
                user = query;
            }

            ViewData["User"] = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id = "")
        {
            ViewData["sample"] = id;
            return Page();
        }
    }
}
