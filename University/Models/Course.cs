using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Course
    {
        // этот атрибут позволяет ввести первичный ключ для курса, а не использовать базу данных, чтобы создать его.
        // указывает, что значения первичного ключа предоставляются пользователем, а не создаются базой данных.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        // Курс назначается одной кафедре, 
        // поэтому по указанным выше причинам имеется внешний ключ DepartmentID и свойство навигации Department.
        public int DepartmentID { get; set; }

        public  virtual Department Department  { get; set; }
        // Свойство Enrollments является свойством навигации
        // Сущность Course может быть связана с любым числом сущностей Enrollment
        // На курс может быть зачислено любое количество учащихся
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        // Курс могут вести несколько преподавателей
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}