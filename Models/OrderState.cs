using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourCompany.Models
{
    public class OrderState
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите сосотяние заказа")]
        [Display(Name = "Состояние заказа")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите описание состояния заказа")]
        [Display(Name = "Пожадуйста, введите введите описание заказа")]
        public string Description { get; set; }
    }
}