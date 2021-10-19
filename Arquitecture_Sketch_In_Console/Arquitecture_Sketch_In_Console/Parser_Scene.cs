using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/*
 * FORMAT
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
        private string _name;
        private string _destinationAirport;
        private string _takeOffAirport;
        private WeatherTypes _weather;
        private string[] _events;
        #region Properties
        public string Name { get { return _name; } }
        public string DestinationAirport { get { return _destinationAirport; } }
        public string TakeOffAirport { get { return _takeOffAirport; } }
        public WeatherTypes Weather { get { return _weather; } }
        public string[] Events { get { return _events; } }
        #endregion
        public void Parse(string filename)
        {
            //abrimos archivo
            StreamReader reader = new StreamReader(filename + ".scenario");
            //leemos atributos
            _name = reader.ReadLine();
            
            string[] airports = reader.ReadLine().Split(';');
            _takeOffAirport = airports[0];
            _destinationAirport = airports[1];
            
            if (!Enum.TryParse<WeatherTypes>(reader.ReadLine(), out _weather))
                throw new Exception("Weather not specified correctly");
           
            //? Los eventos debería parsearlos el guion?
            _events = reader.ReadToEnd().Split('\n');
            reader.Close();
        }
    }
}
