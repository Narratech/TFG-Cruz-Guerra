using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Logic
{
    public class PressButton : Step
    {
        public enum PressType
        {
            OffToOn, OnToOff
        }

        [JsonProperty]
        public string interruptName { get; private set; }

        [JsonProperty]
        public PressType pressType { get; private set; }

        public PressButton(string name, PressType pressType)
        {
            this.interruptName = name;
            this.pressType = pressType;
        }
    }
}
