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

        public void stopVideo()
        {
            renderImage.enabled = false;
            interruptName.enabled = false;
            videoOnToOff.Stop();
            videoOffToOn.Stop();
        }
    }
}