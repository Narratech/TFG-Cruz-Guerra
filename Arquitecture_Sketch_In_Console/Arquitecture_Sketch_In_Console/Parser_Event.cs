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
        private string _typeOfEvent;
        private float _difficulty;
        private Competences[] _competences;

        #region Properties
        public string TypeOfEvent { get { return _typeOfEvent; } }
        public float Difficulty { get { return _difficulty; } }
        public Competences[] EventCompetences { get { return _competences; } }
        #endregion
        public void Parse(string filename)
        {
            //abrimos archivo
            StreamReader reader = new StreamReader(filename + ".event");
            //leemos atributos
            _typeOfEvent = reader.ReadLine();

            if (!float.TryParse(reader.ReadLine(), out _difficulty))
                throw new Exception("Difficulty not specified correctly");
            //? Aquí deberiamos petar o poner por defecto 1?
            if (_difficulty < 0 || _difficulty > 1)
                throw new Exception("Difficulty not specified correctly");

            string[] CompentencesText = reader.ReadToEnd().Split('\n');
            _competences = new Competences[CompentencesText.Length];
            for (int i = 0; i < CompentencesText.Length; i++)
            {

                if (!Enum.TryParse<Competences>(reader.ReadLine(), out _competences[i]))
                    throw new Exception("Competence " + i + " not specified correctly");
            }

            //cerramos el archivo
            reader.Close();
        }
    }
}
