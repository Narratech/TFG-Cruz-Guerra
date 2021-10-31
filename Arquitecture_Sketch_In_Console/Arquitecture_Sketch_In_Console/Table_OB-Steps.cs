using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arquitecture_Sketch_In_Console
{
    class Table_OB_Steps
    {
        public Table_OB_Steps(List<List<Step>> steps)
        {
            Steps = steps;
        }

        [JsonProperty]
       public List<List<Step>> Steps { get; private set; }
    }
}
