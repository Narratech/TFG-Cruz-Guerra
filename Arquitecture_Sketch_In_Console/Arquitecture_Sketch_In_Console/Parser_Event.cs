using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
 * FORMAT
 * 
 * Class / Tipe of event 
 * Dificulty of the whole thing
 * Competence 1
 * Competence 2
 * etc...
 * 
 */

namespace Arquitecture_Sketch_In_Console
{
    class Parser_Event : Parser
    {
        public enum Competences
        {
            Application_of_Procedures,
            Communication,
            Automation,
            Manual_Control,
            Leadership_And_Teamwork,
            Problem_Solving_And_Decision_Making,
            Situation_Awareness,
            Workload_Management
        }

        public Parser_Event()
        {
            TypeOfEvent = "";
            Difficulty = 0;
            EventCompetences = new List<Competences>();

        }
        #region Properties
        public string TypeOfEvent { get; private set; }
        public float Difficulty { get; private set; }
        public List<Competences> EventCompetences { get; private set; }
        #endregion
        public void Parse(string filename)
        {
            //abrimos archivo
            StreamReader reader = new StreamReader(filename + ".event");
            //leemos atributos
            TypeOfEvent = reader.ReadLine();
            float d;
            if (!float.TryParse(reader.ReadLine(), out d))
                throw new Exception("Difficulty not specified correctly");
            Difficulty = d;
            //? Aquí deberiamos petar o poner por defecto 1?

            if (Difficulty < 0 || Difficulty > 1)
                throw new Exception("Difficulty not specified correctly");
            int i = 0;
            while (!reader.EndOfStream)
            {
                Competences c;
                if (!Enum.TryParse<Competences>(reader.ReadLine(), out c))
                    throw new Exception("Competence " + i + " not specified correctly");
                EventCompetences.Add(c);
                i++;
            }
            //cerramos el archivo
            reader.Close();
        }
    }
}
