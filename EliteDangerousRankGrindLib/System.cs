using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteDangerousRankGrindLib
{
    public class System
    {
        public UInt64 id { get; set; }
        public UInt64 edsm_id { get; set; }
        public String name { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public UInt64 population { get; set; }
        public UInt16 government_id { get; set; }
        public String government { get; set; }
        public UInt16 allegiance_id { get; set; }
        public String allegiance { get; set; }
        public UInt16 security_id { get; set; }
        public String security { get; set; }
        public UInt16 primary_economy_id { get; set; }
        public String primary_economy { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime minor_factions_updated_at { get; set; }
        public IList<MinorFactions> minor_faction_presences { get; set; }
    }
}
