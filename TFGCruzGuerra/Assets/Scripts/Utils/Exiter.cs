using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfg
{
    public class Exiter : MonoBehaviour
    {
        public void exit()
        {
            GameManager.Instance.exit();
        }
    }
}