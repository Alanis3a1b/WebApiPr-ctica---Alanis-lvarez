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

        //Prueba enviado a repositorio 

    }



}
