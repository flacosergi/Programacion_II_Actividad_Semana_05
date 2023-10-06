using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos
{
    public class DaoOrden : IDaoOrden
    {
        public int GuardaOrden(Orden_Retiro orden)
        {
            int nueva_orden = 0;
            List<SqlParameter> lista_param = new List<SqlParameter>();
            List<SqlParameter> lista_param_detalle = new List<SqlParameter>();
            lista_param.Add(new SqlParameter("@responsable", orden.responsable));
            SqlParameter salida = new SqlParameter();
            salida.Direction = System.Data.ParameterDirection.Output;
            salida.ParameterName = "nro";
            salida.SqlDbType = System.Data.SqlDbType.Int;
            DBHelper.ObstenerInstacia().AbreConexionConTransaccion();
            nueva_orden = DBHelper.ObstenerInstacia().CargaRegistro("SP_INSERTAR_ORDEN", lista_param, salida);
            foreach (Detalle_Orden detalle in orden.lista_detalle)
            {
                lista_param_detalle.Clear();
                lista_param_detalle.Add(new SqlParameter("@nro_orden", nueva_orden));
                lista_param_detalle.Add(new SqlParameter("@detalle", detalle.detalle_nro));
                lista_param_detalle.Add(new SqlParameter("@codigo", detalle.material.codigo));
                lista_param_detalle.Add(new SqlParameter("@cantidad", detalle.cantidad));
                DBHelper.ObstenerInstacia().CargaRegistro("SP_INSERTAR_DETALLES", lista_param_detalle, null);
            }
            DBHelper.ObstenerInstacia().CierraConexionConTransaccion();
            return nueva_orden;
        }
    }
}
