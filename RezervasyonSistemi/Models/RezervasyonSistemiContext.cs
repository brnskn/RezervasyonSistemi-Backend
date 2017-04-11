using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using RezervasyonSistemi.Models.Mapping;

namespace RezervasyonSistemi.Models
{
    public partial class RezervasyonSistemiContext : DbContext
    {
        static RezervasyonSistemiContext()
        {
            Database.SetInitializer<RezervasyonSistemiContext>(null);
        }

        public RezervasyonSistemiContext()
            : base("Name=RezervasyonSistemiContext")
        {
        }

        public DbSet<Isletmeler> Isletmelers { get; set; }
        public DbSet<KatBilgileri> KatBilgileris { get; set; }
        public DbSet<Kullanicilar> Kullanicilars { get; set; }
        public DbSet<MasaBilgileri> MasaBilgileris { get; set; }
        public DbSet<RezervasyonTalepleri> RezervasyonTalepleris { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IsletmelerMap());
            modelBuilder.Configurations.Add(new KatBilgileriMap());
            modelBuilder.Configurations.Add(new KullanicilarMap());
            modelBuilder.Configurations.Add(new MasaBilgileriMap());
            modelBuilder.Configurations.Add(new RezervasyonTalepleriMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
