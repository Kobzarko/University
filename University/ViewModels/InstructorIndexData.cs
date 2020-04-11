using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University.Models;

namespace University.ViewModels
{
    public class InstructorIndexData
    {
        /// <summary>
        /// cоздаем модель представления, которая включает три свойства, 
        /// каждое из которых содержит данные из одной таблицы.
        /// </summary>
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}