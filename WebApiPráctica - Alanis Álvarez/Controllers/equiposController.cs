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
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<equipos> listadoEquipo = (from e in _equiposContexto.equipos
                                           select e).ToList();

            if (listadoEquipo.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipo);
        }

        //Método con los joins (Obviamente realizar sus parámetros, contexto, inyección de cada tabla y
        //controlador (si nos piden crearle métodos a dicha tabla)
        [HttpGet]
        [Route("GetAll")]

        public IActionResult GetJoin()
        {
            List<equipos> listadoEquipo = (from e in _equiposContexto.equipos
                                           
                                           select e).ToList();

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
            equipoActual.marca_id = equipoModificar.marca_id;
            equipoModificar.tipo_equipo_id = equipoModificar.tipo_equipo_id;
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
