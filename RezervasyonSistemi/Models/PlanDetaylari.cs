using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RezervasyonSistemi.Models
{
    public partial class PlanDetaylari
    {
        public PlanDetaylari()
        {
            this.RezervasyonTalepleris = new List<RezervasyonTalepleri>();
        }

        public int ID { get; set; }
        public Nullable<int> HaftaninGunu { get; set; }
        public Nullable<System.DateTime> BaslangicSaati { get; set; }
        public Nullable<System.DateTime> BitisSaati { get; set; }
        public Nullable<int> PlanID { get; set; }
        [JsonIgnore]
        public virtual Planlar Planlar { get; set; }
        [JsonIgnore]
        public virtual ICollection<RezervasyonTalepleri> RezervasyonTalepleris { get; set; }
    }
}
