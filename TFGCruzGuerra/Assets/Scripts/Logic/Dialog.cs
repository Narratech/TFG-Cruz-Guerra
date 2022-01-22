using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Logic
{
    class Dialog : Step
    {
        [JsonProperty]
        private string _dialog;

        public Dialog(string dialog)
        {
            _dialog = dialog;
        }


        public void Play(Script script)
        {
            UnityEngine.Debug.Log(_dialog);
        }
    }
}
