using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaluator : MonoBehaviour
{
    public void setPos(int index)
    {
        if (index < positions.Length && index >= 0)
            transform.position = positions[index].position;
    }
    [SerializeField] Transform[] positions;
}
