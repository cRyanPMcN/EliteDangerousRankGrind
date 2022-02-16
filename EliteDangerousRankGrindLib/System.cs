using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EliteDangerousRankGrindLib
{
    public class System
    {
        public UInt64 id { get; set; }
        public UInt64 edsm_id { get; set; }
        // Name of System
        public String name { get; set; }
        
        // Location of System in Galaxy
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        
        // Population of System
        public UInt64 population { get; set; }

        // Government of System
        public UInt16 government_id { get; set; }
        public String government { get; set; }

        // Superpower allegiance of System
        // Federation/Empire/Alliance/Independent
        public UInt16 allegiance_id { get; set; }
        public String allegiance { get; set; }

        // Security state of System
        // Affects mission types
        public UInt16 security_id { get; set; }
        public String security { get; set; }
        
        // Economy type of System
        // Affects mission types
        public UInt16 primary_economy_id { get; set; }
        public String primary_economy { get; set; }
        public UInt64 updated_at { get; set; }
        public UInt64 minor_factions_updated_at { get; set; }
        public IList<MinorFactions> minor_faction_presences { get; set; }


        /// <summary>
        /// Creates a default Json Serializer for filtering a given Json file
        /// </summary>
        /// <param name="systemFilePath"></param>
        static void Filter_Json_File(String systemFilePath)
        {
            JsonSerializer serializer = new JsonSerializer();
            //writer.Write(JsonConvert.SerializeObject(systems));
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
            serializer.NullValueHandling = NullValueHandling.Include;
            Filter_Json_File(systemFilePath, serializer);
        }

        /// <summary>
        /// Filters a given Json File.
        /// Removes systems which require a permit and has three or less minor factions, these systems are not useful for calcuations
        /// Removes data which is unnecessary for calcuations
        /// Alliance and Indepentent systems are not removed as the factions in the system are counted, this data is in the factions Json file
        /// </summary>
        /// <param name="systemFilePath"></param>
        /// <param name="serializer"></param>
        static void Filter_Json_File(String systemFilePath, JsonSerializer serializer)
        {
            JArray preFilterSystems;

            using (StreamReader reader = new StreamReader(systemFilePath))
            {
                preFilterSystems = JArray.Parse(reader.ReadToEnd());
            }

            using (StreamWriter writer = new StreamWriter(systemFilePath))
            {
                var systems =
                    from system in preFilterSystems
                    where (bool)system["needs_permit"] == false && system["minor_faction_presences"].Count() > 3
                    orderby system["allegiance"]
                    select new
                    {
                        id = system["id"],
                        edsm_id = system["edsm_id"],
                        name = system["name"],
                        x = system["x"],
                        y = system["y"],
                        z = system["z"],
                        population = system["population"],
                        government_id = system["government_id"],
                        government = system["government"],
                        allegiance_id = system["allegiance_id"],
                        allegiance = system["allegiance"],
                        security_id = system["security_id"],
                        security = system["security"],
                        primary_economy_id = system["primary_economy_id"],
                        primary_economy = system["primary_economy"],
                        updated_at = system["updated_at"],
                        minor_factions_updated_at = system["minor_factions_updated_at"],
                        minor_faction_presences = from presences in system["minor_faction_presences"]
                                                  select new
                                                  {
                                                      minor_faction_id = presences["minor_faction_id"],
                                                      influence = presences["influence"],
                                                      active_states = presences["active_states"],
                                                  }
                    };

                serializer.Serialize(writer, systems);
            }
        }
    }
}
