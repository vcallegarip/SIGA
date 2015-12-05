using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGA_Model;
using SIGA.Models.ViewModels;

namespace SIGA_WebApplication.Controllers
{
    public class MatriculaController : Controller
    {
        private SIGAEntities db = new SIGAEntities();

        // GET: Matricula
        public ActionResult Index()
        {
            var matricula = db.Matricula.Include(m => m.Alumno).Include(m => m.Curso).Include(m => m.Modulo).Include(m => m.Programa).Include(m => m.Recibo);
            return View(matricula.ToList());
        }

        // GET: Matricula/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matricula matricula = db.Matricula.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }

            var alumnos = (from u in db.Usuario
                           join p in db.Persona
                           on u.Per_Id equals p.Per_Id
                           where u.TipoUser_Id == 1
                           select new AlumnoForDDL { Alu_Id = u.User_Id, Alu_Apoderado = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            ViewBag.AlumnoNombre = alumnos[0].Alu_Apoderado;

            return View(matricula);
        }

        // GET: Matricula/Create
        public ActionResult Create()
        {
            var alumnos = (from u in db.Usuario
                           join p in db.Persona
                           on u.Per_Id equals p.Per_Id
                           where u.TipoUser_Id == 1
                           select new AlumnoForDDL { Alu_Id = u.User_Id, Alu_Apoderado = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            ViewBag.Alu_Id = new SelectList(alumnos, "Alu_Id", "Alu_Apoderado");
            ViewBag.CurId = new SelectList(db.Curso, "CurId", "CurName");
            ViewBag.ModId = new SelectList(db.Modulo, "ModId", "ModNombre");
            ViewBag.ProgId = new SelectList(db.Programa, "ProgId", "ProgNombre");
            ViewBag.RecId = new SelectList(db.Recibo, "RecId", "RecId");
            return View();
        }

        // POST: Matricula/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MatriId,ProgId,ModId,CurId,Alu_Id,RecId,MatriFecha,MatriEstado")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                matricula.Alu_Id = db.Alumno.Where(a => a.User_Id == matricula.Alu_Id).Select(c => c.Alu_Id).SingleOrDefault();

                db.Matricula.Add(matricula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Alu_Id = new SelectList(db.Alumno, "Alu_Id", "Alu_Apoderado", matricula.Alu_Id);
            ViewBag.CurId = new SelectList(db.Curso, "CurId", "CurName", matricula.CurId);
            ViewBag.ModId = new SelectList(db.Modulo, "ModId", "ModNombre", matricula.ModId);
            ViewBag.ProgId = new SelectList(db.Programa, "ProgId", "ProgNombre", matricula.ProgId);
            ViewBag.RecId = new SelectList(db.Recibo, "RecId", "RecId", matricula.RecId);
            return View(matricula);
        }

        // GET: Matricula/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matricula matricula = db.Matricula.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }

            var alumnos = (from u in db.Usuario
                           join p in db.Persona
                           on u.Per_Id equals p.Per_Id
                           where u.TipoUser_Id == 1
                           select new AlumnoForDDL { Alu_Id = u.User_Id, Alu_Apoderado = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            ViewBag.Alu_Id = new SelectList(alumnos, "Alu_Id", "Alu_Apoderado", matricula.Alu_Id);
            ViewBag.CurId = new SelectList(db.Curso, "CurId", "CurName", matricula.CurId);
            ViewBag.ModId = new SelectList(db.Modulo, "ModId", "ModNombre", matricula.ModId);
            ViewBag.ProgId = new SelectList(db.Programa, "ProgId", "ProgNombre", matricula.ProgId);
            ViewBag.RecId = new SelectList(db.Recibo, "RecId", "RecId", matricula.RecId);
            return View(matricula);
        }

        // POST: Matricula/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MatriId,ProgId,ModId,CurId,Alu_Id,RecId,MatriFecha,MatriEstado")] Matricula matricula)
        {
            var aluId = (from u in db.Alumno where u.User_Id == matricula.Alu_Id select u).FirstOrDefault();

            matricula.Alu_Id = aluId.Alu_Id;

            if (ModelState.IsValid)
            {
                db.Entry(matricula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var alumnos = (from u in db.Usuario
                           join p in db.Persona
                           on u.Per_Id equals p.Per_Id
                           where u.TipoUser_Id == 1
                           select new AlumnoForDDL { Alu_Id = u.User_Id, Alu_Apoderado = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            ViewBag.Alu_Id = new SelectList(alumnos, "Alu_Id", "Alu_Apoderado", matricula.Alu_Id);
            ViewBag.CurId = new SelectList(db.Curso, "CurId", "CurName", matricula.CurId);
            ViewBag.ModId = new SelectList(db.Modulo, "ModId", "ModNombre", matricula.ModId);
            ViewBag.ProgId = new SelectList(db.Programa, "ProgId", "ProgNombre", matricula.ProgId);
            ViewBag.RecId = new SelectList(db.Recibo, "RecId", "RecId", matricula.RecId);
            return View(matricula);
        }

        // GET: Matricula/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matricula matricula = db.Matricula.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }

            var alumnos = (from u in db.Usuario
                           join p in db.Persona
                           on u.Per_Id equals p.Per_Id
                           where u.TipoUser_Id == 1
                           select new AlumnoForDDL { Alu_Id = u.User_Id, Alu_Apoderado = p.Per_Nombre + " " + p.Per_ApePaterno + " " + p.Per_ApeMaterno }).ToList();

            ViewBag.AlumnoNombre = alumnos[0].Alu_Apoderado;

            return View(matricula);
        }

        // POST: Matricula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Matricula matricula = db.Matricula.Find(id);
            db.Matricula.Remove(matricula);
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

    
}
