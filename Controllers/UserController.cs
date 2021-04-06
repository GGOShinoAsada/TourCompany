using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TourCompany.Models;

namespace TourCompany.Controllers
{
    public class UserController : Controller
    {
        public UserManager<ApplicationUser> UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
  
        private ApplicationSignInManager _signInManager;

        // GET: User
        public ActionResult Index()
        {
            var items = UserManager.Users.ToList();
            return View(items);
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            var item = UserManager.Users.Where(t => t.Id == id).FirstOrDefault();
            return View(new User { UserName = item.UserName, Passport = item.Passport, Address = item.Address, Email = item.Email, Password = item.PasswordHash, PhoneNumber = item.PhoneNumber});
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(RegisterViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser { Email = collection.Email, UserName = collection.UserName, Address = collection.Address, PhoneNumber = collection.PhoneNumber, Passport = collection.Passport };
                    var result = UserManager.Create(user);
                    if (result.Succeeded)
                    {
                        // var t = context.Users.Where(t1 => t1.Email == collection.Email).FirstOrDefault();
                        // var tresult = await _signInManager.PasswordSignInAsync(collection.UserName, collection.Password, false, false);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrors(result);
                        return View(collection);
                    }
                }
                else
                {
                    return View(collection);
                }
           
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {

            var item = UserManager.Users.Where(t=>t.Id==id).FirstOrDefault();
            return View(new EditViewModel { UserName = item.UserName, Passport = item.Passport, Address = item.Address, Email = item.Email, Password = item.PasswordHash, PhoneNuber = item.PhoneNumber });
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                     
                        var data = UserManager.Users.Where(t => t.Id == model.Id).FirstOrDefault();
                        data.Email = model.Email;
                        data.PhoneNumber = model.PhoneNuber;
                        data.Passport = model.Passport;
                        data.Address = model.Address;
                        data.UserName = model.UserName;
                        data.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                        var result = UserManager.Update(data);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            AddErrors(result);
                            return View(model);
                        }
                    }
                    
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View(model);
            }
        }
                 

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            var item = UserManager.FindById(id);
            UserManager.Delete(item);
            return RedirectToAction("Index");
        }

    
        public void AddErrors(IdentityResult r)
        {
            foreach (string err in r.Errors)
            {
                ModelState.AddModelError("", err);
            }
        }
    }
}
