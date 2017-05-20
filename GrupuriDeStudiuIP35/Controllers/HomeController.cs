using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GrupuriDeStudiuIP35;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text;


namespace GrupuriDeStudiuIP35.Controllers
{
    public class HomeController : Controller
    {
        private GrupuriDeStudiuEntities db = new GrupuriDeStudiuEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profil()
        {
            if (Session["Id"] != null)
            {
                var model = db.UTILIZATORIs;
                return View(model);
            }
            else
            {
                return RedirectToAction("Logare");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logare");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMaterie,Nume,Tip,Descriere,Data")] GRUPURI grup)

        {
            if (Session["Id"] != null)
            {
                if (ModelState.IsValid)
                {
                    grup.Data = DateTime.Now.Date;
                    grup.IdMaterie = int.Parse(grup.IdMaterie.ToString());

                    db.GRUPURIs.Add(grup);
                    db.SaveChanges();

                    APARTENENTA ap = new APARTENENTA();
                    ap.IdUtilizator = int.Parse(Session["Id"].ToString());
                    int maxId = db.GRUPURIs.Max(u => u.Id);
                    ap.IdGrup = maxId;
                    ap.Rol = "Creator";

                    db.APARTENENTAs.Add(ap);
                    db.SaveChanges();

                    ModelState.Clear();
                }

                return RedirectToAction("Grup");
            }
            else
            {
                return RedirectToAction("Logare");
            }

        }

        public ActionResult Grup([Bind(Include = "IdGrup,IdUtilizator,Tip,Text,Data")] SUBIECTE subiect)
        {
            if (Session["Id"] != null)
            {
                subiect.IdUtilizator = int.Parse(Session["Id"].ToString());
                subiect.Data = DateTime.Now.Date;
                var model2 = db.GRUPURIs.Include(d => d.MATERII).Include(m => m.APARTENENTAs).Include(a => a.SUBIECTEs);
                return View("Grup", model2);

            }
            else
            {
                return RedirectToAction("Logare");
            }

        }

        public ActionResult GrupurileMele()
        {
            if (Session["Id"] != null)
            {
                int idS = int.Parse(Session["Id"].ToString());
                var model3 = db.GRUPURIs.Include(a => a.APARTENENTAs).Where(b => b.APARTENENTAs.Any(c => c.IdUtilizator == idS));
                return View("GrupurileMele", model3);
            }
            else
            {
                return RedirectToAction("Logare");
            }
        }

        public ActionResult Deschide(int? id)
        {
            if (Session["Id"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var model4 = db.GRUPURIs.Include(d => d.MATERII);
                return View("Deschide", model4);
            }
            else
            {
                return RedirectToAction("Logare");
            }

        }
        

        public ActionResult Forum()
        {

            return View("Forum");
        }

        [HttpGet]
        public ActionResult Cauta()
        {
            if (Session["Id"] != null)
            {
                return View("Cauta");
            }
            else
            {
                return RedirectToAction("Logare");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cauta([Bind(Include = "IdMaterie,Tip")] GRUPURI grup)
        {
            if (Session["Id"] != null)
            {
                if (ModelState.IsValid)
                {

                    var model5 = db.GRUPURIs.Include(d => d.MATERII).Include(c => c.CERERIs).Where(a => a.IdMaterie == grup.IdMaterie && a.Tip == grup.Tip);

                    return View("Rezultat", model5);
                }
                else
                    return View("Rezultat");
            }
            else
            {
                return RedirectToAction("Logare");
            }

        }

        public ActionResult LogOut()
        {
            Session["Id"] = null;
            Session["Nume"] = null;
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Logare");
        }

        public ActionResult Logare()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Logare(UTILIZATORI utilizator)
        {
            if (ModelState.IsValid)
            {
                UTILIZATORI user = db.UTILIZATORIs.FirstOrDefault(u => u.Email == utilizator.Email && u.Parola == utilizator.Parola);
                if (user != null)
                {
                    Session["Id"] = user.Id.ToString();
                  //  Session["Nume"] = user.Nume.ToString();
                    return RedirectToAction("Profil");
                }
                else
                {
                    ViewBag.Message = "Ati introdus o adresa de e-mail sau o parola gresita!";
                }
            }

            return View();
        }

        public ActionResult Trimite(int? id)
        {
            if (Session["Id"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                else

                if (ModelState.IsValid)
                {           
                    CERERI req = new CERERI();
                    req.IdUtilizator = int.Parse(Session["Id"].ToString());
                    req.IdGrup = id ?? default(int);
                    req.Status = "Pending";
                    db.CERERIs.Add(req);
                    db.SaveChanges();

                    ModelState.Clear();
                }

                return View("Trimite");
            }
            else
            {
                return RedirectToAction("Logare");
            }

        }


    }
}