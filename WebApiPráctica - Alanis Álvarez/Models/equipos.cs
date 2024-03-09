using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApiPráctica___Alanis_Álvarez.Models
{
    public class equipos
    {
        [Key]

        public int id_equipos { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int? tipo_equipo_id { get; set; }
        public int? marca_id {  get; set; }
        public string modelo { get; set; }
        public int? anio_compra {  get; set; }
        public decimal costo { get; set; }
        public int? vida_util {  get; set; }
        public int? estado_equipo_id { get; set;}
        public string estado { get; set;}

    }

    public class marcas
    {
        [Key]
        public int id_marcas { get; set; }
        public string nombre_marca { get; set; }
        public string estados { get; set; }

    }

    public class estados_equipo
    {
        [Key]
        public int id_estados_equipos{ get; set; }
        public string descripcion { get; set; }
        public string estados { get; set; }

    }

    public class tipo_equipo
    {
        [Key]
        public int id_tipo_equipo{ get; set; }
        public string descripcion { get; set; }
        public string estados { get; set; }

    }




}
