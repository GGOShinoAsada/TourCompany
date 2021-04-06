using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourCompany.Models;

namespace TourCompany.Controllers
{
    public class PayMethodsController : Controller
    {
        static Context context = new Context();
        // GET: PayMethods
        [AllowAnonymous]
        public ActionResult Index()
        {
            var items = context.PayMethods.ToList();
            return View(items);
        }

        // GET: PayMethods/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var item = context.PayMethods.Where(t => t.Id == id).FirstOrDefault();
            return View(item);
        }

        // GET: PayMethods/Create
        [Authorize(Roles="Главный туроператор, Директор")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayMethods/Create
        [HttpPost]
        [Authorize(Roles ="Главный туроператор, Директор")]
        public ActionResult Create(PayMethod collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    context.PayMethods.Add(collection);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(collection);
                }
             
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: PayMethods/Edit/5
        [Authorize(Roles ="Главный туроператор, Директор")]
        public ActionResult Edit(int id)
        {
            var item = context.PayMethods.Where(t => t.Id == id).FirstOrDefault();
            return View(item);
        }

        // POST: PayMethods/Edit/5
        [HttpPost]
        public ActionResult Edit(PayMethod collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var item = context.PayMethods.Where(t => t.Id == collection.Id).FirstOrDefault();
                    item.Name = collection.Name;
                    item.Description = collection.Description;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(collection);
                }               
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: PayMethods/Delete/5
        [Authorize(Roles ="Главный туроператор, Директор")]
        public ActionResult Delete(int id)
        {
            var item = context.PayMethods.Where(t => t.Id == id).FirstOrDefault();
            context.PayMethods.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: PayMethods/Delete/5
      
    }
}
