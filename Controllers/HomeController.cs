using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TourCompany.Models;

namespace TourCompany.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<ApplicationUser> UserManager
        {
            get
            {
                return getum();
                UserManager<ApplicationUser> getum()
                {
                    var contex = new ApplicationDbContext();
                    var userstore = new UserStore<ApplicationUser>(contex);
                    var d = new UserManager<ApplicationUser>(userstore);
                    return d;
                }
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            

            return View();
        }
        
        public ActionResult Contact()
        {
            Contact con = new Contact();
            var user = UserManager.FindById(HttpContext.User.Identity.GetUserId());

            if (user != null)
            {
                con.Email = user.Email;
                con.Name = user.UserName;
            }
            List<SelectListItem> reasons = new List<SelectListItem>()
            {
                new SelectListItem {Text = "Проблема с авторизацией/ регистрацией", Value = "1"},
                new SelectListItem{ Text = "Проблема при оформлении заказа", Value = "2"},
                new SelectListItem {Text = "Проблема с оплатой", Value = "3"},
                new SelectListItem {Text = "Другая проблема", Value = "4"},
            };
            ViewBag.Reasons = reasons;
            return View(con);
        }
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
           
            if (ModelState.IsValid)
            {
                contact.Reason = Request.Form["Reasons"];
                SendEmail(contact.Email, contact.Name, contact.Reason);
                return View();
            }
            else
            {
                return View(contact);
            }
        }
        private void SendEmail(string email, string username, string msg)
        {
            MailAddress from = new MailAddress("calling.nanija@gmail.com", "Admin");
       
            MailAddress to = new MailAddress(email);
         
            MailMessage m = new MailMessage(from, to);
         
            m.Subject = "Поддержка";
       
            m.Body = "<h2>Ззравствуйте, у пользователя с Email "+email+" возникла проблема "+msg+"</h2>";
          
            m.IsBodyHtml = true;
          
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
      
            smtp.Credentials = new NetworkCredential("calling.znanija.com", "forsag5school46");
            smtp.EnableSsl = true;
            smtp.Send(m);
            Console.Read();
        }
    }
}