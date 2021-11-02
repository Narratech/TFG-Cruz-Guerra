using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arquitecture_Sketch_In_Console
{
    class EntryPoint : JsonManager, Step
    {
        [JsonProperty]
        Source starter_;
        public void Play(Script script)
        {
            throw new NotImplementedException();
        }
    }
}
