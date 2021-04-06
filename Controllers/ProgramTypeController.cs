using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourCompany.Models;

namespace TourCompany.Controllers
{
    
    public class ProgramTypeController : Controller
    {
        static Context context = new Context();
        // GET: ProgramType
        [AllowAnonymous]
        public ActionResult Index()
        {
            var items = context.ProgramTypes.ToList();
            return View(items);
        }

        // GET: ProgramType/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var item = context.ProgramTypes.Where(t => t.Id == id).FirstOrDefault();
            return View(item);
        }

        // GET: ProgramType/Create
        [Authorize(Roles ="Главный туроператор, Директор")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProgramType/Create
        [HttpPost]
        [Authorize(Roles="Главный туропрератор, директор")]
        public ActionResult Create(ProgramType collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.ProgramTypes.Add(collection);
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

        // GET: ProgramType/Edit/5
        [Authorize(Roles ="Главный туроператор, Директор")]
        public ActionResult Edit(int id)
        {
            var item = context.ProgramTypes.Where(t => t.Id == id).FirstOrDefault();
            return View(item);
        }

        // POST: ProgramType/Edit/5
        [HttpPost]
        public ActionResult Edit(ProgramType collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var item = context.ProgramTypes.Where(t => t.Id == collection.Id).FirstOrDefault();
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

        // GET: ProgramType/Delete/5
        [Authorize(Roles ="Главный туроператор, директор")]
        public ActionResult Delete(int id)
        {
            var item = context.ProgramTypes.Where(i => i.Id == id).FirstOrDefault();
            context.ProgramTypes.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }   
    }
}
