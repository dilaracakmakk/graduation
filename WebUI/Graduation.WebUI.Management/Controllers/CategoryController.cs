using graduation.Data;
using Graduation.WebUI.Management.Helpers;
using Graduation.WebUI.Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Graduation.WebUI.Management.Controllers
{
    public class CategoryController : Controller
    {
        CategoryData _categoryData;
        public CategoryController(CategoryData categoryData)
        {
            _categoryData = categoryData;
        }

        [HttpGet]

        public IActionResult Index()
        {
            var categories = _categoryData.GetBy(x => !x.IsDelete);
           
            return View(categories);
        }
        [HttpGet]

        public IActionResult Add()
        {
            var category = new graduation.Model.Category()
            {
                IsActive = false,
                IsDelete = false,
                MetaDescription = "",
                MetaTitle = "",
                Name = "",
                Slug = "",
            };

            return View(category);
        }

        [HttpPost]

        public IActionResult Add(graduation.Model.Category category)
        {


            var errors = new List<string>();

            if (string.IsNullOrEmpty(category.Name)) errors.Add("kategori adı boş olamaz");
            if (string.IsNullOrEmpty(category.Slug)) errors.Add("kategori slug boş olamaz");
            if (errors.Count() > 0)
            {
                ViewBag.Result =new ViewModelResult(false, "Hata Oluştu", errors);
                return View(category);

            }


            var operationResult = _categoryData.Insert(category);
            if (operationResult.IsSucceed)
            {
                ViewBag.Result = new ViewModelResult(true, "Yeni Kategori Eklendi");
                return View(new graduation.Model.Category());
            }

            ViewBag.Result = new ViewModelResult(false, operationResult.Message);
            return View(category);
        }
        [HttpGet]

        public IActionResult Edit(int id)
        {
            var category = _categoryData.GetByKey(id);
            if (category == null)
                return RedirectToAction("Index", "Home", new { q = "kategori-bulunamadı" });


            return View(category);
        }

        [HttpPost]

        public IActionResult Edit(graduation.Model.Category category)
        {

            var errors = new List<string>();
            var modelInDb = _categoryData.GetByKey(category.Id);

            if (string.IsNullOrEmpty(category.Name)) errors.Add("kategori adı boş olamaz");
            if (string.IsNullOrEmpty(category.Slug)) errors.Add("kategori slug boş olamaz");
            if (errors.Count() > 0)

            {
                ViewBag.Result = new ViewModelResult(false, "Hata Oluştu", errors);


                return View(category);

            }
            category.Slug = category.Slug.ToSlug();

            var exist_category = _categoryData.GetBy(x => x.Slug == category.Slug && !x.IsDelete && x.Id != category.Id).FirstOrDefault();
            if (exist_category != null)
            {
                ViewBag.Result = new ViewModelResult(false, "Zaten kayıtlı");


                return View(category);
            }

            modelInDb.Name = category.Name;
            modelInDb.MetaTitle = category.MetaTitle;
            modelInDb.MetaDescription = category.MetaDescription;
            modelInDb.IsActive = category.IsActive;
            modelInDb.Slug = category.Slug.ToSlug();

            var operationResult = _categoryData.Update(modelInDb);
            if (operationResult.IsSucceed)
            {
                ViewBag.Result = new ViewModelResult(true, "Kategori Güncellendi");

                return View(category);
            }

            ViewBag.Result = new ViewModelResult(false, operationResult.Message);

            return View(category);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _categoryData.GetByKey(id);
            if (category == null)
                return RedirectToAction("Index", "Category", new { q = "kategori bulunamadı" });

            category.IsDelete = true;
            var operationResult = _categoryData.Update(category);
            if (operationResult.IsSucceed)
                return RedirectToAction("Index", "Category", new { q = "kategori silindi" });
            else
                return RedirectToAction("Index", "Category", new { q = "kategori silinemedi" });

        }
    }
}
