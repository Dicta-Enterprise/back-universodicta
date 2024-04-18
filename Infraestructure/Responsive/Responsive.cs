using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Back_UniversoDicta.Infraestructure.Responsive
{
    public class Responsive
    {
        public dynamic mensaje { get; set; }
        public bool ok { get; set; }


        public Responsive() {}

        public Responsive(bool OK, string Mensaje = "")
        {
            this.mensaje = Mensaje;
            this.ok = OK;
        }
    }
}