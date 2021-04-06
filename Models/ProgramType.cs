using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourCompany.Models
{
    public class ProgramType
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите название типа программы")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите описание типа программы")]
        public string Description { get; set; }
    }
}