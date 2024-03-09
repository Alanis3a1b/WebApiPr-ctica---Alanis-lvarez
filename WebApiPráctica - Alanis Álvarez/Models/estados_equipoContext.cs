using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace WebApiPráctica___Alanis_Álvarez.Models
{
    public class estados_equipoContext : DbContext
    {
        public estados_equipoContext(DbContextOptions<estados_equipoContext> options) : base(options)
        {

        }

        public DbSet<estados_equipo> estados_equipos { get; set; }
    }
}
