using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    //Esta interfaz la implementarán los diálogos, animaciones, pulsaciones de botón, etc.
    abstract class Step
    {
        public float startTime, duration;
        public abstract void Play(Script script);
    }
}
