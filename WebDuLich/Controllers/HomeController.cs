using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebDuLich.Models.Entities;

namespace WebDuLich.Controllers
{
    public class HomeController : Controller
    {
        WebDuLich_ConText db = new WebDuLich_ConText();
        public ActionResult Index()
        {
            return View();
        }
        /*public ActionResult RenderNav()
        {
            WebDuLich_ConText db = new WebDuLich_ConText();
            List<NavItem> listNav = db.NavItems.ToList();
            return PartialView("Web_Header", listNav);
        }*/
        
        public ActionResult RenderCountry()
        {
            List<Country> listItem = db.Countries.ToList();
            return PartialView("Web_Header", listItem);
        }
        public ActionResult RenderCountry2()
        {
            List<Country> listItem = db.Countries.ToList();
            return PartialView("Beijing_Header", listItem);
        }
        public ActionResult RenderInfo()
        {
            WebDuLich_ConText db = new WebDuLich_ConText();
            List<Info> listInfo = db.Infoes.ToList();
            return PartialView("Web_Main", listInfo);
        }
        public ActionResult RenderProductByNameCountry(int CatId)
        {

            List<Info> listInfo = db.Infoes.Where(h => h.CountryId == CatId).ToList();
            return PartialView("Web_Main", listInfo);
        }

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer _user)
        {
            WebDuLich_ConText db = new WebDuLich_ConText();

            if (ModelState.IsValid)
            {

                var check = db.Customers.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Roles = "user";
                    _user.Password = GetMD5(_user.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Customers.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            WebDuLich_ConText db = new WebDuLich_ConText();
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = db.Customers.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FullName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    Session["Roles"] = data.FirstOrDefault().Roles;
                    //ViewBag.h = data.FirstOrDefault().Roles;
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index");
        }

        //public ActionResult Search(string Name = "")
        //{
        //    WebDuLich_ConText db = new WebDuLich_ConText();
        //    List<Info> listInfo = db.Infoes.Where(p => p.Country.Contains(Name)).ToList();
        //    return PartialView("Web_Main", listInfo);
        //}


        public ActionResult Booking()
        {

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FullName");
            ViewBag.InfoId = new SelectList(db.Infoes, "Id", "Name");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Booking(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return View();
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FullName", order.CustomerId);
            ViewBag.InfoId = new SelectList(db.Infoes, "Id", "Name", order.InfoId);
            return View(order);
        }
        /*public ActionResult ViewPost(string title)
        {

            if (!String.IsNullOrWhiteSpace(title))
            {
                return View(new Info
                {
                    Id = p.Id,
                    Name = p.Name,
                    Country = p.Country,
                    UnitPrice = p.UnitPrice,
                    Image = p.Image,

                });
            }
            return HttpNotFound();
        }*/
        public ActionResult Beijing_Main(int Id)
        {
            var objInfo = db.Infoes.Where(n => n.Id == Id).FirstOrDefault();
            return View(objInfo);
        }
        
    }
    

}
