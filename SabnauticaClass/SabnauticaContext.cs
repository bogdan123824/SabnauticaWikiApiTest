using Microsoft.EntityFrameworkCore;

namespace SabnauticaWiki.SabnauticaClass
{
    public class SabnauticaContext : DbContext
    {
        public SabnauticaContext(DbContextOptions<SabnauticaContext> options) : base (options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Fauna> Faunas { get; set; }
        public DbSet<Flora> Floras { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Biome> Biomes { get; set; }
    }
}
