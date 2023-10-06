using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcialApp.Acceso_a_datos;

namespace ParcialApp.Servicios
{
    public class GestorFactory : Factory
    {
        public override DaoMaterial CrearDaoMaterial()
        {
            return new DaoMaterial();
        }

        public override DaoOrden CrearDaoOrden()
        {
            return new DaoOrden();
        }

        public override GestorMaterial CrearGestorMaterial()
        {
            return new GestorMaterial(this);
        }

        public override GestorOrden CrearGestorOrden()
        {
            return new GestorOrden(this);
        }
    }
}
