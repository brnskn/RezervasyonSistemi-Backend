using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RezervasyonSistemi.Models.Mapping
{
    public class KatBilgileriMap : EntityTypeConfiguration<KatBilgileri>
    {
        public KatBilgileriMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.KatIsmi)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("KatBilgileri");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.KatIsmi).HasColumnName("KatIsmi");
            this.Property(t => t.IsletmeID).HasColumnName("IsletmeID");

            // Relationships
            this.HasOptional(t => t.Isletmeler)
                .WithMany(t => t.KatBilgileris)
                .HasForeignKey(d => d.IsletmeID);

        }
    }
}
