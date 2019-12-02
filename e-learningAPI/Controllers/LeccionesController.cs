using coneccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace e_learningAPI.Controllers
{
    public class LeccionesController : ApiController
    {

        private Entities dbContext = new Entities();

        /// <summary>
        /// Metodo para obtener todas las lecciones creadas
        /// </summary>
        /// <returns>Devuelve un objeto JSON con los cursos</returns>
        [HttpGet]
        public IEnumerable<Leccione> Get()
        {
            using (Entities leccionesEntities = new Entities())
            {
                return leccionesEntities.Lecciones.ToList();
            }
        }


        /// <summary>
        /// Metodo para obtener todas las lecciones creadas
        /// </summary>
        /// <returns>Devuelve un objeto JSON con los cursos</returns>
        [HttpGet]
        public List<Leccione> getleccionesxcurso(int iIdCurso)
        {
            using (Entities leccionesEntities = new Entities())
            {

                var query = from relcursolec in leccionesEntities.RelCursoLeccions
                            join lec in leccionesEntities.Lecciones on relcursolec.iIdLeccion equals lec.iIdLeccion
                            where relcursolec.iIdCurso == iIdCurso
                            select lec;

                return query.ToList();
            }
        }


        /// <summary>
        /// METODO PARA CREAR LECCIONES
        /// </summary>
        /// <param name="_leccion"></param>
        /// <returns>RETORNA LA LECCION INSERTADA</returns>
        [HttpPost]

        public IHttpActionResult crearleccion([FromBody]Leccione _leccion)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Lecciones.Add(_leccion);
                    dbContext.SaveChanges();
                    return Ok(_leccion);
                }
                else { return BadRequest(); }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// METODO PARA MODIFICAR LECCIONES
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_leccion"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult modificarleccion(int _id, [FromBody]Leccione _leccion)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var lExiste = dbContext.Lecciones.Count(x => x.iIdLeccion == _id) > 0;
                    if (lExiste)
                    {
                        dbContext.Entry(_leccion).State = System.Data.Entity.EntityState.Modified;
                        dbContext.SaveChanges();

                        return Ok(_leccion);
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
        /// Metodo para eliminar lecciones
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Eliminarleccion(int _id)
        {

            try
            {
                var leccion = dbContext.Lecciones.Find(_id);
                if (leccion != null)
                {
                    dbContext.Lecciones.Remove(leccion);
                    dbContext.SaveChanges();

                    return Ok(leccion);
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
