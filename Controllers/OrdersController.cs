using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourCompany.Models;

namespace TourCompany.Controllers
{
    public class OrdersController : Controller
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

        // GET: Orders
        [Authorize(Roles ="Главный туроператор, Туроператор, Директор")]
        public ActionResult Index()
        {
            var items = context.Orders.ToList();
            foreach (var item in items)
            {
                item.OrderState = context.OrderStates.Where(t => t.Id == item.OrderStateId).FirstOrDefault();
                item.PayMethod = context.PayMethods.Where(t => t.Id == item.PayMethodId).FirstOrDefault();
                item.ProgramType = context.ProgramTypes.Where(t => t.Id == item.ProgramTypeId).FirstOrDefault();
                item.Tour = context.Tours.Where(t => t.Id == item.TourId).FirstOrDefault();
                var data = UserManager.FindById(item.CustomerId.ToString());
                TourCompany.Models.User user = new User{ UserName = data.UserName, Address = data.Address, Email = data.Email, Passport = data.Passport, PhoneNumber = data.PhoneNumber, Password = data.PasswordHash};
                item.User = user;
            }

            return View(items);
        }

        [Authorize(Roles="Туроператор, Главный туроператор, Директор")]
        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            var item = context.Orders.Where(t => t.Id == id).FirstOrDefault();
            item.Tour = context.Tours.Where(t => t.Id == item.TourId).FirstOrDefault();
            item.OrderState = context.OrderStates.Where(t=>t.Id==item.OrderStateId).FirstOrDefault();
            item.PayMethod = context.PayMethods.Where(t => t.Id == item.PayMethodId).FirstOrDefault();
            item.ProgramType = context.ProgramTypes.Where(t => t.Id == item.ProgramTypeId).FirstOrDefault();
            return View(item);
        }

    

        // GET: Orders/Edit/5
        [Authorize(Roles ="Главный туроператор, Туроператор, Директор")]
        public ActionResult Edit(int id)
        {

            var item = context.Orders.Where(t => t.Id == id).FirstOrDefault();
            item.Tour = context.Tours.Where(t => t.Id == item.TourId).FirstOrDefault();
            item.OrderState = context.OrderStates.Where(t => t.Id == item.OrderStateId).FirstOrDefault();
            item.PayMethod = context.PayMethods.Where(t => t.Id == item.PayMethodId).FirstOrDefault();
            item.ProgramType = context.ProgramTypes.Where(t => t.Id == item.ProgramTypeId).FirstOrDefault();
            var data = UserManager.FindById(item.CustomerId.ToString());
            TourCompany.Models.User user = new User { UserName = data.UserName, Address = data.Address, Email = data.Email, Passport = data.Passport, PhoneNumber = data.PhoneNumber, Password = data.PasswordHash };
            item.User = user;
            SelectList sl1 = new SelectList(context.OrderStates.ToList(), "Id", "Name",item.OrderStateId);
            SelectList sl2 = new SelectList(context.PayMethods.ToList(), "Id", "Name",item.PayMethodId);
            SelectList sl3 = new SelectList(context.ProgramTypes.ToList(), "Id", "Name",item.ProgramTypeId);
            ViewBag.OrderStates = sl1;
            ViewBag.PayMethods = sl2;
            ViewBag.OrderStates = sl3;
            return View(item);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [Authorize(Roles="Главный туроператор, Туроператор, Директор")]
        public ActionResult Edit(Order order)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var item = context.Orders.Where(y => y.Id == order.Id).FirstOrDefault();
                    item.HumanCount = order.HumanCount;
                    item.CustomerId = order.CustomerId;
                    item.DateOfDispatch = order.DateOfDispatch;
                    item.DaysCount = order.DaysCount;
                    item.Description = order.Description;
                    item.OrderStateId = order.OrderStateId;
                    item.PayMethodId = order.PayMethodId;
                    item.ProgramTypeId = order.ProgramTypeId;
                    item.TourId = order.TourId;
                    context.SaveChanges();
                }
                else
                {
                    return View(order);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }       
       
    }
}
