using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using graduation.Model;

namespace Graduation.WebUI.Management.Models
{
    public class ContentEditViewModel : Controller
    {

        public ContentEditViewModel()
        {
            Content = new graduation.Model.Content();
            Categories = new List<graduation.Model.ContentCategory>();
            Tags = new List<graduation.Model.ContentTag>();
        }
        public graduation.Model.Content Content { get; set; }
        public List<graduation.Model.ContentCategory> Categories { get; set; }

        public List<graduation.Model.ContentTag> Tags { get; set; }

        public List<string> TagNames { get; set; }

    }
}
