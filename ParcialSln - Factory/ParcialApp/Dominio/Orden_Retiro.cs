using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Dominio
{
    public class Orden_Retiro
    {
        public int nro { get; set; }
        public string responsable { get; set; }
        public List<Detalle_Orden> lista_detalle { get; set; }

        public Orden_Retiro()
        {
            nro = 0;
            responsable = string.Empty;
            lista_detalle = new List<Detalle_Orden>();
        }

        public Orden_Retiro(int num, string resp, List<Detalle_Orden> lista_det)
        {
            nro = num;
            responsable = resp;
            lista_detalle = lista_det;
        }
    }
}
