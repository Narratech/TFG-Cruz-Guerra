using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Arquitecture_Sketch_In_Console
{
    class JsonManager
    {
        /// <summary>
        /// Called after importFromJson
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            return 0;
        }

        public int ExportToJSON(string route)
        {
            try
            {
                if (!route.EndsWith(".json")) {
                    route = route + ".json";
                }
                StreamWriter scen = new StreamWriter(route);
                scen.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
                scen.Close();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return -1;
            }

            return 0;
        }

        public static T ImportFromJSON<T>(string route) where T : JsonManager
        {
            try
            {
                if (!route.EndsWith(".json")) {
                    route = route + ".json";
                }
                StreamReader read = new StreamReader(route);
                T t = JsonConvert.DeserializeObject<T>(read.ReadToEnd());
                read.Close();
                if (t.Init() < 0) {
                    Console.Error.WriteLine("Error initializing: " + t);
                }
                return t;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return default(T);
            }
        }
    }
}

