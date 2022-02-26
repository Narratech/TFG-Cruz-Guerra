using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class SoundAlarm : Step
    {
        [JsonProperty]
        public string soundAlarmName { get; set; }

        [JsonProperty]
        public bool loop { get; set; }

        public SoundAlarm(string name, bool loop)
        {
            this.soundAlarmName = name;
            this.loop = loop;
            startTime = -1;
            duration = -1;
        }

        public SoundAlarm()
        {

        }
    }
}