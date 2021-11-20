using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //new lemem cont çalıştırıp db oluşturmam için yeterli --test
            //BusinessLayer.Test test = new BusinessLayer.Test();

            //bunu başlatınca insert yapacak repository miz
            //test.InsertTest();

            //bunu başlatınca update yapacak repository miz
            //test.UpdateTest();
            //test.DeleteTest();
            //test.CommentTest();

            if (TempData["mm"] != null)
            {
                return View(TempData["mm"] as List<Note>);
            }
            
            NoteManager nm = new NoteManager();

            //bunu c# sıralar
            return View(nm.GetAllNote().OrderByDescending(x => x.ModifiedOn).ToList());
            //bunu SQL sıralar
            //return View(nm.GetAllNoteQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();
            return View("Index", nm.GetAllNote().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.LoginUser(model);

                if (res.Errors.Count > 0)
                {
                    //hata mesajlarını rahat kontrol edip aksiyon alınabilir
                    if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        //misal aktive etmesi için link verilebilir
                        ViewBag.SetLink = "E-posta gönder";
                    }

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }

                Session["login"] = res.Result;      //session
                return RedirectToAction("Index");   //yönlendirme
            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //RegisterViewModel deki tanımlara uygun mu diye
            if (ModelState.IsValid)
            {
                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }


                //EvernoteUser user = null;

                //try
                //{
                //  user = eum.RegisterUser(model);
                //}
                //catch (Exception e)
                //{
                //    ModelState.AddModelError("", e.Message);
                //}

                //if(model.Username == "aaa")
                //{
                //    ModelState.AddModelError("", "Kullanıcı Adı Kullanılıyor :/");
                //}

                //if (model.EMail == "aa@aa.com")
                //{
                //    ModelState.AddModelError("", "E-posta Adresi Kullanılıyor :/");
                //}

                //model elemanlarında hata var ise viev a geri dön
                //foreach (var item in ModelState)
                //{
                //    if(item.Value.Errors.Count > 0)
                //    {
                //        return View(model);
                //    }
                //}

                //if(user == null)
                //{
                //    return View(model);
                //}



                return RedirectToAction("RegisterOk");
            }

            //uygun değilse direk gönderdik hataları gönderecek
            return View(model);
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult UserActivate(Guid activate_id)
        {
            //kullanıcı aktivasyonu
            return View();
        }

        public ActionResult LogOut()
        {
            return View();
        }
    }
}