# e-learningAPI

_api de creacion de cursos, lecciones y cuestionarios_

## Comenzando 🚀

_Estas instrucciones te permitirán obtener una copia del proyecto en funcionamiento en tu máquina local para propósitos de desarrollo y pruebas._


### Pre-requisitos 📋

_realizado con c# y sql server_


### Instalación 🔧
_Es necesario ejecutar el archivo "script" para crear las tablas necesarias
_Se requiere configurar el archivo app.Config Con su propia cadena de conexion para su base de datos sql_



### USO

se crearon los siguientes controladores para cada una de las funcionalidades requeridas:

## Lecciones
_GET api/Lecciones_	
_GET api/Lecciones?iIdCurso={iIdCurso}	(OBTIENE LAS LECCIONES DE UN CURSO EN ESPECIFICO)_
_POST api/Lecciones_	
_PUT api/Lecciones?_id={_id}_	
_DELETE api/Lecciones?_id={_id}_	

## tblCursos
GET api/tblCursos	
GET api/tblCursos?iIdCurso={iIdCurso}	(OBTIENE UN CURSO EN ESPECIFICO)
POST api/tblCursos	
PUT api/tblCursos?_id={_id}	
DELETE api/tblCursos?_id={_id}	

## preguntas
GET api/preguntas	
POST api/preguntas	
PUT api/preguntas?_id={_id}	
DELETE api/preguntas?_id={_id}	

## RespuestasAlumnos
POST api/RespuestasAlumnos (METODO QUE REALIZA EL GUARDADO DE LAS RESPUESTAS DE LOS ALUMNOS AL CONTESTAR UNA LECCION)
ESTRUCTURA JSON QUE RECIBE EL METODO:
            {
              "iIdAlumno": 1,
              "iIdCurso": 2,
              "lstRespuestasAlumno": [
                {
                  "$id": "2",
                  "entidadPregunta": {
                    "$id": "3",
                    "iIdPregunta": 1,
                    "cPregunta": "sample string 2",
                    "iIdLeccion": 1,
                    "lAplicanTodas": true,
                    "iPuntaje": 1
                  },
                  "respuestasPregunta": [
                    {
                      "$id": "4",
                      "iIdConfigRespuesta": 1,
                      "iIdPregunta": 1,
                      "lEsCorrecta": true
                    },
                    {
                      "$ref": "4"
                    }
                  ]
                },
                {
                  "$ref": "2"
                }
              ]
            }


# EJERCICIO 2

_se creo un metodo para generar toda lo logica que se solicitaba en el ejercicio 2, para ahorrar tiempo en la creacion de otro proyecto_

## Comenzando 🚀

_se ejecuta igual que la api anterior,es un metodo que recibe los siguientes datos:
{
  "cantidadPruebas": 4,
  "lstTamanosMatriz": [
{
      "$id": "1",
      "N":1,
      "M":1
    },
    {
      "$id": "2",
      "N":2,
      "M":2
    },
    {
      "$id": "3",
      "N":3,
      "M":1
    }
,
    {
      "$id": "4",
      "N":3,
      "M":3
    }
  ]
}
_

_RETORNA UNA LISTA CON LAS POSICIONES SEGUN EL ORDEN COMO SE ENVIAN LAS PRUEBAS PARA ESTE CASO POR EJEMPLO:_
{
R
L
D
R
}
