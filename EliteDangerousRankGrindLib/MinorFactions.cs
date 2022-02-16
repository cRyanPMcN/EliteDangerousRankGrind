using System.Collections.Generic;

namespace EliteDangerousRankGrindLib
{
    public class MinorFactions
    {
        public int minor_faction_id { get; set; }
        public double influence { get; set; }
        public IList<State> active_states { get; set; }
    }
}
