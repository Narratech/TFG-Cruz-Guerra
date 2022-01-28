using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    class Table_OB_Steps : JsonManager
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
                bool found = isGood ? StepsIfGood.ContainsKey(OB) : StepsIfBad.ContainsKey(OB);
                
                if (!found)
                    addDefaultStep(OB);

                List<Step> steps = isGood ? new List<Step>(StepsIfGood[OB]) : new List<Step>(StepsIfBad[OB]);
                return stepsCloned(steps);
            }
            return null;
        }

        private void addDefaultStep(string OB)
        {
            List<Step> stepsGood = new List<Step>();
            stepsGood.Add(new Dialog("good:" + OB));
            StepsIfGood.Add(OB, stepsGood);

            List<Step> stepsBad = new List<Step>();
            stepsBad.Add(new Dialog("bad:" + OB));
            StepsIfBad.Add(OB, stepsBad);
        }

        private List<Step> stepsCloned(List<Step> steps)
        {
            for(int i = 0; i < steps.Count; i++)
            {
                switch (steps[i])
                {
                    case Dialog d:
                        steps[i] = new Dialog(d.dialog);
                        break;
                }
            }
            return steps;
        }
    }
}
