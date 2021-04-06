using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace TourCompany.Models
{
    public class Tour
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите название тура")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, выберите тип тура")]
       
        public int TourTypeId { get; set; }
        [Required(ErrorMessage = "Пожалуйста, выбериите страну")]
     
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите стоимость тура")]
        public double Cost { get; set; }
        [Display(Name ="Количество дней")]
        [Required(ErrorMessage ="Пожалуйста, введите количество дней")]
    
        public int CountDays { get; set; }
        [Required(ErrorMessage ="Пожалуйста, ввыедие дату отправления")]
        [Display(Name ="Дата оправления")]

        public DateTime DateOfDispatch { get; set; }
        [Required(ErrorMessage = "Пожалуйста, укадите статус")]
        [Display(Name="Статус")]
        public bool Status { get; set; } = true;

        [Required(ErrorMessage = "Пожалуйста, введите описание тура")]
        public string Description { get; set; }
        [NotMapped]
        public Country Country { get; set; }
        [NotMapped]
        public Type Type { get; set; }
        public Tour()
        {
            Country = new Country();
            Type = new Type();
        }
    }
}