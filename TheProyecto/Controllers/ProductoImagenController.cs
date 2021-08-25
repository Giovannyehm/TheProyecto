using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheProyecto.Models;

namespace TheProyecto.Controllers
{
    public class ProductoImagenController : Controller
    {
        // GET: ProductoImagen
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_imagen.ToList());
            }
        }

        public static string NombreProducto(int idProducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }

        public ActionResult ListaProducto()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.producto.ToList());
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
        public ActionResult Create(producto_imagen producto_imagen)
        {
            if (!ModelState.IsValid)
                return View();


            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_imagen.Add(producto_imagen);
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
                var findProduImage = db.producto_imagen.Find(id);
                return View(findProduImage);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findProduImage = db.producto_imagen.Find(id);
                    db.producto_imagen.Remove(findProduImage);
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
                    var findProduImage = db.producto_imagen.Where(a => a.id == id).FirstOrDefault();
                    return View(findProduImage);
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
        public ActionResult Edit(producto_imagen editProduImage)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var produImage = db.producto_imagen.Find(editProduImage.id);

                    produImage.id_producto = editProduImage.id_producto;

                    produImage.imagen = editProduImage.imagen;

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