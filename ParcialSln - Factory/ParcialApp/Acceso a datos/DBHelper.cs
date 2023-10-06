using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer;

namespace ParcialApp.Acceso_a_datos
{
    public class DBHelper
    {
        SqlConnection conexion = null;
        SqlCommand comando = null;

        private static DBHelper instancia = null;

        public DBHelper()
        {
            conexion = new SqlConnection(Properties.Resources.cadena_conexion);
            comando = new SqlCommand();
            comando.Connection = conexion;

        }

        public static DBHelper ObstenerInstacia()
        {
            if (instancia == null)
                instancia = new DBHelper();
            return instancia;

        }

        public void AbreConexionConTransaccion()
        {
            try
            {
                conexion.Open();
                comando.Transaction = conexion.BeginTransaction();
            }
            catch
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public void CierraConexionConTransaccion()
        {
            try
            {
                comando.Transaction.Commit();
                conexion.Close();
            }
            catch
            {
                if (comando.Transaction != null)
                    comando.Transaction.Rollback();
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public DataTable Consultar(string SP)
        {
            try
            {
                DataTable Tabla = new DataTable();
                comando.CommandText = SP;
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                Tabla.Load(comando.ExecuteReader());
                conexion.Close();
                return Tabla;
            }
            catch
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
                return null;
            }

        }

        public int CargaRegistro(string SP, List<SqlParameter> lista_param, SqlParameter salida)
        {
            try
            {
                comando.CommandText = SP;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                if (salida != null)
                    comando.Parameters.Add(salida);
                foreach (SqlParameter param in lista_param)
                {
                    comando.Parameters.Add(param);
                }
                comando.ExecuteNonQuery();
                if (salida != null)
                    return (int)salida.Value;
                else
                    return 0;
            }
            catch
            {
                if (comando.Transaction != null)
                    comando.Transaction.Rollback();
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
                return 0;
            }
        }
    }
}
