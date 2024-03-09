using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace WebApiPráctica___Alanis_Álvarez.Models
{
    public class marcasContext : DbContext
    {
        public marcasContext(DbContextOptions<marcasContext> options) : base(options)
        {

        }

        //public DbSet<marcas> marcas { get; set; }
    }
}
