using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace tfg
{

    public class Evaluator : MonoBehaviour
    {
        public void setPos(int index)
        {
            if (index < positions.Length && index >= 0)
                transform.position = positions[index].position;
        }
        public void setRandomOBs()
        {

        }
        [SerializeField] Transform[] positions;
        [SerializeField] LevelManager levelManager;
        [SerializeField] TextModifier[] OB;
    }
}
