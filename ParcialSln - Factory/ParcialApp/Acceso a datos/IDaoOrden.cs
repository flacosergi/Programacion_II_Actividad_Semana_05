using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos
{
    public interface IDaoOrden
    {
        int GuardaOrden(Orden_Retiro orden);
    }
}
