using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
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
