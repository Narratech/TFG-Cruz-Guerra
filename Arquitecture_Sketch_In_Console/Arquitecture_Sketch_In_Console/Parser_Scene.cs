using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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

namespace Arquitecture_Sketch_In_Console
{
    public enum WeatherTypes { Sunny, Windy, Rainy }

    class Parser_Scene : Parser
    {
        #region Properties
        public string Name { get; private set; }
        public string TakeOffAirport { get; private set; }
        public string DestinationAirport { get; private set; }
        public WeatherTypes Weather { get; private set; } 
        public List<string> Events { get; private set; }
        #endregion

        public Parser_Scene()
        {
            Name = "";
            TakeOffAirport = "";
            DestinationAirport = "";
            Weather = WeatherTypes.Sunny;
            Events = new List<string>();
        }

        public void Parse(string filename)
        {
            //abrimos archivo
            StreamReader reader = new StreamReader(filename + ".scenario");
            //leemos atributos
            Name = reader.ReadLine();
            
            string[] airports = reader.ReadLine().Split(';');
            TakeOffAirport = airports[0];
            DestinationAirport = airports[1];

            WeatherTypes w;
            
            if (!Enum.TryParse<WeatherTypes>(reader.ReadLine(), out w))
                throw new Exception("Weather not specified correctly");

            Weather = w;
            while (!reader.EndOfStream) {
                string line = reader.ReadLine();
                Events.Add(line);
            }

            reader.Close();
        }
    }
}
