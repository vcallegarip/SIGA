using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGA_Model;

namespace SIGA_WebApplication.Controllers
{
    public class CalificacionController : Controller
    {
        private SIGAEntities db = new SIGAEntities();

        // GET: Calificacion
        public ActionResult Index()
        {
            var calificacion = db.Calificacion.Include(c => c.Alumno).Include(c => c.Curso).Include(c => c.Modulo).Include(c => c.Profesor);
            return View(calificacion.ToList());
        }

        // GET: Calificacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }

            calificacion.Alu_Id = db.Alumno.Where(a => a.Alu_Id == calificacion.Alu_Id).Select(c => c.User_Id).SingleOrDefault();
            calificacion.Prof_Id = db.Profesor.Where(a => a.Prof_Id == calificacion.Prof_Id).Select(c => c.User_Id).SingleOrDefault();

            var alumnos = (from u in db.Usuario
                           join p in db.Persona
                           on u.Per_Id equals p.Per_Id
                           where u.TipoUser_Id == 1
                           select new AlumnoCalifForDDL { Alu_Id = u.User_Id, AluNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            var profesores = (from u in db.Usuario
                              join p in db.Persona
                              on u.Per_Id equals p.Per_Id
                              where u.TipoUser_Id == 2
                              select new ProfesorCalifForDDL { Prof_Id = u.User_Id, ProfNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            //ViewBag.AlumnoNombre = alumnos[0].AluNombre;
            //ViewBag.ProfesorNombre = profesores[0].ProfNombre;

            ViewBag.Alu_Id = new SelectList(alumnos, "Alu_Id", "AluNombre", calificacion.Alu_Id);
            ViewBag.Cur_Id = new SelectList(db.Curso, "CurId", "CurName", calificacion.Cur_Id);
            ViewBag.ModId = new SelectList(db.Modulo, "ModId", "ModNombre", calificacion.ModId);
            ViewBag.Prof_Id = new SelectList(profesores, "Prof_Id", "ProfNombre", calificacion.Prof_Id);

            return View(calificacion);
        }

        // GET: Calificacion/Create
        public ActionResult Create()
        {

            var alumnos = (from u in db.Usuario
                           join p in db.Persona
                           on u.Per_Id equals p.Per_Id
                           where u.TipoUser_Id == 1
                           select new AlumnoCalifForDDL { Alu_Id = u.User_Id, AluNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            var profesores = (from u in db.Usuario
                              join p in db.Persona
                              on u.Per_Id equals p.Per_Id
                              where u.TipoUser_Id == 2
                              select new ProfesorCalifForDDL { Prof_Id = u.User_Id, ProfNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();


            ViewBag.Alu_Id = new SelectList(alumnos, "Alu_Id", "AluNombre");
            ViewBag.Cur_Id = new SelectList(db.Curso, "CurId", "CurName");
            ViewBag.ModId = new SelectList(db.Modulo, "ModId", "ModNombre");
            ViewBag.Prof_Id = new SelectList(profesores, "Prof_Id", "ProfNombre");
            return View();
        }

        // POST: Calificacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CalifId,Prof_Id,Alu_Id,ModId,Cur_Id,Cal_Fech,Cal_N1,Cal_N2,Cal_N3,Cal_N4,Cal_N5,Cal_N6")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                calificacion.Alu_Id = db.Alumno.Where(a => a.User_Id == calificacion.Alu_Id).Select(c => c.Alu_Id).SingleOrDefault();
                calificacion.Prof_Id = db.Profesor.Where(a => a.User_Id == calificacion.Prof_Id).Select(c => c.Prof_Id).SingleOrDefault();

                db.Calificacion.Add(calificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var alumnos = (from u in db.Usuario
                           join p in db.Persona
                           on u.Per_Id equals p.Per_Id
                           where u.TipoUser_Id == 1
                           select new AlumnoCalifForDDL { Alu_Id = u.User_Id, AluNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            var profesores = (from u in db.Usuario
                              join p in db.Persona
                              on u.Per_Id equals p.Per_Id
                              where u.TipoUser_Id == 2
                              select new ProfesorCalifForDDL { Prof_Id = u.User_Id, ProfNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            ViewBag.Alu_Id = new SelectList(alumnos, "Alu_Id", "Alu_Apoderado", calificacion.Alu_Id);
            ViewBag.Cur_Id = new SelectList(db.Curso, "CurId", "CurName", calificacion.Cur_Id);
            ViewBag.ModId = new SelectList(db.Modulo, "ModId", "ModNombre", calificacion.ModId);
            ViewBag.Prof_Id = new SelectList(profesores, "Prof_Id", "Prof_Especialidad", calificacion.Prof_Id);
            return View(calificacion);
        }

        // GET: Calificacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Calificacion calificacion = db.Calificacion.Find(id);

            if (calificacion == null)
            {
                return HttpNotFound();
            }

            calificacion.Alu_Id = db.Alumno.Where(a => a.Alu_Id == calificacion.Alu_Id).Select(c => c.User_Id).SingleOrDefault();
            calificacion.Prof_Id = db.Profesor.Where(a => a.Prof_Id == calificacion.Prof_Id).Select(c => c.User_Id).SingleOrDefault();

            var alumnos = (from u in db.Usuario
                           join p in db.Persona
                           on u.Per_Id equals p.Per_Id
                           where u.TipoUser_Id == 1
                           select new AlumnoCalifForDDL { Alu_Id = u.User_Id, AluNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            var profesores = (from u in db.Usuario
                              join p in db.Persona
                              on u.Per_Id equals p.Per_Id
                              where u.TipoUser_Id == 2
                              select new ProfesorCalifForDDL { Prof_Id = u.User_Id, ProfNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            ViewBag.Alu_Id = new SelectList(alumnos, "Alu_Id", "AluNombre", calificacion.Alu_Id);
            ViewBag.Cur_Id = new SelectList(db.Curso, "CurId", "CurName", calificacion.Cur_Id);
            ViewBag.ModId = new SelectList(db.Modulo, "ModId", "ModNombre", calificacion.ModId);
            ViewBag.Prof_Id = new SelectList(profesores, "Prof_Id", "ProfNombre", calificacion.Prof_Id);
            return View(calificacion);
        }

        // POST: Calificacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CalifId,Prof_Id,Alu_Id,ModId,Cur_Id,Cal_Fech,Cal_N1,Cal_N2,Cal_N3,Cal_N4,Cal_N5,Cal_N6")] Calificacion calificacion)
        {
            calificacion.Alu_Id = db.Alumno.Where(a => a.User_Id == calificacion.Alu_Id).Select(c => c.Alu_Id).SingleOrDefault();
            calificacion.Prof_Id = db.Profesor.Where(a => a.User_Id == calificacion.Prof_Id).Select(c => c.Prof_Id).SingleOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(calificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var alumnos = (from u in db.Usuario
                           join p in db.Persona
                           on u.Per_Id equals p.Per_Id
                           where u.TipoUser_Id == 1
                           select new AlumnoCalifForDDL { Alu_Id = u.User_Id, AluNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            var profesores = (from u in db.Usuario
                              join p in db.Persona
                              on u.Per_Id equals p.Per_Id
                              where u.TipoUser_Id == 2
                              select new ProfesorCalifForDDL { Prof_Id = u.User_Id, ProfNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            ViewBag.Alu_Id = new SelectList(alumnos, "Alu_Id", "AluNombre", calificacion.Alu_Id);
            ViewBag.Cur_Id = new SelectList(db.Curso, "CurId", "CurName", calificacion.Cur_Id);
            ViewBag.ModId = new SelectList(db.Modulo, "ModId", "ModNombre", calificacion.ModId);
            ViewBag.Prof_Id = new SelectList(profesores, "Prof_Id", "ProfNombre", calificacion.Prof_Id);
            return View(calificacion);
        }

        // GET: Calificacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }

            calificacion.Alu_Id = db.Alumno.Where(a => a.Alu_Id == calificacion.Alu_Id).Select(a => a.User_Id).SingleOrDefault();
            calificacion.Prof_Id = db.Profesor.Where(p => p.Prof_Id == calificacion.Prof_Id).Select(p => p.User_Id).SingleOrDefault();


            var alumno = (from u in db.Usuario
                           join p in db.Persona
                           on u.Per_Id equals p.Per_Id
                           where u.TipoUser_Id == 1 && u.User_Id == calificacion.Alu_Id
                          select new AlumnoCalifForDDL { Alu_Id = u.User_Id, AluNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).SingleOrDefault();

            var profesor = (from u in db.Usuario
                              join p in db.Persona
                              on u.Per_Id equals p.Per_Id
                              where u.TipoUser_Id == 2 && u.User_Id == calificacion.Prof_Id
                            select new ProfesorCalifForDDL { Prof_Id = u.User_Id, ProfNombre = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).SingleOrDefault();

            ViewBag.AlumnoNombre = alumno.AluNombre;
            ViewBag.ProfesorNombre = profesor.ProfNombre;

            return View(calificacion);
        }

        // POST: Calificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calificacion calificacion = db.Calificacion.Find(id);
            db.Calificacion.Remove(calificacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class AlumnoCalifForDDL
    {
        public int Alu_Id { get; set; }
        public string AluNombre { get; set; }
    }

    public class ProfesorCalifForDDL
    {
        public int Prof_Id { get; set; }
        public string ProfNombre { get; set; }
    }

}
