using System;
using System.Collections.Generic;

namespace RezervasyonSistemi.Models
{
    public partial class MasaBilgileri
    {
        public MasaBilgileri()
        {
            this.RezervasyonTalepleris = new List<RezervasyonTalepleri>();
        }

        public int ID { get; set; }
        public string MasaIsmi { get; set; }
        public Nullable<int> MasaNumarasi { get; set; }
        public Nullable<int> KatID { get; set; }
        public virtual KatBilgileri KatBilgileri { get; set; }
        public virtual ICollection<RezervasyonTalepleri> RezervasyonTalepleris { get; set; }
    }
}
