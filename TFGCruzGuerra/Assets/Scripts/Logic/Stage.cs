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

    public enum FlightSections { PushBack, Taxi, Takeoff, Climb, Cruise, Descent, Landing }

    public class Stage : JsonManager
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
                int f = isAFlightSection(eventName);

                if (f < 0)  Events.Add(JsonManager.ImportFromJSON<Event>("Assets/GameAssets/Events/" + eventName));
                else        Events.Add(new Event());

                Events[Events.Count - 1].flightSection = f;
            }
            return 0;
        }

        private int isAFlightSection(string name)
        {
            switch (name)
            {
                case "PushBack": return (int)FlightSections.PushBack;
                case "Taxi": return (int)FlightSections.Taxi;
                case "Takeoff": return (int)FlightSections.Takeoff;
                case "Climb": return (int)FlightSections.Climb;
                case "Cruise": return (int)FlightSections.Cruise;
                case "Descent": return (int)FlightSections.Descent;
                case "Landing": return (int)FlightSections.Landing;
                default: return -1;
            }
        }
    }
}
