using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using University.DAL;
using University.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace University.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();


        /// <summary>
        /// Install-Package PagedList.Mvc 
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        // GET: Student
        // добавляет параметр page, текущий параметр порядка сортировки и текущий параметр фильтра
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            // предоставляет представление с текущей строкой фильтра
            ViewBag.CurrentFilter = searchString; 

            var students = from s in db.Students
                           select s;
            // строка поиска
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            
            // сортировка 
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default: // name ASCE
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            // При нажатии ссылки на подкачку переменная page содержит номер отображаемой страницы
            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.

        // атрибут ValidateAntiForgeryToken помогает предотвратить атаки подделки межсайтовых запросов 
        // Атрибут Bind является одним из способов защиты от чрезмерного размещения в сценариях создания. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstMidName, EnrollmentDate")]Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException  /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.Students.Find(id);
            // обновить поля из данных, введенных пользователем, в отправленные данные формы.
            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException  /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Student/Delete/5
        // метод HttpGet Delete не удаляет данные
        // Этот код принимает необязательный параметр , который указывает, был ли вызван метод после сбоя сохранения изменений.
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            // извлекает выбранную сущность, а затем вызывает метод Remove
            //  чтобы задать для сущности состояние Deleted
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                // либо так + повышение производительности в большом объеме
                //Student studentToDelete = new Student() { ID = id };
                //db.Entry(studentToDelete).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        // Чтобы закрыть подключения к базе данных и освободить ресурсы
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
