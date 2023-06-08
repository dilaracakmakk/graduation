using graduation.Data;
using graduation.WebUI.Site.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace graduation.WebUI.Site.Controllers
{
    public class TagController : Controller
    {

        TagData _tagData;
        ContentData _contentData;
        ContentTagData _contentTagData;


        public TagController(TagData _tagData, ContentData _contentData, ContentTagData _contentTagData)
        {
            this._tagData = _tagData;
            this._contentData = _contentData;
            this._contentTagData = _contentTagData;
        }

        public IActionResult Index(string slug, int page = 1)
        {
            var tag = _tagData.GetBy(x => x.Slug == slug && x.IsActive && !x.IsDeleted).FirstOrDefault();
            if (tag == null)
                return RedirectToAction("Index", "Home", new { q = "etiket-bulunamadı" });

            var category_content_ids = _contentTagData.GetByPage(x => x.TagId == tag.Id, page, 10)
                    .Select(x => x.ContentId).ToList();
            var total_data = _contentTagData.GetCount(x => x.TagId == tag.Id);
            double c = double.Parse(total_data.ToString()) / 10;
            c = Math.Ceiling(c);

            var contents = _contentData.GetContentsByIds(category_content_ids, 10);

            var model = new TagViewModel()
            {
                Tag = tag,
                Contents = contents,
                CurrentPage = page,
                TotalData = total_data,
                TotalPage = c,

            };
            return View(model);
        }
    }
}

    

