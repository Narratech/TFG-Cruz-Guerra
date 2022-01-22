using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPanel : MonoBehaviour
{
    /// <summary>
    /// funcionalidad b�sica de un panel. Para paneles m�s avanzados hacer un prefab variant y un script que herede 
    /// de este con la nueva funcionalidad
    /// </summary>
    [SerializeField] GameObject _panel;
    public virtual void close() { _panel.SetActive(false); }
    public virtual void open() { _panel.SetActive(true); }
}
