using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourCompany.Models;

namespace TourCompany.Controllers
{
    public class TourController : Controller
    {
        static Context context = new Context();


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

        // GET: Tour
        [Authorize(Roles ="Главный туроператор, Туроператор, Директор")]
        public ActionResult Index()
        {
            var items = context.Tours.ToList();
            foreach (var item in items)
            {
                item.Country = context.Countries.Where(t => t.Id == item.CountryId).FirstOrDefault();
                item.Type = context.Types.Where(t => t.Id == item.TourTypeId).FirstOrDefault();
            }
            return View(items);
        }

        // GET: Tour/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            var item = context.Tours.Where(t => t.Id == id).FirstOrDefault();
            item.Type = context.Types.Where(t => t.Id == item.TourTypeId).FirstOrDefault();
            item.Country = context.Countries.Where(t => t.Id == item.CountryId).FirstOrDefault();
            return View(item);
        }

        // GET: Tour/Create
        [Authorize(Roles ="Директор, Главный туроператор")]
        public ActionResult Create()
        {
            SelectList sl1 = new SelectList(context.Countries.ToList(), "Id", "Name");
            SelectList sl2 = new SelectList(context.Types.ToList(), "Id", "Name");
            ViewBag.Countries = sl1;
            ViewBag.Types = sl2;
            return View();
        }

        [Authorize]
        public ActionResult GetAllTours()
        {
            var items = context.Tours.Where(t=>t.Status==true).ToList();
            foreach (var item in items)
            {
                item.Country = context.Countries.Where(t => t.Id == item.CountryId).FirstOrDefault();
                item.Type = context.Types.Where(t => t.Id == item.TourTypeId).FirstOrDefault();
            }
            return View(items);
        }

       // public ActionResult 
       [Authorize]

        public ActionResult Checkout(int id)
        {
            var user = UserManager.FindById(HttpContext.User.Identity.GetUserId());
            var data = context.Tours.Where(t => t.Id == id).FirstOrDefault();
            Order order = new Order();
            order.Cipher = DateTime.UtcNow + "/" + data.Name;
            order.User = new Models.User { UserName = user.UserName, Address = user.Address, Email = user.Email, Password = user.PasswordHash, PhoneNumber = user.PhoneNumber, Passport = user.Passport };
            order.DaysCount = data.CountDays;
            order.TourId = id;
            order.DateOfDispatch = data.DateOfDispatch;
            order.ReturnDate = data.DateOfDispatch.AddDays(data.CountDays);
            var currenttype = context.Tours.Where(t => t.Id == id).FirstOrDefault();
            order.HumanCount = context.Types.Where(t => t.Id == currenttype.TourTypeId).FirstOrDefault().HumansCount;
            order.Cost = currenttype.Cost * order.HumanCount;
            order.Tour = currenttype;
            SelectList pogt = new SelectList(context.ProgramTypes, "Id", "Name");
            SelectList paym = new SelectList(context.PayMethods, "Id", "Name");
            ViewBag.ProgramTypes = pogt;
            ViewBag.PayMethods = paym;
            return View(order);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {

            SelectList pogt = new SelectList(context.ProgramTypes, "Id", "Name");
            SelectList paym = new SelectList(context.PayMethods, "Id", "Name");
            ViewBag.ProgramTypes = pogt;
            ViewBag.PayMethods = paym;
            if (Validate(order))
            {
                try
                {
                    var item = context.Tours.Where(t => t.Name == order.Tour.Name).FirstOrDefault();
                    var user = UserManager.FindById(HttpContext.User.Identity.GetUserId());
                    order.Cipher = order.Cipher = DateTime.UtcNow + "/" + order.Tour.Name;
                    var  currenttype = context.Tours.Where(t => t.Name == order.Tour.Name).FirstOrDefault();
                    OrderState os = context.OrderStates.Where(t => t.Name == "Заказ ожидает обработки").FirstOrDefault();
                    order.OrderStateId = os.Id;
                    order.CustomerId = user.Id;
                    order.TourId = currenttype.Id;
                    item.Status = false;
                    context.Orders.Add(order);
                    context.SaveChanges();
                    return RedirectToAction("GetAllTours");
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var item in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type {0} in state {1} has the following validation errors", item.Entry.GetType().Name, item.Entry.State);
                        foreach (var item1 in item.ValidationErrors)
                        {
                            string msg = "property " + item1.PropertyName + "error" + item1.ErrorMessage;
                            ModelState.AddModelError("", msg);
                        }
                    }
                    return View(order);
                }
               
            }
            else
            {
                return View(order);
            }
        }
        public bool Validate(Order o)
        {
            
            return  ((o.User.UserName != null) && (o.DaysCount >= 0) && (o.HumanCount >= 0) && (o.Tour.Name != null) && (o.DateOfDispatch != null) && (o.ReturnDate != null) && (o.Description != null));
            
        }

