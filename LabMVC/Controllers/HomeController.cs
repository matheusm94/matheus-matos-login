using LabMVC.Filtros;
using LabWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabMVC.Controllers
{
    public class HomeController : LogadoController
    {
        [AutenticadoFilter]
        public ActionResult Index()
        {
            // if (!Logado()) return null;

            // return new HttpUnauthorizedResult();
            ViewBag.usuarios = 1;
            //ViewBag.clientes = Cliente.Todos();
            //ViewData["clientes ssds"] = Cliente.Todos();
            //return View(new
            //{
            //    Clientes = Cliente.Todos(),
            //    Mensagem = "oi"
            //});
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}