using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EliteDangerousRankGrindLib
{
    public class Station
    {
        public UInt64 id;
        public String name;
        public UInt64 system_id;
        public String max_landing_pad_size;
        public UInt64 distance_to_star;
        public UInt16 government_id;
        public String government;
        public UInt16 allegiance_id;
        public String allegiance;
        public Boolean is_planetary;

        /// <summary>
        /// Creates a default Json Serializer for filtering a given Json file
        /// </summary>
        /// <param name="systemFilePath"></param>
        static void Filter_Json_File(String stationFilePath)
        {
            JsonSerializer serializer = new JsonSerializer()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };
            Filter_Json_File(stationFilePath, serializer);
        }

        /// <summary>
        /// Filters a given Json File.
        /// Removes planetary stations, and stations which are farther than 750 light seconds from the arrival point
        /// </summary>
        /// <param name="stationFilePath"></param>
        /// <param name="serializer"></param>
        static void Filter_Json_File(String stationFilePath, JsonSerializer serializer)
        {
            JArray preFilterStations;

            using (StreamReader reader = new StreamReader(stationFilePath))
            {
                preFilterStations = JArray.Parse(reader.ReadToEnd());
            }

            using (StreamWriter writer = new StreamWriter(stationFilePath))
            {
                var stations =
                     from station in preFilterStations
                     where (UInt32?)station["distance_to_star"] < 750 && station["is_planetary"].Value<bool?>() == false
                     select new
                     {
                         id = station["id"],
                         name = station["name"],
                         system_id = station["system_id"],
                         max_landing_pad_size = station["max_landing_pad_size"],
                         distance_to_star = station["distance_to_star"],
                         government_id = station["government_id"],
                         government = station["government"],
                         allegiance_id = station["allegiance_id"],
                         allegiance = station["allegiance"],
                         is_planetary = station["is_planetary"]
                     };

                serializer.Serialize(writer, stations);
            }
        }
    }
}
