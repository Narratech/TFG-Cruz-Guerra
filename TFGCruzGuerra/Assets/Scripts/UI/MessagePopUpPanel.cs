using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePopUpPanel : PopUpPanel
{
    [SerializeField] Text _msg;
    public void setMessage(string msg) { _msg.text = msg; }

}
