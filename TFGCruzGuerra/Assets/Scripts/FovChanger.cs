using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovChanger : MonoBehaviour
{
    [SerializeField] float defaultFov = 66;
    [SerializeField] float maxFov = 101;
    void Start()
    {
        float fov = (defaultFov * 16.0f / 9.0f) * Screen.height / Screen.width;
        Camera.main.fieldOfView = Mathf.Min(fov, maxFov);
    }

}
