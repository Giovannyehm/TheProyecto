using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheProyecto.Models;

namespace TheProyecto.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.compra.ToList());
            }
        }

        public static string NombreUsuario(int id_usuario)
        {
            using (var db = new inventario2021Entities())
            {
                return db.usuario.Find(id_usuario).nombre;
            }
        }

        public ActionResult ListaUsuario()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public static string NombreCliente(int id_cliente)
        {
            using (var db = new inventario2021Entities())
            {
                return db.cliente.Find(id_cliente).nombre;
            }
        }

        public ActionResult ListaCliente()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.cliente.ToList());
            }
        }

        //vista crear
        public ActionResult Create()
        {
            return View();
        }

        //recibir datos crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(compra compra)
        {
            if (!ModelState.IsValid)
                return View();


            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.compra.Add(compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var findCompra = db.compra.Find(id);
                return View(findCompra);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findCompra = db.compra.Find(id);
                    db.compra.Remove(findCompra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        //vista editar
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findCompra = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findCompra);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        //recibir datos editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(compra editCompra)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var compra = db.compra.Find(editCompra.id);

                    compra.fecha = editCompra.fecha;

                    compra.total = editCompra.total;

                    compra.id_usuario = editCompra.id_usuario;

                    compra.id_cliente = editCompra.id_cliente;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }
    }
}