using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Logic
{
   public class JsonManager
    {
        /// <summary>
        /// Called after importFromJson
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            return 0;
        }

        public int ExportToJSON(string route, bool declareType = false)
        {
            try
            {
                if (!route.EndsWith(".json")) {
                    route = route + ".json";
                }
                StreamWriter scen = new StreamWriter(route);

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Formatting = Formatting.Indented;

                if (declareType)
                    settings.TypeNameHandling = TypeNameHandling.Objects;

                scen.Write(JsonConvert.SerializeObject(this, settings));
                scen.Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return -1;
            }

            return 0;
        }

        public static T ImportFromJSON<T>(string route, bool declareType = false) where T : JsonManager
        {
            try
            {
                if (!route.EndsWith(".json")) {
                    route = route + ".json";
                }
                StreamReader read = new StreamReader(route);

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Formatting = Formatting.Indented;

                if(declareType)
                    settings.TypeNameHandling = TypeNameHandling.Objects; 

                T t = JsonConvert.DeserializeObject<T>(read.ReadToEnd(), settings);
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

