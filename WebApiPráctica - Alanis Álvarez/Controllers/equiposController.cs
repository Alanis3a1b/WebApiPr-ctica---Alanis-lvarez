using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPráctica___Alanis_Álvarez.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiPráctica___Alanis_Álvarez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public equiposController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        //Método para leer todos los registros
        //[HttpGet]
        //[Route("GetAll")]

        //public IActionResult Get()
        //{
        //    List<equipos> listadoEquipo = (from e in _equiposContexto.equipos
        //                                   select e).ToList();

        //    if (listadoEquipo.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(listadoEquipo);
        //}

        //Método con los JOIN. Obviamente realizar para cada tabla a utilizar sus parámetros, contexto, inyección de cada tabla y
        //controlador (si nos piden crearle métodos a dicha tabla)
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            var listadoEquipo = (from e in _equiposContexto.equipos
                                           join t in _equiposContexto.tipo_equipo
                                                  on e.id_tipo_equipo equals t.id_tipo_equipo
                                           join m in _equiposContexto.marcas
                                                  on e.id_marca equals m.id_marcas
                                           join es in _equiposContexto.estados_equipo
                                                  on e.id_estados_equipo equals es.id_estados_equipo
                                         select new
                                         {
                                             //Aquí definimos los campos a seleccionar, con su respectivo alias que indica
                                             //la tabla donde corresponden (ejemplo: e.id_equipos, traera todos los id de la tabla equipos)
                                             e.id_equipos,
                                             e.nombre,
                                             e.descripcion,
                                             e.id_tipo_equipo,
                                             tipo_equipo = t.descripcion,
                                             e.id_marca,
                                             //las variable igualadas son los nombres de campo en como quiere mostrarse
                                             //(Ejemplo marca = "Samsung")
                                             marca = m.nombre_marca,
                                             e.id_estados_equipo,
                                             estados_equipos = es.id_estados_equipo,
                                             estado_equipo = es.descripcion,
                                             //Aquí vamos a hacer un campo compuesto...
                                             detalle = $"Tipo: { t.descripcion}, Marca { m.nombre_marca}, Estado Equipo { es.descripcion} ",
                                             e.estado
                                         }).ToList();

            /*Por defecto, termina en ".ToList();"*/

            /*Si se quiere elegir solo un número específico de filas, se utiliza el TOP N,
              por ejemplo, el top 10 de productos más vendidos, etc. 
              para ello se agrega al final... "}).Take(N).ToList();", donde N es el número específico de filas
             */

            /*Para saltar registros es SKIP N, así: "}).Skip(N).Take(10).ToList();", donde N es el número de registrosa a saltar*/

            /*Por último, para hacer una consulta ordenada por otro campo (en este ejemplo id_estadosS_equipo), se utiliza OrderBy:
              "}).OrderBy(resultado => resultado.id_estado_equipo).ToList();"
            
             Si se quiere que sea utilizar más campos utilizamos el ThenBy o ThenByDescending:
              "}).OrderBy(resultado => resultado.id_estados_equipo).ThenBy(resultado => resultado.id_marca).ThenByDescending(resultado => resultado.id_tipo_equipo).ToList();"
              
            */

            if (listadoEquipo.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipo);
        }


        //Método para buscar por ID
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id) 
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();
            
            if (equipo == null) 
            {
                return NotFound();
            
            }

            return Ok(equipo);
        }

        //Buscar por descripción
        [HttpGet]
        [Route("Find/{descripcion}")]

        public IActionResult Get(string filtro) 
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.descripcion.Contains(filtro)
                               select e).FirstOrDefault();
            if (equipo == null) 
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        //Método para crear o insertar registros
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEquipo([FromBody] equipos equipo)
        {
            try
            {
                _equiposContexto.equipos.Add(equipo);
                _equiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para modificar los registros de la tabla
        [HttpPut]
        [Route("Actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] equipos equipoModificar)
        {
            equipos? equipoActual = (from e in _equiposContexto.equipos
                                     where e.id_equipos == id
                                     select e).FirstOrDefault();

            if (equipoActual == null) 
            { return NotFound(); }

            equipoActual.nombre = equipoModificar.nombre;
            equipoActual.descripcion = equipoModificar.descripcion;
            equipoActual.id_marca = equipoModificar.id_marca;
            equipoModificar.id_tipo_equipo= equipoModificar.id_tipo_equipo;
            equipoModificar.anio_compra = equipoModificar.anio_compra;
            equipoModificar.costo = equipoModificar.costo;

            _equiposContexto.Entry(equipoActual).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(equipoModificar);

            

        }

        [HttpPut]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
            { return NotFound(); }

            _equiposContexto.equipos.Attach(equipo);
            _equiposContexto.equipos.Remove(equipo);
            _equiposContexto.SaveChanges();

            return Ok(equipo);

        }

    }



}
