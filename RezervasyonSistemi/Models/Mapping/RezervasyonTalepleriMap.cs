using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RezervasyonSistemi.Models.Mapping
{
    public class RezervasyonTalepleriMap : EntityTypeConfiguration<RezervasyonTalepleri>
    {
        public RezervasyonTalepleriMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("RezervasyonTalepleri");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.KullaniciID).HasColumnName("KullaniciID");
            this.Property(t => t.MasaID).HasColumnName("MasaID");
            this.Property(t => t.OnayDurumu).HasColumnName("OnayDurumu");
            this.Property(t => t.Tarih).HasColumnName("Tarih");
            this.Property(t => t.PlanDetaylariID).HasColumnName("PlanDetaylariID");

            // Relationships
            this.HasOptional(t => t.Kullanicilar)
                .WithMany(t => t.RezervasyonTalepleris)
                .HasForeignKey(d => d.KullaniciID);
            this.HasOptional(t => t.MasaBilgileri)
                .WithMany(t => t.RezervasyonTalepleris)
                .HasForeignKey(d => d.MasaID);
            this.HasOptional(t => t.PlanDetaylari)
                .WithMany(t => t.RezervasyonTalepleris)
                .HasForeignKey(d => d.PlanDetaylariID);

        }
    }
}
