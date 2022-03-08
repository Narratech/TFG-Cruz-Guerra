using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Logic
{
    class Abort : Step
    {
        public Abort()
        {
            startTime = -1;
            duration = -1;
        }
    }
}
