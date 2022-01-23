using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextModifier : MonoBehaviour
{
    [SerializeField] Text _text;
    public void setNumber(float n)
    {
        _text.text = n.ToString();
    }
    public void setText(string s)
    {
        _text.text = s;
    }
}
