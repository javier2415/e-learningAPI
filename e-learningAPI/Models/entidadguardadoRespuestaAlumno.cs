using coneccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learningAPI.Models
{
    public class entidadguardadoRespuestaAlumno
    {
        
        public int iIdAlumno { get; set; }

        public int iIdCurso { get; set; }

        public List<entidadguardadoPregunta> lstRespuestasAlumno{ get; set; }
    }
}