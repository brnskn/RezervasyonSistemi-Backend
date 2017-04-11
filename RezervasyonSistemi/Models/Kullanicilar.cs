using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RezervasyonSistemi.Models
{
    public partial class Kullanicilar
    {
        public Kullanicilar()
        {
            this.Isletmelers = new List<Isletmeler>();
            this.RezervasyonTalepleris = new List<RezervasyonTalepleri>();
        }

        public int ID { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string Mail { get; set; }
        public string TelefonNumarasi { get; set; }
        public string KullaniciAdi { get; set; }
        [JsonIgnore]
        public string Sifre { get; set; }
        public Nullable<System.DateTime> OlusturmaTarihi { get; set; }
        public Nullable<bool> KullaniciTipi { get; set; }
        [JsonIgnore]
        public virtual ICollection<Isletmeler> Isletmelers { get; set; }
        [JsonIgnore]
        public virtual ICollection<RezervasyonTalepleri> RezervasyonTalepleris { get; set; }
    }
}
