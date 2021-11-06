using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arquitecture_Sketch_In_Console
{
    class Animation : Step
    {
        [JsonProperty]
        int _id;

        public Animation(int id)
        {
            _id = id;
        }

        public void Play(Script script)
        {
            Console.WriteLine("Hola, soy la animación {0}", _id);
        }
    }
}
