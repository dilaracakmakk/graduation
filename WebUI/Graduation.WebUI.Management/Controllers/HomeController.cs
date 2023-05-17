using graduation.Data;
using Graduation.WebUI.Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace graduation.WebUI.Management.Controllers
{
    public class HomeController : Controller
    {
      
        ContentData _contentData;
        TagData _tagData;
        public HomeController(ContentData _contentData,TagData _tagData)
        {
            this._contentData = _contentData;
            this._tagData = _tagData;
        }
        public IActionResult Index()
        {

            var content = _contentData.GetByPage(x => !x.IsDeleted, 1, 10, "Publish Date", true);
            var tag = _tagData.GetByPage(x => !x.IsDeleted, 1, 10, "Publish Date", true);

            var model = new HomeViewModel()
            {
                Contents = content,
                Tags = tag,
            };
            return View(model);
        }
    }
}
