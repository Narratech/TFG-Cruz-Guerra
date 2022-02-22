using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageModifier : MonoBehaviour
{
    /// <summary>
    /// La idea es que esta clase sirva para cambiar cualquier atributo de las im�genes. Se le ir�n a�adirendo m�todos seg�n
    /// sean necesarios
    /// </summary>
    [SerializeField] Image _img;
    public void ReplaceImage(Sprite image)
    {
        _img.sprite = image;
    }
   
    
}
