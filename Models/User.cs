using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourCompany.Models
{
    public class User
    {
        [Required]
        public string Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите серию и номер паспорта")]
        public string Passport { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите паспортные данные")]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите адрес электронной почты")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите адрес")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите номер телефона")]
        [Phone]
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }
        public User()
        {
            Role = new Role();
        }
    }
}