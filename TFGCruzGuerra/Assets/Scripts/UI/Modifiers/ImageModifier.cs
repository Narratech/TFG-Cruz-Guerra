using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageModifier : MonoBehaviour
{
    /// <summary>
    /// La idea es que esta clase sirva para cambiar cualquier atributo de las imágenes. Se le irán añadirendo métodos según
    /// sean necesarios
    /// </summary>
    [SerializeField] Image _img;
    public void ReplaceImage(Sprite image)
    {
        _img.sprite = image;
    }
   
    
}
