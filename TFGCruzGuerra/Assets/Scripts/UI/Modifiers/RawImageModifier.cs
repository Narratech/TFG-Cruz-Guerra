using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RawImageModifier : MonoBehaviour
{
    [SerializeField] RawImage _img;
    public void setAlpha(float alpha)
    {
        if (alpha >= 0 && alpha <= 1)
            _img.color = new Color(1, 1, 1, alpha);
    }
}
