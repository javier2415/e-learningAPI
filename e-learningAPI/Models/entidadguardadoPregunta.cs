using coneccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learningAPI.Models
{
    public class entidadguardadoPregunta
    {
        public  pregunta entidadPregunta { get; set; }
        public List<ConfigRespuesta> respuestasPregunta { get; set; }

    }
}