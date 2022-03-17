using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Logic
{
    class WeatherChange : Step
    {
        public string weather; //Posible weathers: Sunny, Rain

        public WeatherChange()
        {
            startTime = -1;
            duration = -1;
            weather = "Sunny";
        }
    }
}
