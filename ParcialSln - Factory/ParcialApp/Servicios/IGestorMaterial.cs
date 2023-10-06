using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Servicios
{
    public interface IGestorMaterial
    {
        List<Material> CargarMateriales();
    }
}
