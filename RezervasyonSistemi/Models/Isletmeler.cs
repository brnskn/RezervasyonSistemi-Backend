using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RezervasyonSistemi.Models
{
    public partial class Isletmeler
    {
        public Isletmeler()
        {
            this.KatBilgileris = new List<KatBilgileri>();
        }

        public int ID { get; set; }
        public string IsletmeIsmi { get; set; }
        public string IsletmeAdresi { get; set; }
        public string IsletmeAciklamasi { get; set; }
        public string IsletmeNumarasi { get; set; }
        [JsonIgnore]
        public Nullable<int> KullaniciID { get; set; }
        public string IsletmeKrokiResim { get; set; }
        [JsonIgnore]
        public virtual Kullanicilar Kullanicilar { get; set; }
        [JsonIgnore]
        public virtual ICollection<KatBilgileri> KatBilgileris { get; set; }
    }
}
