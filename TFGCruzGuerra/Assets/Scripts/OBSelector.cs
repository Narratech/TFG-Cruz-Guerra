using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OBSelector : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        //si esta en el cuarto cuadrante lo pasamos al primero
        float oldAngle = transform.localEulerAngles.z > 270 ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;
        float newAngle = oldAngle - eventData.delta.x/dragDivisor;
        print(newAngle);
        if (Mathf.Abs(newAngle) <= maxAngle)
            transform.localEulerAngles = new Vector3(0, 0, newAngle);
    }
    [SerializeField] float maxAngle = 21;
    [SerializeField] float dragDivisor = 2;
}
