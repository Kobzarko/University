using University.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace University.DAL
{

    //Чтобы указать Entity Framework использовать класс инициализатора, 
    //    добавьте элемент в элемент entityFramework
    //    в файле Web.config приложения (в корневой папке проекта)

  //    <contexts>
  //         <context type = "ContosoUniversity.DAL.SchoolContext, ContosoUniversity" >
  //            < databaseInitializer type="ContosoUniversity.DAL.SchoolInitializer, ContosoUniversity" />
  //         </context>
  //    </contexts>


    public class SchoolContext : DbContext
    {
        // Имя строки подключения по умолчанию в этом примере будет SchoolContext, то же, что и то, что вы указываете явно.
        public SchoolContext() : base("SchoolContext")
        {
        }

        //Можно опустить операторы DbSet<Enrollment> и DbSet<Course>, и это будет работать одинаково.
        //    Entity Framework будет включать их неявно, так как сущность Student ссылается на сущность Enrollment,
        //    а Enrollment сущность ссылается на сущность Course.

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }


        // Инструкция modelBuilder.Conventions.Remove в методе OnModelCreating предотвращает множественное преобразование имен таблиц. 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
