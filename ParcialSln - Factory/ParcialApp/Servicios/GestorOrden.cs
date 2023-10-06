using ParcialApp.Acceso_a_datos;
using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Servicios
{
    public class GestorOrden: IGestorOrden
    {
        private IDaoOrden dao_orden;
        public GestorOrden(GestorFactory factoria)
        {
            dao_orden = factoria.CrearDaoOrden();
        }

        public int CargarOrden(Orden_Retiro orden)
        {
            return dao_orden.GuardaOrden(orden);

        }
       
    }
}
