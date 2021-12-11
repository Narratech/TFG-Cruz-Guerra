using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotBehaviour : MonoBehaviour
{
    [SerializeField] string _message;
    private void OnMouseDown()
    {
        print(_message);
    }


}
