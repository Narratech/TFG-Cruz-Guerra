using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    //Esta interfaz la implementarán los diálogos, animaciones, pulsaciones de botón, etc.
    public abstract class Step
    {
        public enum Result { Good = 0, Bad = 1, Neutral=2 }
        public float startTime, duration;
        public Result result;
        public string OB;
    }
}
