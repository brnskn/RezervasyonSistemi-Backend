using System;
using System.Collections.Generic;

namespace RezervasyonSistemi.Models
{
    public partial class RezervasyonTalepleri
    {
        public int ID { get; set; }
        public Nullable<int> KullaniciID { get; set; }
        public Nullable<int> MasaID { get; set; }
        public Nullable<bool> OnayDurumu { get; set; }
        public virtual Kullanicilar Kullanicilar { get; set; }
        public virtual MasaBilgileri MasaBilgileri { get; set; }
    }
}
