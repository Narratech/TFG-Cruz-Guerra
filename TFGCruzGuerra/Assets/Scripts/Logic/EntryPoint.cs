using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    class EntryPoint : Step
    {
        [JsonProperty]
        Source starter_;
        public override void Play(Script script)
        {
            throw new NotImplementedException();
        }
    }
}
