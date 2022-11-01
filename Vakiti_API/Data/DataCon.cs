using Microsoft.EntityFrameworkCore;

namespace Vakiti_API.Data
{
    public class DataCon: DbContext
    {
        public DataCon(DbContextOptions <DataCon> options): base(options)
        {

        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
