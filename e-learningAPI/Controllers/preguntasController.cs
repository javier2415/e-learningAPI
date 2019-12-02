using coneccion;
using e_learningAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace e_learningAPI.Controllers
{
    public class preguntasController : ApiController
    {

        private Entities dbContext = new Entities();

        /// <summary>
        /// Metodo para obtener todas las preguntas
        /// </summary>
        /// <returns>Devuelve un objeto JSON con los cursos</returns>
        [HttpGet]
        public IEnumerable<pregunta> Get()
        {
            using (Entities preguntaEntities = new Entities())
            {
                return preguntaEntities.preguntas.ToList();
            }
        }

        /// <summary>
        /// METODO PARA CREAR PREGUNTAS CON SUS RESPUESTAS
        /// </summary>
        /// <param name="_pregunta"></param>
        /// <returns>RETORNA LA PREGUNTA INSERTADA</returns>
        [HttpPost]

        public IHttpActionResult crearPregunta([FromBody]entidadguardadoPregunta _pregunta)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.preguntas.Attach(_pregunta.entidadPregunta);
                    dbContext.ConfigRespuestas.AddRange(_pregunta.respuestasPregunta);
                    dbContext.SaveChanges();
                    return Ok(_pregunta);
                }
                else { return BadRequest(); }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// METODO PARA MODIFICAR PREGUNTA
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_pregunta"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult modificarPregunta(int _id, [FromBody]entidadguardadoPregunta _pregunta)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var lExiste = dbContext.preguntas.Count(x => x.iIdPregunta == _id) > 0;
                    if (lExiste)
                    {
                        dbContext.Entry(_pregunta.entidadPregunta).State = System.Data.Entity.EntityState.Modified;
                        dbContext.Entry(_pregunta.respuestasPregunta).State = System.Data.Entity.EntityState.Modified;
                        dbContext.SaveChanges();

                        return Ok(_pregunta);
                    }
                    else { return NotFound(); }
                }
                else { return BadRequest(); }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Metodo para eliminar preguntas
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult EliminarPregunta(int _id)
        {

            try
            {
                var pregunta = dbContext.preguntas.Find(_id);
                if (pregunta != null)
                {

                    var respuestas = dbContext.ConfigRespuestas.ToList().Where(x => x.iIdPregunta == _id);
                    dbContext.preguntas.Remove(pregunta);
                    
                    if (respuestas.Count() > 0)
                    {
                        dbContext.ConfigRespuestas.RemoveRange(respuestas);
                    }
                 
                    dbContext.SaveChanges();

                    return Ok(pregunta);
                }
                else { return NotFound(); }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
