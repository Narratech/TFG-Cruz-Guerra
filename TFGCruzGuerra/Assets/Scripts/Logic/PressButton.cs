using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Logic
{
    public class PressButton : Step
    {
        public enum PressType : byte
        {
            OffToOn, OnToOff,Default
        }

        [JsonProperty]
        public string interruptName { get; set; }

        [JsonProperty]
        public PressType pressType { get; set; }

        public PressButton(string name, PressType pressType)
        {
            this.interruptName = name;
            this.pressType = pressType;
            startTime = -1;
            duration = -1;
        }

        public PressButton()
        {

        }
    }
}
