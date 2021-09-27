using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheProyecto.Models;


namespace TheProyecto.Controllers
{
    public class ProveedorController : Controller
    {
        [Authorize]
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.proveedor.ToList());
            }
                
        }

        //vista crear Proveedor
        public ActionResult Create()
        {
            return View();
        }


        //recibir datos crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View();


            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.proveedor.Add(proveedor);
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
                var findProvider = db.proveedor.Find(id);
                return View(findProvider);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findProvider = db.proveedor.Find(id);
                    db.proveedor.Remove(findProvider);
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
                    proveedor findProvider = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findProvider);
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
        public ActionResult Edit(proveedor editProvider)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var provider = db.proveedor.Find(editProvider.id);

                    provider.nombre = editProvider.nombre;

                    provider.direccion = editProvider.direccion;

                    provider.telefono = editProvider.telefono;

                    provider.nombre_contacto = editProvider.nombre_contacto;

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

        public ActionResult uploadCSV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploadCSV(HttpPostedFileBase fileForm)
        {
            try
            {
                //string guardar archivo
                string filePath = string.Empty;

                //condicion saber si llego
                if(fileForm != null)
                {
                    //ruta carpeta que guarda archivo
                    string path = Server.MapPath("~/Uploads/");

                    //condicion para saber si "Uploads" existe...
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //obtener nombre archivo
                    filePath = path + Path.GetFileName(fileForm.FileName);

                    //Obtener extension archivo
                    string extension = Path.GetExtension(fileForm.FileName);

                    //guardar
                    fileForm.SaveAs(filePath);

                    string csvData = System.IO.File.ReadAllText(filePath);

                    foreach (string row in csvData.Split('\n'))
                    {
                        if (string.IsNullOrEmpty(row))
                        {
                            var newProveedor = new proveedor
                            {
                                nombre = row.Split(';')[0],
                                direccion = row.Split(';')[1],
                                telefono = row.Split(';')[2],
                                nombre_contacto = row.Split(';')[3]
                            };

                            using (var db = new inventario2021Entities())
                            {
                                db.proveedor.Add(newProveedor);
                                db.SaveChanges();
                            }
                        }
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

    }
}