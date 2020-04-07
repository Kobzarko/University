using System;
using System.Collections.Generic;

namespace University.Models
{
    public class Student
    {
        //Свойство ID будет использоваться в качестве столбца первичного ключа 
        //    в таблице базы данных, соответствующей этому классу
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        //Свойство Enrollments является свойством навигации
        //Свойства навигации содержат другие сущности, связанные с этой сущностью.
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}