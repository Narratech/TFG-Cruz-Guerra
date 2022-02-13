using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
 * FORMAT JSON
 * 
 * Class / Tipe of event 
 * Dificulty of the whole thing
 * OB 1
 * OB 2
 * etc...
 * 
 */

namespace Logic
{
    public class Event : JsonManager
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string TypeOfEvent { get; set; }

        [JsonProperty]
        public float Difficulty { get; set; }

        [JsonProperty]
        public List<string> OBs { get; set; }

        [JsonIgnore]
        public int flightSection { get; set; }

        public Event()
        {
            Name = "None";
            TypeOfEvent = "None";
            Difficulty = 0;
            OBs = null;
            flightSection = -1;
        }
    }
}
