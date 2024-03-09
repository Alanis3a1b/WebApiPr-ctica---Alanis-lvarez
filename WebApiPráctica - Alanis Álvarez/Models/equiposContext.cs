using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace WebApiPráctica___Alanis_Álvarez.Models
{
    public class equiposContext : DbContext
    {
        public equiposContext(DbContextOptions<equiposContext> options) : base(options) 
        { 
        
        }

        /*Tablas para hacer los joins metidas en un solo contexto*/
        public DbSet<equipos> equipos { get; set; }

        public DbSet<marcas> marcas { get; set; }

        public DbSet<tipo_equipo> tipo_equipo { get; set; }

        public DbSet<estados_equipo> estados_equipo { get; set; }

        /*Comentario*/

    }
}
