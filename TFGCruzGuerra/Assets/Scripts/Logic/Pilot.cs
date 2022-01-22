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

namespace Logic
{
    public class Pilot : JsonManager
    {
        public enum GenderEnum:int { None, Male, Female }
        public string Name { get; set; }
        public int Age { get; set; }
        public float Experience { get; set; }
        //realmente queremos que se le pongan imagenes personalizadas?
        //public string ImageRoute { get; set; }
        public GenderEnum Gender { get; set; }
        public Dictionary<string, float> Competences { get; private set; }

        public Pilot(string name, int age, float experience, GenderEnum gender, Dictionary<string, float> competences)
        {
            Name = name;
            Age = age;
            Experience = experience;
            Gender = gender;
            Competences = competences;
        }
    }
}
