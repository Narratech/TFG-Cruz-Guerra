using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arquitecture_Sketch_In_Console
{
    class Table_OB_Steps
    {
        public Table_OB_Steps()
        {
            StepsIfGood = null;
            StepsIfBad = null;
        }

        [JsonProperty]
        private Dictionary<string, List<Step>> StepsIfGood;

        [JsonProperty]
        private Dictionary<string, List<Step>> StepsIfBad;

        public List<Step> getStepsForOB(string OB, bool isGood)
        {
            if (OB != null && OB != "") {
                List<Step> found;
                if(isGood)
                    return StepsIfGood.TryGetValue(OB, out found) ? found : null;
                else
                    return StepsIfBad.TryGetValue(OB, out found) ? found : null;
            }
            return null;
        }
    }
}
