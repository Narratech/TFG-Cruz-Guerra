using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
 * FORMAT JSON
 * 
 * Class / Tipe of event 
 * Dificulty of the whole thing
 * OB 1
 * OB 2
 * etc...
 * 
 */

namespace Arquitecture_Sketch_In_Console
{
    class Event
    {
        public string Name { get; private set; }
        public string TypeOfEvent { get; private set; }
        public float Difficulty { get; private set; }
        public List<Competences> EventCompetences { get; private set; }

        private string fileName;

        public Event()
        {
            Name = "None";
            TypeOfEvent = "None";
            Difficulty = 0;
            EventCompetences = null;
            fileName = null;
        }

        public Event(string name, string typeOfEvent, float difficulty, List<Competences> eventCompetences)
        {
            TypeOfEvent = typeOfEvent;
            Name = name;
            Difficulty = difficulty;
            EventCompetences = eventCompetences;
            TypeOfEvent = TypeOfEvent.ToLower().Replace(' ', '_');
            fileName = "Events/" + name + ".json";
        }
    }
}
