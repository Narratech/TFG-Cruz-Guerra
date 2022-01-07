using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tfg
{
    public class PilotBehaviour : MonoBehaviour
    {
        [SerializeField] string _message;
        [SerializeField] string _name;
        [SerializeField] Text _Text;
        [SerializeField] Text _Name;

        private void OnMouseDown()
        {
            _Text.text = _message;
            _Name.text = _name;
        }

    }
}