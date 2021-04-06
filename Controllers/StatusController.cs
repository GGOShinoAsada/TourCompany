using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourCompany.Models;

namespace TourCompany.Controllers
{
    public class StatusController : Controller
    {
        static Context context = new Context();
        // GET: Status
        [AllowAnonymous]
        public ActionResult Index()
        {
            var items = context.OrderStates.ToList();
            return View(items);
        }

        // GET: Status/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var item = context.OrderStates.Where(t => t.Id == id).FirstOrDefault();
            return View(item);
        }

        // GET: Status/Create
        [Authorize(Roles ="Главный туроператор, Директор")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        [HttpPost]
        [Authorize(Roles="Главный туроператор, Директор")]
        public ActionResult Create(OrderState collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    context.OrderStates.Add(collection);
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

        // GET: Status/Edit/5
        [Authorize(Roles ="Главный туроппрератор, Директор")]
        public ActionResult Edit(int id)
        {
            var item = context.OrderStates.Where(t => t.Id == id).FirstOrDefault();
            return View(item);
        }

        // POST: Status/Edit/5
        [HttpPost]
        [Authorize(Roles ="Главный туроператор, Директор")]
        public ActionResult Edit(OrderState collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var item = context.OrderStates.Where(t => t.Id == collection.Id).FirstOrDefault();
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

        // GET: Status/Delete/5
        [Authorize(Roles ="Главный туроператор, Директор")]
        public ActionResult Delete(int id)
        {
            var item = context.OrderStates.Where(t => t.Id == id).FirstOrDefault();
            context.OrderStates.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Status/Delete/5
        
    }
}
