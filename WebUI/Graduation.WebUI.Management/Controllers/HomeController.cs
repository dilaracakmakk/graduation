using graduation.Data;
using Graduation.WebUI.Management.Authorize;
using Graduation.WebUI.Management.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace graduation.WebUI.Management.Controllers
{
    public class HomeController : Controller
    {
        AuthorData _authorData;
        public HomeController(AuthorData _authorData)
        {
            this._authorData = _authorData;
        }
        [Authorize]
        public IActionResult Index()
        {

           
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel()
            {
                Username = "",
            Password = "",
            
        };

            return View(model);
        }
      
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var errors = new List<string>();
            var return_model = new LoginModel();

            if (string.IsNullOrEmpty(username)) errors.Add("kullanıcı adı boş bırakılamaz");
            if (string.IsNullOrEmpty(password)) errors.Add("şifre boş bırakılamaz");
            if(errors.Count()>0)
            {
                ViewBag.Result = new ViewModelResult(false, "hata oluştu", errors);
                return View(return_model);
            }
            var author = _authorData.GetBy(x => x.Username == username && x.Password == password && x.IsActive && !x.IsDeleted).FirstOrDefault();
            if(author ==null )
            {
                ViewBag.Result = new ViewModelResult(false, "böyle bir kullanıcı bulunamadı");
                return_model.Username = username;
                return View(return_model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,author.Fullname),
                new Claim("AuthorId", author.Id.ToString()),
                new Claim(ClaimTypes.Role, author.RoleId.ToString())
            };
            var clasim_identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var auth_properties = new AuthenticationProperties
            {
                ExpiresUtc = System.DateTimeOffset.UtcNow.AddMinutes(60),
            };
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(clasim_identity),
                auth_properties
                );
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public IActionResult _403()
        {
           
            return View();
        }
    }
}
