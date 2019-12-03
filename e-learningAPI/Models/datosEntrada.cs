using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learningAPI.Models
{
    public class datosEntrada
    {
        public int cantidadPruebas { get; set; }
        public List<matriz> lstTamanosMatriz { get; set; }
    }

    public class matriz
    {
        public int N { get; set; }
        public int M { get; set; }
    }
}