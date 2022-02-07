using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    class Change : Step
    {
        [JsonProperty]
        public Source source_;
    }
}
