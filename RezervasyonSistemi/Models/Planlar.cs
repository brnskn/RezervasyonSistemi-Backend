using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RezervasyonSistemi.Models
{
    public partial class Planlar
    {
        public Planlar()
        {
            this.PlanDetaylaris = new List<PlanDetaylari>();
        }

        public int ID { get; set; }
        public string PlanIsmi { get; set; }
        public string PlanAciklamasi { get; set; }
        public Nullable<int> IsletmeID { get; set; }
        [JsonIgnore]
        public virtual Isletmeler Isletmeler { get; set; }
        [JsonIgnore]
        public virtual ICollection<PlanDetaylari> PlanDetaylaris { get; set; }
    }
}
