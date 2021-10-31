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

namespace Arquitecture_Sketch_In_Console
{
    class Event : JsonManager
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty]
        public string TypeOfEvent { get; set; }

        [JsonProperty]
        public float Difficulty { get; set; }

        [JsonProperty]
        public List<Competences> EventCompetences { get; set; }

        public Event()
        {
            Name = "None";
            TypeOfEvent = "None";
            Difficulty = 0;
            EventCompetences = null;
        }
    }
}
