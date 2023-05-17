using graduation.Data;
using Graduation.WebUI.Management.Helpers;
using Graduation.WebUI.Management.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Graduation.WebUI.Management.Controllers
{
    public class AuthorController : Controller
    {
        AuthorData _authorData;
        public AuthorController(AuthorData _authorData)

        {
            this._authorData = _authorData;

        }
         [HttpGet]
        
        public IActionResult Index()
        {
            var authors = _authorData.GetBy(x => !x.IsDeleted);
            return View(authors);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var author = new graduation.Model.Author();
            return View(author);
        }
        [HttpPost]
        public IActionResult Add(graduation.Model.Author author)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(author.Fullname)) errors.Add("Ad Soyad boş bırakılamaz");
            if (string.IsNullOrEmpty(author.Username)) errors.Add("Kullanıcı Adı boş bırakılamaz");
            if (string.IsNullOrEmpty(author.Password)) errors.Add("Şifre boş bırakılamaz");

            if (errors.Count() > 0)
            {
                ViewBag.Result = new ViewModelResult(false, "hata oluştu", errors);
                return View(author);
            }

            var operationResult = _authorData.Insert(author);
            if(operationResult.IsSucceed)
            {
               
                ViewBag.Result = new ViewModelResult(true, "yeni yazar eklendi");
                return View(new graduation.Model.Author());
                }
            ViewBag.Result = new ViewModelResult(false, operationResult.Message);
            return View(author);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = _authorData.GetByKey(id);
            if (author == null)
                return RedirectToAction("Index", "Author", new { q = "kullanici-bulunamadi" });

          
            return View(author);
        }
        [HttpPost]
        public IActionResult Edit(graduation.Model.Author author)
        {
            var modelInDb = _authorData.GetByKey(author.Id);

            var errors = new List<string>();
            if (string.IsNullOrEmpty(author.Fullname)) errors.Add("Ad Soyad boş bırakılamaz");
            if (string.IsNullOrEmpty(author.Username)) errors.Add("Kullanıcı Adı boş bırakılamaz");
            if (string.IsNullOrEmpty(author.Password)) errors.Add("Şifre boş bırakılamaz");
            if (errors.Count() > 0)
            {
               
                ViewBag.Result = new ViewModelResult(false, "hata oluştu", errors);
                return View(author);

            }
            var exist_content = _authorData.GetBy(x => x.Username == author.Username && !x.IsDeleted && x.Id != author.Id).FirstOrDefault();
            if(exist_content != null)
            {
                ViewBag.Result = new ViewModelResult(false, "bu kullanıcı adı zaten kullanılıyor");
                return View(author);

            }
            modelInDb.Fullname = author.Fullname;
            modelInDb.IsActive = author.IsActive;
            modelInDb.Mail = author.Mail;
            modelInDb.Password = author.Password;
            modelInDb.Username = author.Username;




            var operationResult = _authorData.Update(modelInDb);
            if(operationResult.IsSucceed)
            {

                ViewBag.Result = new ViewModelResult(true, "yazar güncellendi");
                return View(modelInDb);
            }
            ViewBag.Result = new ViewModelResult(false, operationResult.Message);
            return View(author);

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var author = _authorData.GetByKey(id);
            if (author == null)
                return RedirectToAction("Index", "Author", new { q = "yazar-bulunamadi" });


            author.IsDeleted = true;
            var operationResult = _authorData.Update(author);
            if (operationResult.IsSucceed)
                return RedirectToAction("Index", "Author", new { q = "yazar-silindi" });
            else
                return RedirectToAction("Index", "Author", new { q = "yazar-silinemedi" });

        }
       
    }
    }

