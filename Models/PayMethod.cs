using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourCompany.Models
{
    public class PayMethod
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите название способа оплаты")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите описание способа оплаты")]
        public string Description { get; set; }
    }
}