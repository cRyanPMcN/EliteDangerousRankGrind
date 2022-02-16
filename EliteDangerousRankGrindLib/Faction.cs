using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EliteDangerousRankGrindLib
{
    public class Faction
    {
        public UInt64 id;
        public String name;
        public UInt16 government_id;
        public String government;
        public UInt16 allegiance_id;
        public String allegiance;

        /// <summary>
        /// Creates a default Json Serializer for filtering a given Json file
        /// </summary>
        /// <param name="systemFilePath"></param>
        static void Filter_Json_File(String factionFilePath)
        {
            JsonSerializer serializer = new JsonSerializer() { 
                Formatting = Formatting.Indented, 
                NullValueHandling = NullValueHandling.Include 
            };
            Filter_Json_File(factionFilePath, serializer);
        }

        /// <summary>
        /// Filters a given Json File.
        /// Removes factions that are not aligned with Federation or Empire, only these two allegiances have a rank to grind
        /// </summary>
        /// <param name="factionFilePath"></param>
        /// <param name="serializer"></param>
        static void Filter_Json_File(String factionFilePath, JsonSerializer serializer)
        {
            JArray preFilterFactions;

            using (StreamReader reader = new StreamReader(factionFilePath))
            {
                preFilterFactions = JArray.Parse(reader.ReadToEnd());
            }

            using (StreamWriter writer = new StreamWriter(factionFilePath))
            {
                var factions =
                    from faction in preFilterFactions
                    where (String)faction["allegiance"] == "Federation" || (String)faction["allegiance"] == "Empire"
                    orderby faction["allegiance"]
                    select new
                    {
                        id = faction["id"],
                        name = faction["name"],
                        updated_at = faction["updated_at"],
                        government_id = faction["government_id"],
                        government = faction["government"],
                        allegiance_id = faction["allegiance_id"],
                        allegiance = faction["allegiance"]
                    };

                serializer.Serialize(writer, factions);
            }
        }
    }
}
