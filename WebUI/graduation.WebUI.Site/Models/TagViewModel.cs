
using graduation.Model;
using System.Collections.Generic;

namespace graduation.WebUI.Site.Models
{
    public class TagViewModel
    {
        public TagViewModel()
        {
            Tag = new Model.Tag();
            Contents = new List<Model.Content>();
        }
        public Model.Tag Tag { get; set; }
        public List<Model.Content> Contents { get; set; }

        public int CurrentPage { get; set;  }
        public int TotalData { get; set;  }
        public double TotalPage { get; set; }
    }
}
