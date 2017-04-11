using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RezervasyonSistemi.Models.Mapping
{
    public class IsletmelerMap : EntityTypeConfiguration<Isletmeler>
    {
        public IsletmelerMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.IsletmeIsmi)
                .HasMaxLength(50);

            this.Property(t => t.IsletmeAdresi)
                .HasMaxLength(150);

            this.Property(t => t.IsletmeAciklamasi)
                .HasMaxLength(50);

            this.Property(t => t.IsletmeNumarasi)
                .HasMaxLength(50);

            this.Property(t => t.IsletmeKrokiResim)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Isletmeler");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IsletmeIsmi).HasColumnName("IsletmeIsmi");
            this.Property(t => t.IsletmeAdresi).HasColumnName("IsletmeAdresi");
            this.Property(t => t.IsletmeAciklamasi).HasColumnName("IsletmeAciklamasi");
            this.Property(t => t.IsletmeNumarasi).HasColumnName("IsletmeNumarasi");
            this.Property(t => t.KullaniciID).HasColumnName("KullaniciID");
            this.Property(t => t.IsletmeKrokiResim).HasColumnName("IsletmeKrokiResim");

            // Relationships
            this.HasOptional(t => t.Kullanicilar)
                .WithMany(t => t.Isletmelers)
                .HasForeignKey(d => d.KullaniciID);

        }
    }
}
