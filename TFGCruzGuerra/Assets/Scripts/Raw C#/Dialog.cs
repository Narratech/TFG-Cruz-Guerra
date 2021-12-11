using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Arquitecture_Sketch_In_Console
{
    class Dialog : Step
    {
        [JsonProperty]
        private string _dialog;

        public Dialog(string dialog)
        {
            _dialog = dialog;
        }


        public void Play(Script script)
        {
            Console.WriteLine(_dialog);
        }
    }
}
