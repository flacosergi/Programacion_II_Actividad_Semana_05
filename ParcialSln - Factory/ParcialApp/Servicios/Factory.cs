using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcialApp.Acceso_a_datos;

namespace ParcialApp.Servicios
{
    public abstract class Factory
    {
        public abstract DaoOrden CrearDaoOrden();
        public abstract GestorOrden CrearGestorOrden();
        public abstract DaoMaterial CrearDaoMaterial();
        public abstract GestorMaterial CrearGestorMaterial();
    }

   
}