        // POST: Tour/Create
        [HttpPost]
        [Authorize(Roles ="Главный туроператор, Директор")]
        public ActionResult Create(FakeTour collection)
        {

            try
            {
                SelectList sl1 = new SelectList(context.Countries.ToList(), "Id", "Name");
                SelectList sl2 = new SelectList(context.Types.ToList(), "Id", "Name");
                ViewBag.Countries = sl1;
                ViewBag.Types = sl2;
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Tour tour = new Tour { Name = collection.Name, TourTypeId = collection.TourTypeId, CountryId = collection.CountryId, Cost = collection.Cost, CountDays = collection.CountDays, DateOfDispatch = DateTime.Parse(collection.DateOfDispatch), Description = collection.Description };
                    context.Tours.Add(tour);
                    context.Entry(tour).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else 
                {
                    return View(collection);
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var item in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type {0} in state {1} has the following validation errors", item.Entry.GetType().Name, item.Entry.State);
                    foreach (var item1 in item.ValidationErrors)
                    {
                        string msg = "property " + item1.PropertyName + "error" + item1.ErrorMessage;
                        ModelState.AddModelError("", msg);
                    }
                }
                return View(collection);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(collection);
            }
        }

        // GET: Tour/Edit/5
        [Authorize(Roles ="Главнй туроператор, Директор")]
        public ActionResult Edit(int id)
        {
            var item = context.Tours.Where(t => t.Id == id).FirstOrDefault();
            SelectList sl1 = new SelectList(context.Types.ToList(),"Id","Name");
            SelectList sl2 = new SelectList(context.Countries.ToList(), "Id", "Name");
            ViewBag.Countries = sl2;
            ViewBag.TourTypes = sl1;
            return View(new FakeTour { Id = id, Name = item.Name, Cost = item.Cost, CountDays = item.CountDays, CountryId = item.CountryId, DateOfDispatch = item.DateOfDispatch.ToString(), Description = item.Description, TourTypeId = item.TourTypeId});
        }

        // POST: Tour/Edit/5
        [HttpPost]
        [Authorize(Roles ="Директор, Главный туроператор")]
        public ActionResult Edit(FakeTour collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var item = context.Tours.Where(t => t.Id == collection.Id).FirstOrDefault();
                    item.Name = collection.Name;
                    item.Description = collection.Description;
                    item.Cost = collection.Cost;
                    item.CountDays = collection.CountDays;
                    item.DateOfDispatch = DateTime.Parse(collection.DateOfDispatch);
                    item.CountryId = collection.CountryId;
                    item.TourTypeId = collection.TourTypeId;
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

        // GET: Tour/Delete/5
        [Authorize(Roles ="Главный туроператор, директор")]
        public ActionResult Delete(int id)
        {
            var rem = context.Tours.Where(t => t.Id == id).FirstOrDefault();
            context.Tours.Remove(rem);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
