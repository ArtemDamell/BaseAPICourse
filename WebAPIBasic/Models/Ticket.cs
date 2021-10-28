//using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel.DataAnnotations;
//using WebAPIBasic.Models.ModelValidators;

//namespace WebAPIBasic.Models
//{
//    // 10.
//    public class Ticket
//    {
//        // 12.
//        // 15.
//        //[FromQuery(Name = "tId")]
//        public int Id { get; set; }
//        // 12.
//        // 15.
//        //[FromRoute(Name = "pId")]

//        // 16. Добавляем аннотацию валидации
//        [Required]
//        // 17. Превращаем в nullable int
//        public int? ProjectId { get; set; }

//        // 16. Добавляем аннотацию валидации
//        [Required]
//        public string Title { get; set; }

//        public string Description { get; set; }

//        // 18. Расширяем модель новыми свойствами
//        public string Owner { get; set; }

//        // 21. Применяем свою валидационную аннотацию к свойству
//        [Ticket_OwnerDateValidator]
//        // 22. Практическое задание
//        [Ticket_FutureDateValidator]
//        public DateTime? EventDate { get; set; }

//        // 25.4
//        public DateTime? EnteredDate { get; set; }
//    }
//}
