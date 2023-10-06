using ParcialApp.Acceso_a_datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcialApp.Dominio;


namespace ParcialApp.Servicios
{
    public class GestorMaterial: IGestorMaterial
    {
        private IDaoMaterial DaoMat;
        public GestorMaterial(GestorFactory factoria)
        {
            DaoMat = factoria.CrearDaoMaterial();
        }
       

        public List<Material> CargarMateriales()
        {
            return DaoMat.GetMateriales();
        }

    }
}
