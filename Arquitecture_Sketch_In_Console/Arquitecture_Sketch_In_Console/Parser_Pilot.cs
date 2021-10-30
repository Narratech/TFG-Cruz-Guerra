using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
    class Parser_Pilot : Parser
    {
        #region Properties
        public string Name { get; private set; }
        public string Age { get; private set; }
        public string Experience { get; private set; }
        public string ImageRoute { get; private set; }
        public string BehaviourTable { get; private set; }
        public Dictionary<string, float> Competences { get; private set;  }
        #endregion

        public Parser_Pilot()
        {
            Name = "";
            Age = "";
            Experience = "";
            ImageRoute = "";
            BehaviourTable = "";
            Competences = new Dictionary<string, float>();
        }

        public void Parse(string filename)
        {
            //abrimos archivo
            StreamReader reader = new StreamReader(filename + ".pilot");

            //leemos atributos
            Name = reader.ReadLine();

            //leemos atributos
            Age = reader.ReadLine();

            //leemos atributos
            Experience = reader.ReadLine();

            //leemos atributos
            ImageRoute = reader.ReadLine();

            //leemos atributos
            BehaviourTable = reader.ReadLine();

            while (!reader.EndOfStream) {
                string[] line = reader.ReadLine().Split(':');
                float competenceHability;
                if (!float.TryParse(line[1], out competenceHability))
                    throw new Exception("Error al leer habilidad de la competencia: " + line[0]);
                Competences[line[0]] = competenceHability;
            }

            reader.Close();
        }
    }
}
