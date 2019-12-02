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
    public class tblCursosController : ApiController
    {
     private Entities dbContext = new Entities();

        /// <summary>
        /// Metodo para obtener todos los cursos creados
        /// </summary>
        /// <returns>Devuelve un objeto JSON con los cursos</returns>
        [HttpGet]
        public IEnumerable<Curso> Get()
        {
            using (Entities cursosEntities = new Entities())
            {
                return cursosEntities.Cursos.ToList();
            }
        }


        /// <summary>
        /// Metodo para obtener todos los cursos y sus lecciones relaccionadas
        /// </summary>
        /// <returns>Devuelve un objeto JSON con los cursos y sus lecciones</returns>
        [HttpGet]
        public List<cursoleccionDTO> getcursoleccion(int iIdCurso)
        {
            using (Entities leccionesEntities = new Entities())
            {

                var query = from relcursolec in leccionesEntities.RelCursoLeccions
                            join curso in leccionesEntities.Cursos on relcursolec.iIdCurso equals curso.iIdCurso
                            join lec in leccionesEntities.Lecciones on relcursolec.iIdLeccion equals lec.iIdLeccion
                            where relcursolec.iIdCurso == iIdCurso
                            select new cursoleccionDTO{
                                curso = curso,
                                lstLecciones = (from relcursolec in leccionesEntities.RelCursoLeccions
                                                join lec in leccionesEntities.Lecciones on relcursolec.iIdLeccion equals lec.iIdLeccion where relcursolec.iIdCurso == iIdCurso select lec).ToList()
                            };

                return query.ToList();
            }
        }


        /// <summary>
        /// METODO PARA CREAR CURSOS
        /// </summary>
        /// <param name="_curso"></param>
        /// <returns>RETORNA EL CURSO INSERTADO</returns>
        [HttpPost]
        
        public IHttpActionResult crearCurso([FromBody]Curso _curso) {

            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Cursos.Add(_curso);
                    dbContext.SaveChanges();
                    return Ok(_curso);
                }
                else { return BadRequest();  }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// METODO PARA MODIFICAR CURSOS
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_curso"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult modificarCurso(int _id, [FromBody]Curso _curso)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var lExiste = dbContext.Cursos.Count(x => x.iIdCurso == _id) > 0;
                    if (lExiste)
                    {
                        dbContext.Entry(_curso).State = System.Data.Entity.EntityState.Modified;
                        dbContext.SaveChanges();

                        return Ok(_curso);
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

        [HttpDelete]
        public IHttpActionResult EliminarCurso(int _id)
        {

            try
            {
                    var curso = dbContext.Cursos.Find(_id);
                    if (curso != null)
                    {
                        dbContext.Cursos.Remove(curso);
                        dbContext.SaveChanges();

                        return Ok(curso);
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
