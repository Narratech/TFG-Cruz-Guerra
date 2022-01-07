using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
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
