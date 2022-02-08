using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Logic;

namespace tfg
{
    public class InterruptManager : MonoBehaviour
    {

        [SerializeField] Text interruptName;
        [SerializeField] RawImage renderImage;
        [SerializeField] VideoPlayer videoOffToOn, videoOnToOff;

        void Start()
        {
#if UNITY_EDITOR
            if(interruptName == null || renderImage == null || videoOffToOn == null || videoOnToOff == null)
            {
                Debug.LogError("InterruptManager not corrected setted");
            }
#endif
            videoOffToOn.Prepare();
            videoOnToOff.Prepare();

            videoOffToOn.loopPointReached += videoCompleted;
            videoOnToOff.loopPointReached += videoCompleted;
        }

        public void playVideo(string name, PressButton.PressType pressType)
        {
            interruptName.text = name;
            renderImage.enabled = true;
            interruptName.enabled = true;

            switch (pressType)
            {
                case PressButton.PressType.OffToOn:
                    videoOnToOff.Stop();
                    videoOffToOn.Play();
                    break;
                case PressButton.PressType.OnToOff:
                    videoOffToOn.Stop();
                    videoOnToOff.Play();
                    break;
            }
        }
        
        private void videoCompleted(VideoPlayer vp)
        {
            renderImage.enabled = false;
            interruptName.enabled = false;
        }
    }
}