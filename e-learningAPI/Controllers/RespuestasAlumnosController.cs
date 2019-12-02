using System;
using System.Collections.Generic;
using coneccion;
using e_learningAPI.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace e_learningAPI.Controllers
{
    public class RespuestasAlumnosController : ApiController
    {

        private Entities dbContext = new Entities();


        /// <summary>
        /// METODO PARA GUARDAR LAS RESPUESTAS DE LOS ALUMNOS
        /// </summary>
        /// <param name="_respuestas"></param>
        /// <returns>RETORNA LAS RESPUESTAS GUARDADAS</returns>
        [HttpPost]

        public IHttpActionResult guardarRespuestas([FromBody]entidadguardadoRespuestaAlumno _respuestas)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var respuesta in _respuestas.lstRespuestasAlumno)
                    {
                        dbContext.RespuestasAlumnos.Add(
                                                        new RespuestasAlumno
                                                        {
                                                            iIdAlumno = _respuestas.iIdAlumno,
                                                            iIdCurso = _respuestas.iIdCurso,
                                                            iIdLeccion = respuesta.entidadPregunta.iIdLeccion,
                                                            iIdPregunta = respuesta.entidadPregunta.iIdPregunta,
                                                            iIdConfigRespuesta = respuesta.respuestasPregunta.FirstOrDefault().iIdConfigRespuesta
                                                                
                                                        });
                    }
                    dbContext.SaveChanges();
                    return Ok(_respuestas);

                }
                else { return BadRequest(); }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
