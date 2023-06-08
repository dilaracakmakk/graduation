﻿using graduation.Data;
using graduation.WebUI.Site.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;


namespace graduation.WebUI.Site.Controllers
{
    public class CategoryController : Controller
    {
       
            CategoryData _categoryData;
            ContentData _contentData;
            ContentCategoryData _contentCategoryData;

        
    public CategoryController(CategoryData _categoryData, ContentData _contentData, ContentCategoryData _contentCategoryData)
    {
        this._categoryData = _categoryData;
        this._contentData = _contentData;
        this._contentCategoryData = _contentCategoryData;
    }

    public IActionResult Index(string slug, int page=1)
        {
        var category = _categoryData.GetBy(x => x.Slug == slug && x.IsActive && !x.IsDelete).FirstOrDefault();
        if (category == null)
            return RedirectToAction("Index", "Home", new { q = "kategori-bulunamadı" });

        var category_content_ids = _contentCategoryData.GetByPage(x => x.CategoryId == category.Id, page,10)
                .Select(x => x.ContentId).ToList();
            var total_data = _contentCategoryData.GetCount(x => x.CategoryId == category.Id);
            double c = double.Parse(total_data.ToString()) / 10;
            c = Math.Ceiling(c);

            var contents = _contentData.GetContentsByIds(category_content_ids, 10);

            var model = new CategoryViewModel()
        {
                Category = category,
                Contents = contents,
                CurrentPage = page,
                TotalData = total_data,
                TotalPage = c,

        };
        return View(model);
    }
    }
}
