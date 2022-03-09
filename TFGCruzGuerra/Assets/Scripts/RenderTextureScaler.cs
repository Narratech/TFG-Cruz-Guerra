using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureScaler : MonoBehaviour
{
    
    [SerializeField] RenderTexture _texture;
    void Start()
    {
        _texture.height = Screen.height;
        _texture.width = Screen.width;
    }

   
}
