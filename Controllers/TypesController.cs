using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourCompany.Models;
using Type = TourCompany.Models.Type;

namespace TourCompany.Controllers
{
    public class TypesController : Controller
    {
        private Context context = new Context();
        // GET: Types
        [AllowAnonymous]
        public ActionResult Index()
        {
            var items = context.Types.ToList();
            return View(items);
        }

        // GET: Types/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var item = context.Types.Where(t => t.Id == id).FirstOrDefault();
            return View(item);
        }

        // GET: Types/Create
        [Authorize(Roles ="Главный туроператор, директор")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Types/Create
        [HttpPost]
        [Authorize(Roles ="Главный туроператор, Директор")]
        public ActionResult Create(Type collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Types.Add(collection);
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

        // GET: Types/Edit/5
        [Authorize(Roles="Главный туроператор, Диретор")]
        public ActionResult Edit(int id)
        {
            var item = context.Types.Where(t => t.Id == id).FirstOrDefault();
            return View(item);
        }

        // POST: Types/Edit/5
        [HttpPost]
        [Authorize(Roles ="Главный туроператор, Директор")]
        public ActionResult Edit(Type collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid) {
                    var i = context.Types.Where(t => t.Id == collection.Id).FirstOrDefault();
                    i.Name = collection.Name;
                    i.HumansCount = collection.HumansCount;
                    i.Description = collection.Description;
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

        // GET: Types/Delete/5
        [Authorize(Roles ="Главный туроператор, директор")]
        public ActionResult Delete(int id)
        {
            var item = context.Types.Where(t => t.Id == id).FirstOrDefault();
            context.Types.Remove(item);
            context.SaveChanges();
            return View();
        }

        
        
    }
}
