using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParcialApp.Acceso_a_datos
{
    public class DaoMaterial : IDaoMaterial
    {
        public List<Material> GetMateriales()
        {
            List<Material> nueva_lista = new List<Material>();
            DataTable tabla = DBHelper.ObstenerInstacia().Consultar("SP_CONSULTAR_MATERIALES");
            foreach (DataRow fila in tabla.Rows)
            {
                Material mat = new Material();
                mat.codigo = (int)fila["codigo"];
                mat.nombre = (string)fila["nombre"];
                mat.stock = (decimal)fila["stock"];
                nueva_lista.Add(mat);
            }
            return nueva_lista;
        }
    }
}
