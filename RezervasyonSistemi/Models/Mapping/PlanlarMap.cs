using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RezervasyonSistemi.Models.Mapping
{
    public class PlanlarMap : EntityTypeConfiguration<Planlar>
    {
        public PlanlarMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.PlanIsmi)
                .HasMaxLength(50);

            this.Property(t => t.PlanAciklamasi)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Planlar");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.PlanIsmi).HasColumnName("PlanIsmi");
            this.Property(t => t.PlanAciklamasi).HasColumnName("PlanAciklamasi");
            this.Property(t => t.IsletmeID).HasColumnName("IsletmeID");

            // Relationships
            this.HasOptional(t => t.Isletmeler)
                .WithMany(t => t.Planlars)
                .HasForeignKey(d => d.IsletmeID);

        }
    }
}
