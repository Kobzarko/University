using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)] // длина строки
        public string Name { get; set; }

        [DataType(DataType.Currency)] // тип валюта 
        [Column(TypeName = "money")] // Сопоставление столбцов столбец будет хранить денежные суммы
        public decimal Budget { get; set; }

        [DataType(DataType.Date)] // тип дата
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // формат вывода даты
        [Display(Name = "Start Date")] // название 
        public DateTime StartDate { get; set; }

        // Кафедра может иметь или не иметь администратора, и администратор всегда является преподавателем.
        public int? InstructorID { get; set; } // допускает null 

        // Свойство навигации называется Administrator но содержит сущность Instructor
        public virtual Instructor Administrator { get; set; }

        // В отделе может быть много курсов, поэтому имеется Courses свойство навигации
        public virtual ICollection<Course> Courses { get; set; }
    }

    ///Если бизнес-правила, необходимые для InstructorID свойства,
    ///не допускают значения NULL, необходимо использовать следующую 
    ///инструкцию fluent API, чтобы отключить каскадное удаление для связи
    /// modelBuilder.Entity().HasRequired(d => d.Administrator).WithMany().WillCascadeOnDelete(false);

}

