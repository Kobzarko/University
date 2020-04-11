using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        // Свойство EnrollmentID будет первичным ключом
        public int EnrollmentID { get; set; }
        // Свойство CourseID представляет собой внешний ключ.
        public int CourseID { get; set; }
        // Свойство StudentID представляет собой внешний ключ.
        public int StudentID { get; set; }
        // Свойство Grade является перечислением.
        // Знак вопроса после объявления типа Grade указывает, что свойство Grade допускает значение NULL.
        [DisplayFormat(NullDisplayText = "No grade")] 
        public Grade? Grade { get; set; }

        /// <summary>
        /// Запись зачисления предназначена для одного курса,
        /// поэтому доступно свойство первичного ключа CourseID и свойство навигации Course
        /// </summary>
        public virtual Course Course { get; set; }

        /// <summary>
        /// Запись зачисления предназначена для одного учащегося,
        /// поэтому доступно свойство первичного ключа StudentID и свойство навигации Student
        /// </summary>
        public virtual Student Student { get; set; }
    }
}