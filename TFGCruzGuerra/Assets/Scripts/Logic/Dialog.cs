using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Logic
{
    class Dialog : Step
    {
        [JsonProperty]
        public string dialog { get; set; }

        public Dialog(string dialog)
        {
            this.dialog = dialog;
            startTime = -1;
            duration = -1;
        }

        public Dialog()
        {

        }
    }
}
