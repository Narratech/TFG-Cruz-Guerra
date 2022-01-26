using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Logic
{
    class Dialog : Step
    {
        [JsonProperty]
        public string dialog { get; private set; }

        public Dialog(string dialog)
        {
            this.dialog = dialog;
        }


        public void Play(Script script)
        {
            //nothing
        }
    }
}
