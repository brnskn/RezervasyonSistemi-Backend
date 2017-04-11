using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RezervasyonSistemi.Models.Mapping
{
    public class MasaBilgileriMap : EntityTypeConfiguration<MasaBilgileri>
    {
        public MasaBilgileriMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.MasaIsmi)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MasaBilgileri");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.MasaIsmi).HasColumnName("MasaIsmi");
            this.Property(t => t.MasaNumarasi).HasColumnName("MasaNumarasi");
            this.Property(t => t.KatID).HasColumnName("KatID");

            // Relationships
            this.HasOptional(t => t.KatBilgileri)
                .WithMany(t => t.MasaBilgileris)
                .HasForeignKey(d => d.KatID);

        }
    }
}
