using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace WebApiPráctica___Alanis_Álvarez.Models
{
    public class tipo_equipoContext : DbContext
    {
        public tipo_equipoContext(DbContextOptions<tipo_equipoContext> options) : base(options)
        {

        }

        public DbSet<tipo_equipo> tipo_equipo { get; set; }
    }
}
