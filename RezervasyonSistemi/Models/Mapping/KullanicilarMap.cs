using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RezervasyonSistemi.Models.Mapping
{
    public class KullanicilarMap : EntityTypeConfiguration<Kullanicilar>
    {
        public KullanicilarMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Isim)
                .HasMaxLength(50);

            this.Property(t => t.Soyisim)
                .HasMaxLength(50);

            this.Property(t => t.Mail)
                .HasMaxLength(50);

            this.Property(t => t.TelefonNumarasi)
                .HasMaxLength(50);

            this.Property(t => t.KullaniciAdi)
                .HasMaxLength(50);

            this.Property(t => t.Sifre)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kullanicilar");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Isim).HasColumnName("Isim");
            this.Property(t => t.Soyisim).HasColumnName("Soyisim");
            this.Property(t => t.Mail).HasColumnName("Mail");
            this.Property(t => t.TelefonNumarasi).HasColumnName("TelefonNumarasi");
            this.Property(t => t.KullaniciAdi).HasColumnName("KullaniciAdi");
            this.Property(t => t.Sifre).HasColumnName("Sifre");
            this.Property(t => t.OlusturmaTarihi).HasColumnName("OlusturmaTarihi");
            this.Property(t => t.KullaniciTipi).HasColumnName("KullaniciTipi");
        }
    }
}
