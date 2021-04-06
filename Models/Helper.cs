using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TourCompany.Models
{
    public class Helper
    {
        public class REPost
        {
            [Required]
            public string rolename { get; set; }
            public string[] IdsToAdd { get; set; }
            public string[] IdsToDelete { get; set; }
        }

        public class REModificated
        {
            public ApplicationRole Role { get; set; }
            public List<FakeUser> Members { get; set; }
            public List<FakeUser> UnMembers { get; set; }
            public REModificated()
            {
                Members = new List<FakeUser>();
                UnMembers = new List<FakeUser>();
            }
        }

        public class FakeUser
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public bool Flag { get; set; } = false;
        }
    }
    public static class CustomContext
    {

        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            var context = new ApplicationDbContext();
            var rolestore = new UserStore<ApplicationUser>(context);
            var d = new UserManager<ApplicationUser>(rolestore);
            return new MvcHtmlString(d.FindByIdAsync(id).Result.UserName);
        }
    }
    public class FakeTour
    { 
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите название тура")]
        public string Name { get; set; }
        public int TourTypeId { get; set; }
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите стоимость тура")]
        public double Cost { get; set; }
        [Display(Name = "Количество дней")]
        [Required(ErrorMessage = "Пожалуйста, введите количество дней")]
        [NotMapped]
        public int CountDays { get; set; }
        [Required(ErrorMessage = "Пожалуйста, ввыедие дату отправления")]
        [Display(Name = "Дата оправления")]
        [NotMapped]
        public string DateOfDispatch { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите описание тура")]
        public string Description { get; set; }
    }
}