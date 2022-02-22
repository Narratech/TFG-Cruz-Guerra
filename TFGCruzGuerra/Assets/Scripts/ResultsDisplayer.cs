using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsDisplayer : MonoBehaviour
{
    public enum OBDetection : byte
    {
        Undetected, Correct, Incorrect
    }
    Dictionary<OBDetection, int> _detection;
    int _totalOBs;
    [SerializeField] Image _undetected;
    [SerializeField] Image _correct;
    [SerializeField] Image _incorrect;
    [SerializeField] float _seconds = 5;
    float _secondsPerSection;
    const float numSections = 3;
    bool _anim;
    private void Start()
    {
        _totalOBs = 0;
        _detection = new Dictionary<OBDetection, int>();
        foreach (OBDetection item in Enum.GetValues(typeof(OBDetection)))
        {
            _detection[item] = 0;
        }
    }
    /// <summary>
    /// Cuando se emita un nuevo OB se llamara a esto. Como el jugador aun no lo ha detectado lo dejamos en undetected y sumamos 1 al total de obs
    /// </summary>
    public void newOB()
    {
        _detection[OBDetection.Undetected]++;
        _totalOBs++;
    }
    /// <summary>
    /// Cuando el jugador detecte uno de los OB se llamara a esto. Se quitara el OB de undetected y dependiendo del resultado aumentara el numero de 
    /// correctos o incorrectos
    /// </summary>
    /// <param name="result">El resultado</param>
    public void detect(OBDetection result)
    {
        if (result == OBDetection.Undetected)
            Debug.LogWarning("If the player detected an OB, it should be either Correct or Incorrect");
        else
        {
            _detection[OBDetection.Undetected]--;
            _detection[result]++;
        }
    }

    public void display()
    {
        _secondsPerSection = _seconds / numSections;
    }
}
