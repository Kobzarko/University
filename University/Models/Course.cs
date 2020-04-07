using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Course
    {
        // этот атрибут позволяет ввести первичный ключ для курса, а не использовать базу данных, чтобы создать его.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        // Свойство Enrollments является свойством навигации
        // Сущность Course может быть связана с любым числом сущностей Enrollment
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}