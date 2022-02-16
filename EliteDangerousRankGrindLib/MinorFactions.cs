using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteDangerousRankGrindLib
{
    public class MinorFactions
    {
        public int minor_faction_id { get; set; }
        public double influence { get; set; }
        public IList<State> active_states { get; set; }
    }
}
