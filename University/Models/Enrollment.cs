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
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}