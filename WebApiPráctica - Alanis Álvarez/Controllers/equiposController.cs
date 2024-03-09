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

        ////Método para leer todos los registros
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

        //Método con los join. Obviamente realizar para cada tabla a utilizar sus parámetros, contexto, inyección de cada tabla y
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
                                     e.id_equipos,
                                     e.nombre,
                                     e.descripcion,
                                     e.id_tipo_equipo,
                                     tipo_equipo = t.descripcion,
                                     e.id_marca,
                                     marca = m.nombre_marca,
                                     e.id_estados_equipo,
                                     estados_equipos = es.id_estados_equipo,
                                     e.estado
                                 }).ToList();

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
            equipoModificar.id_marca = equipoModificar.id_marca;
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
