

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class OfficeAssignment
    {
     
        
            [Key] // идентификатор ID указан явно
            [ForeignKey("Instructor")] // внешний ключ
            public int InstructorID { get; set; }
            [StringLength(50)] // длина 50
            [Display(Name = "Office Location")] // заголовок имеет такой вид
            public string Location { get; set; }

            public virtual Instructor Instructor { get; set; } // один к одному
        

    }
}