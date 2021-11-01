using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

/*
 * FORMAT
 * 
 * Name 
 * Age
 * Experience
 * Imagen (route)
 * Habilidad Competencia 1: number
 * Habilidad Competencia 2: number
 * etc...
 * 
 */

namespace Arquitecture_Sketch_In_Console
{
    class Pilot : JsonManager
    {
        public string Name { get; private set; }
        public string Age { get; private set; }
        public string Experience { get; private set; }
        public string ImageRoute { get; private set; }
        public Dictionary<string, float> Competences { get; private set; }

        public Pilot(string name, string age, string experience, string imageRoute, Dictionary<string, float> competences)
        {
            Name = name;
            Age = age;
            Experience = experience;
            ImageRoute = imageRoute;
            Competences = competences;
        }
    }
}
