using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotBehaviour : MonoBehaviour
{
    [SerializeField] string _message;
    [SerializeField] Text _Text;
    private void OnMouseDown()
    {
        _Text.text = _message;
    }
   

}
