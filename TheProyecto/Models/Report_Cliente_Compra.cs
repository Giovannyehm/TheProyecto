using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheProyecto.Models
{
    public class Report_Cliente_Compra
    {
        public String nombreCliente { get; set; }
        public String documentoCliente { get; set; }
        public System.DateTime fechaCompra { get; set; }
        public int totalCompra { get; set; }
    }
}