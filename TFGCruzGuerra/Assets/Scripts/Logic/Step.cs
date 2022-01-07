using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    //Esta interfaz la implementarán los diálogos, animaciones, pulsaciones de botón, etc.
    interface Step
    {
        void Play(Script script);
    }
}
