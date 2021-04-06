using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourCompany.Models
{
    public class Type
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите тип тура")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите количество человек")]
        [Display(Name ="Количество человек")]
        public int HumansCount { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите описание типа")]
        [Display(Name="Описание")]
        public string Description { get; set; }
        public List<Tour> Tours { get; set; }
        public Type()
        {
            Tours = new List<Tour>();

        }
    }
}