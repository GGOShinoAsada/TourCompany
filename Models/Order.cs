using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourCompany.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Это поле обязательно")]
        public string Cipher { get; set; }
        [Required(ErrorMessage = "Пожалуйста укажите заказчика")]
        [Display(Name = "Закзчик")]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите количество дней")]
        [Display(Name = "Количество дней")]
        public int DaysCount { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите количество человек")]
        [Display(Name = "Количество человек")]
        public int HumanCount { get; set; }
        [Required(ErrorMessage = "Пожалуйста, укажите тур")]
        [Display(Name = "Тур")]
        public int TourId { get; set; }
        [Required(ErrorMessage = "Пожалуйста, укажите дату отпракления")]
        [Display(Name = "Дата отправления")]
        public DateTime DateOfDispatch { get; set; }
        [Required(ErrorMessage = "Пожалуйста, укажите дату возвращения")]
        [Display(Name = "Дата возвращения")]
        public DateTime ReturnDate { get; set; }
        [Required(ErrorMessage = "Пожалуйста, укажите способ оплаты")]
        [Display(Name = "Спосособ оплаты")]
        public int PayMethodId { get; set; }
        [Required(ErrorMessage ="Пожалуйста, укажите стоимость заказа")]
        [Display(Name ="Стоимость заказа")]
        public double Cost { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите состояние заказа")]
        [Display(Name = "Состояние заказа")]
        public int OrderStateId { get; set; }

        [Required(ErrorMessage ="Пожалуйста, введите тип программы")]
        [Display(Name ="Название программы")]
  
        public int ProgramTypeId { get; set; }

        [Display(Name = "Описание заказа")]
        public string Description { get; set; }
        [NotMapped]
        public PayMethod PayMethod { get; set; }
        [NotMapped]
        public OrderState OrderState { get; set; }
        [NotMapped]
        public ProgramType ProgramType { get; set; }
        [NotMapped]
        public User User { get; set; }
        [NotMapped]
        public Tour Tour { get; set; }
        public Order()
        {
            PayMethod = new PayMethod();
            OrderState = new OrderState();
            ProgramType = new ProgramType();
            User = new User();
            Tour = new Tour();
        }
    }
}