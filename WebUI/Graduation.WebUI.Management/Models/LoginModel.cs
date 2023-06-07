
using graduation.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Graduation.WebUI.Management.Models
{
    public class LoginModel : Controller
    {

        public string Username { get; set; }
        public string Password { get; set; }

    }
}
