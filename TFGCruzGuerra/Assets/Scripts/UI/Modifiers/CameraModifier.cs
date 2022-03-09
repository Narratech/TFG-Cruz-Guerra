using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModifier : MonoBehaviour
{
    [SerializeField] Camera _cam;
    public void removeTargetTexture()
    {
        _cam.targetTexture = null;
    }
}
