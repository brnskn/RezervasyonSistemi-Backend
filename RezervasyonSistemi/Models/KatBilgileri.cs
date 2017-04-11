using System;
using System.Collections.Generic;

namespace RezervasyonSistemi.Models
{
    public partial class KatBilgileri
    {
        public KatBilgileri()
        {
            this.MasaBilgileris = new List<MasaBilgileri>();
        }

        public int ID { get; set; }
        public string KatIsmi { get; set; }
        public Nullable<int> IsletmeID { get; set; }
        public virtual Isletmeler Isletmeler { get; set; }
        public virtual ICollection<MasaBilgileri> MasaBilgileris { get; set; }
    }
}
