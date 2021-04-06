using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourCompany.Models
{
   
    public class Contact
    {
        [Required(ErrorMessage = "Пожалуйста, ввведите имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите адрес электронной почты")]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пожалуйста, укажите причину")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }
    }
    
}