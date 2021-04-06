using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourCompany.Models;

namespace TourCompany.Controllers
{
    public class CountriesController : Controller
    {
        static Context context = new Context();
        [AllowAnonymous]
        // GET: Countries
        public ActionResult Index()
        {
            var items = context.Countries.ToList();
            return View(items);
        }
        [AllowAnonymous]
        // GET: Countries/Details/5
        public ActionResult Details(int id)
        {
            var item = context.Countries.Where(t => t.Id == id).FirstOrDefault();
            return View(item);
        }

        // GET: Countries/Create
        [Authorize(Roles = "Главный туроператор, Директор")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [Authorize(Roles = "Главный туроператор, Директор")]
        public ActionResult Create(Country collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Countries.Add(collection);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(collection);
                }
                // TODO: Add insert logic here

                
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Countries/Edit/5
        [Authorize(Roles = "Главный туроператор, Директор")]
        public ActionResult Edit(int id)
        {
            var item = context.Countries.Where(t => t.Id == id).FirstOrDefault();
            return View(item);
        }

        // POST: Countries/Edit/5
        [HttpPost]
        [Authorize(Roles = "Главный туроператор, Директор")]
        public ActionResult Edit(Country collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var item = context.Countries.Where(t => t.Id == collection.Id).FirstOrDefault();
                    item.Name = collection.Name;
                    item.Description = collection.Description;
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

        // GET: Countries/Delete/5
        [Authorize(Roles = "Главный туроператор, Директор")]
        public ActionResult Delete(int id)
        {
            var item = context.Countries.Where(t => t.Id == id).FirstOrDefault();
            context.Countries.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }      
    }
}
