using e_learningAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace e_learningAPI.Controllers
{
    public class Ejercicio2Controller : ApiController
    {
        /// <summary>
        /// EJERCICIO 2 RECORRIDO DE MATRIZ
        /// </summary>
        /// <param name="_datosEntrada"></param>
        /// <returns>RETORNA LA POSICION FINAL DEL RECORRIDO</returns>
        [HttpPost]
        public IHttpActionResult ejecutar([FromBody]datosEntrada _datosEntrada)
        {

            try
            {
                if (ModelState.IsValid)

                {
                    if ((_datosEntrada.cantidadPruebas >= 1) && (_datosEntrada.cantidadPruebas <= 5000) && (_datosEntrada.cantidadPruebas <= _datosEntrada.lstTamanosMatriz.Count))
                    {
                        string mensajeError = "El parametro cantidadPruebas debe cumplir las siguientes caracteristicas: (1 <= T <= 5000) y debe ser menor o igual a la cantidad de casos de prueba";
                        return BadRequest(mensajeError);
                    }

                        var respuesta = "";
                        string[] posicion = {"R","D", "L", "U", "R" };
                    
                        for (int j = 0; j < _datosEntrada.cantidadPruebas; j++)
                        {
                        
                        //variables para controlar la posicion actual del movimiento.
                        var contadorPosiciones = 1;
                        var matrix = new string[_datosEntrada.lstTamanosMatriz[j].N, _datosEntrada.lstTamanosMatriz[j].M];
                        
                        //variables para indicar el inicio del recorrido
                        int cordenanaInicial = 0; 
                        int cordenadaFinal = 0;

                        //ciclo para poder reiniciar las vueltas segun sea necesario
                        while (cordenanaInicial < _datosEntrada.lstTamanosMatriz[j].N && cordenadaFinal < _datosEntrada.lstTamanosMatriz[j].M)
                        {
                            //recorrido para iniciar los cambios de posicion (simpre es un paso a la derecha)
                            for (int i = cordenadaFinal; i < _datosEntrada.lstTamanosMatriz[j].M; ++i)
                            {
                                matrix[cordenanaInicial, cordenadaFinal] = (contadorPosiciones <= 4 ? (posicion[0]) : "0");
                                contadorPosiciones = 0;
                            }
                            cordenanaInicial++;

                            //recorrido para detectar el final de las columnas y realizar el movimiento hacia abajo
                            for (int i = cordenanaInicial; i < _datosEntrada.lstTamanosMatriz[j].N; ++i)
                            {
                                matrix[cordenanaInicial - 1, cordenadaFinal] = (contadorPosiciones <= 4 ? (posicion[1]) : "0");
                                contadorPosiciones = 1;
                            }
                            _datosEntrada.lstTamanosMatriz[j].M--;

                           //recorrido para detectar el final de las filas, y dar un giro hacia la izquierda
                            if (cordenanaInicial < _datosEntrada.lstTamanosMatriz[j].N)
                            {
                                for (int i = _datosEntrada.lstTamanosMatriz[j].M - 1; i >= cordenadaFinal; --i)
                                {
                                    matrix[cordenanaInicial - 1, cordenadaFinal] = (contadorPosiciones <= 4 ? (posicion[2]) : "0");
                                    contadorPosiciones = 2;
                                }
                                _datosEntrada.lstTamanosMatriz[j].N--;
                            }

                            //recorrido para detectar el inicio de cada columna, en caso de tener que iniciar otra vez los recorridos, (giro hacia arriba)
                            if (cordenadaFinal < _datosEntrada.lstTamanosMatriz[j].M)
                            {
                                for (int i = _datosEntrada.lstTamanosMatriz[j].N - 1; i >= cordenanaInicial; --i)
                                {
                                    matrix[cordenanaInicial - 1, cordenadaFinal] = (contadorPosiciones <= 4 ? (posicion[3]) : "0");
                                    contadorPosiciones = 3;
                                }
                                cordenadaFinal++;
                            }
                        }

                        //creacion de la respuesta con los recorridos realizados tomando la ultima posicion del arreglo
                        List<string> lstdatos = new List<string>();
                        foreach (var item in matrix)
                        {
                            
                            if (item != null)
                            {
                                lstdatos.Add(item);
                            }
                            
                        }
                            respuesta += lstdatos.LastOrDefault() + ",";
                        }


                    return Ok(respuesta.Split(','));
                }
                else { return BadRequest(); }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
