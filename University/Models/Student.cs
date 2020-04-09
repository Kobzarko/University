using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Student
    {
        //Свойство ID будет использоваться в качестве столбца первичного ключа 
        //    в таблице базы данных, соответствующей этому классу
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")] // ограничение к входным даннвм
        [StringLength(50, MinimumLength = 2)] // длина не более 50 символов минимум 2
        [Required] // обязательное поле
        [Display(Name = "Last Name")] // заголовки имеют такой вид
        public string LastName { get; set; }

        [Required] // обязательное поле
        [Column("FirstName")] // при создании FirstMidName будет FirstName
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")] // Первый символ в Верхнем регистре  остальные нижний
        [StringLength(50,ErrorMessage ="First name must be shorter")] // Длина строки
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        
        [DataType(DataType.Date)] //  используется для указания более конкретного типа данных
        // Параметр ApplyFormatInEditMode указывает, что указанное форматирование 
        // должно также применяться при отображении значения в текстовом поле для редактирования.
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Enrollment Date")] // заголовки имеют такой вид
        public DateTime EnrollmentDate { get; set; }


        //  возвращает значение, созданное путем объединения двух других свойств
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        //Свойство Enrollments является свойством навигации
        //Свойства навигации содержат другие сущности, связанные с этой сущностью.
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}