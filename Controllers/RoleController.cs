using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourCompany.Models;
using static TourCompany.Models.Helper;

namespace TourCompany.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role

      
        public RoleManager<ApplicationRole> RoleManager
        {
            get
            {
                return getrm();
                RoleManager<ApplicationRole> getrm()
                {
                    var context = new ApplicationDbContext();
                    var rolestore = new RoleStore<ApplicationRole>(context);
                    var d = new RoleManager<ApplicationRole>(rolestore);
                    return d;
                }
            }
        }
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
        [Authorize(Roles = "Администратор, Директор")]
        public ActionResult Index()
        {
            var roles = RoleManager.Roles;
            return View(roles.ToList());
        }
        [Authorize(Roles = "Администратор, Директор")]
        // GET: Role/Details/5
        public ActionResult Details(string id)
        {

            var t = RoleManager.FindById(id);
            return View(new RoleEdit { Id = t.Id, Name = t.Name, Description = t.Description });
        }
       // [Authorize(Roles = "Administrator, Director")]
        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Администратор, Директор")]
        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(RoleModel collection)
        {
            try
            {
                var r = new ApplicationRole { Name = collection.Name, Description = collection.Description };
                if (r != null)
                {

                    var col = RoleManager.Create(r);
                    if (col.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(col);
                        return View();
                    }
                }
                else
                {
                    return View();
                }

                // TODO: Add insert logic here
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Администратор, Директор")]
        // GET: Role/Edit/5
        public ActionResult Edit(string id)
        {
            var role = RoleManager.FindById(id);
            string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();
            IEnumerable<ApplicationUser> members = UserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            List<ApplicationUser> unmembers = new List<ApplicationUser>();
            REModificated model = new REModificated();
            model.Role = role;

            if (unmembers.ToList().Count() == 0)
            {
                unmembers = UserManager.Users.ToList();
            }
            else
            {
                foreach (var t in UserManager.Users.ToList())
                {
                    foreach (var d in unmembers)
                    {
                        if (!d.Id.Equals(t.Id))
                        {
                            unmembers.Add(t);
                            break;

                        }
                    }
                }
            }

            foreach (var d in members)
            {
                model.Members.Add(new FakeUser { Id = d.Id, Name = d.UserName });
            }
            foreach (var t in unmembers)
            {
                model.UnMembers.Add(new FakeUser { Id = t.Id, Name = t.UserName });
            }
            return View(model);
        }


        // POST: Role/Edit/5
        [HttpPost]
        [Authorize(Roles = "Администратор, Директор")]
        public ActionResult Edit(REPost model)
        {
            try
            {
                // TODO: Add update logic here
                IdentityResult r;

                if (ModelState.IsValid)
                {
                    foreach (string uid in model.IdsToAdd ?? new string[] { })
                    {
                        r = UserManager.AddToRole(uid, model.rolename);
                        if (!r.Succeeded)
                        {
                            return View("Error", r.Errors);
                        }
                    }
                    foreach (string uis in model.IdsToDelete ?? new string[] { })
                    {
                        //id роли
                        var role = RoleManager.FindById(uis);
                        var user = UserManager.FindById(uis);

                        r = UserManager.RemoveFromRole(user.Id, model.rolename);
                        if (!r.Succeeded)
                        {
                            return View("Error", r.Errors);
                        }
                    }

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error", "role is not found");
            }
        }
       //[Authorize(Roles = "Administrator, Director")]
        // GET: Role/Delete/5
      
        public ActionResult Delete(string id)
        {
            var role = RoleManager.FindById(id);
            if (role != null)
            {
                var rezult =  RoleManager.Delete(role);
                if (rezult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("error", rezult.Errors);
                }
            }
            else
            {

            }
            return View();
        }


        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
       
    }
}
