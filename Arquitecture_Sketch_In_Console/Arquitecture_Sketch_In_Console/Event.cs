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
        public Event(string typeOfEvent, float difficulty, List<Competences> eventCompetences)
        {
            TypeOfEvent = typeOfEvent;
            Difficulty = difficulty;
            EventCompetences = eventCompetences;
        }

        public string TypeOfEvent { get; private set; }
        public float Difficulty { get; private set; }
        public List<Competences> EventCompetences { get; private set; }

    }
}
