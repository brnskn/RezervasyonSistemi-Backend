using Newtonsoft.Json;
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
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<int> PlanDetaylariID { get; set; }
        public virtual Kullanicilar Kullanicilar { get; set; }
        public virtual MasaBilgileri MasaBilgileri { get; set; }
        public virtual PlanDetaylari PlanDetaylari { get; set; }
    }
}
