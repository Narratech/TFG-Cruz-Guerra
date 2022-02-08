using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Logic
{
    class Anim : Step
    {
        [JsonProperty]
        public string animName { get; private set; }

        public Anim(string name)
        {
            this.animName = name;
        }
    }
}
