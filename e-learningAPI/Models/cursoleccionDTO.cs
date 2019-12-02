using coneccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learningAPI.Models
{
    public class cursoleccionDTO
    {
        public Curso curso { get; set; }
        public List<Leccione> lstLecciones { get; set; }
    }
}