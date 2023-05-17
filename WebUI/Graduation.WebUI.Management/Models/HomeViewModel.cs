
using graduation.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Graduation.WebUI.Management.Models
{
    public class HomeViewModel : Controller
    {
        public HomeViewModel()
        {
            Tags = new List<graduation.Model.Tag>();
            Categories = new List<graduation.Model.Category>();
            Contents = new List<graduation.Model.Content>();
        }

        public List<graduation.Model.Tag>Tags { get; set; }
        public List<graduation.Model.Category>Categories{ get; set; }
        public List<graduation.Model.Content>Contents { get; set; }


    }
}
