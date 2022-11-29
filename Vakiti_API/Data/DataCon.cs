using Microsoft.EntityFrameworkCore;
using Vakiti_API.Models;

namespace Vakiti_API.Data
{
    public class DataCon: DbContext
    {
        public DataCon(DbContextOptions <DataCon> options): base(options)
        {

        }

        public DbSet<CorsOrigin> corsOrigins { get; set; }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
