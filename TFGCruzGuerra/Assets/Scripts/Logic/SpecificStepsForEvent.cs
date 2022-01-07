using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    class SpecificStepsForEvent : JsonManager
    {
        [JsonProperty]
        public List<Step> StepsIfGood { get; private set; }

        [JsonProperty]
        public List<Step> StepsIfBad { get; private set; }

        public SpecificStepsForEvent()
        {
        }
    }
}
