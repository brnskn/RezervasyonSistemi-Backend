using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RezervasyonSistemi.Models.Mapping
{
    public class PlanDetaylariMap : EntityTypeConfiguration<PlanDetaylari>
    {
        public PlanDetaylariMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("PlanDetaylari");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.HaftaninGunu).HasColumnName("HaftaninGunu");
            this.Property(t => t.BaslangicSaati).HasColumnName("BaslangicSaati");
            this.Property(t => t.BitisSaati).HasColumnName("BitisSaati");
            this.Property(t => t.PlanID).HasColumnName("PlanID");

            // Relationships
            this.HasOptional(t => t.Planlar)
                .WithMany(t => t.PlanDetaylaris)
                .HasForeignKey(d => d.PlanID);

        }
    }
}
