using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPanel : MonoBehaviour
{
    /// <summary>
    /// funcionalidad básica de un panel. Para paneles más avanzados hacer un prefab variant y un script que herede 
    /// de este con la nueva funcionalidad
    /// </summary>
    [SerializeField] GameObject _panel;

    Animator anim;

    [SerializeField] AnimationClip openClip, closeClip;
    [SerializeField] UnityEngine.Events.UnityEvent onClose;
    [SerializeField] UnityEngine.Events.UnityEvent onOpen;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void close()
    {
        if (anim && closeClip)
        {
            anim.Play(closeClip.name);
        }
        else _panel.SetActive(false);
        onClose?.Invoke();
    }

    public virtual void open()
    {
        if (anim && openClip)
            anim.Play(openClip.name);
        else _panel.SetActive(true);
        onOpen?.Invoke();
    }
}
