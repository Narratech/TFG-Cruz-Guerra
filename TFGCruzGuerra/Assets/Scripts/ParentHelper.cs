using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentHelper : MonoBehaviour
{
   public void unParent()
    {
        transform.SetParent(null);
    }
    public void Parent(Transform other)
    {
        transform.SetParent(other);
        transform.position = other.position;
        transform.localPosition= Vector3.zero;
        transform.localRotation= Quaternion.identity;
    }
}
