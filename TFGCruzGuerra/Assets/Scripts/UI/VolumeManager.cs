using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfg.UI
{
    public class VolumeManager : MonoBehaviour
    {
        public void valueChanged(float newVal)
        {
            GameManager.Instance.volume = newVal;
        }
    }
}