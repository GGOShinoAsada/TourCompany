using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourCompany.Models
{
    public class Status
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите название статуса")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите описание статуса")]
        public string Description { get; set; }
    }
}