using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Course
    {
        // этот атрибут позволяет ввести первичный ключ для курса, а не использовать базу данных, чтобы создать его.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        // Свойство Enrollments является свойством навигации
        // Сущность Course может быть связана с любым числом сущностей Enrollment
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}