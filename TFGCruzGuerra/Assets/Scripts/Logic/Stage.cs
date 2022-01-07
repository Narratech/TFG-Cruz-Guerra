using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization;

/*
 * FORMAT JSON
 * 
 * Name 
 * Airport 1; Airport2
 * Climate
 * Etc (de momento fuera)
 * Ex
 * Ey
 * Ez
 * Ew
 * etc...
 * 
 */

namespace Logic
{
    public enum WeatherTypes { Sunny, Windy, Rainy }

    class Stage:JsonManager
    {
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty]
        public string TakeOffAirport { get; private set; }
        [JsonProperty]
        public string DestinationAirport { get; private set; }
        [JsonProperty]
        public WeatherTypes Weather { get; private set; }
        [JsonProperty]
        private List<string> EventNames;
        [JsonIgnore]
        public List<Event> Events { get; private set; }


        //necesario para deserializar, aunque tambien se puede poner [JsonProperty] en las propiedades que queremos deserializar
        public Stage(string name, string takeOffAirport, string destinationAirport, WeatherTypes weather, List<string> e)
        {
            Name = name;
            TakeOffAirport = takeOffAirport;
            DestinationAirport = destinationAirport;
            Weather = weather;
            EventNames = e;
            Events = new List<Event>();
        }

        protected override int Init()
        {
            foreach (string eventName in EventNames)
            {
                Events.Add(JsonManager.ImportFromJSON<Event>("Assets/GameAssets/Events/" + eventName));
            }
            return 0;
        }
    }
}
