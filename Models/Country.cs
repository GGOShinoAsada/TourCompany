using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourCompany.Models
{
    public class Country
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите название страны")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите описание страны")]
        public string Description { get; set; }
        public List<Tour> Tours { get; set; }
        public Country()
        {
            Tours = new List<Tour>();
        }
    }
}