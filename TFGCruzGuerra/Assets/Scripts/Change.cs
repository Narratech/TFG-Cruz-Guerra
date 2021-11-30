using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arquitecture_Sketch_In_Console
{
    class Change : JsonManager, Step
    {
        [JsonProperty]
        private Source source_;
        public void Play(Script script)
        {
            script.setCurrent(source_);
        }
    }
}
