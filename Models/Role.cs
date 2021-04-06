using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourCompany.Models
{
    public class Role
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите название роли")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        public string Description { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}