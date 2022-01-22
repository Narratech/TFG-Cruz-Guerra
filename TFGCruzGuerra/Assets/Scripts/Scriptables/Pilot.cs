using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Pilot", menuName = "ScriptObjects/Pilot")]
    public class Pilot : ScriptableObject
    {
        public enum GenderEnum { None, Male, Female }
        public string Name;
        public int Age;
        public float Experience;
        public GenderEnum Gender;

        //De momento mejor evitar que el jugador meta imágenes
        //public string ImageRoute { get; private set; }
        public Dictionary<string, float> Competences { get; private set; }



    }
}