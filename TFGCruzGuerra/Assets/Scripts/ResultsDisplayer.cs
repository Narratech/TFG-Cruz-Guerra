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
    [SerializeField] Image[] _bars;
    [SerializeField] float _seconds = 5;
    [SerializeField] PopUpPanel _panel;
    [SerializeField] [Tooltip("Seconds to wait until the panel opens")] float _secondsOffset = .5f;
    int[] _detection;
    float[] _weights;
    int _totalOBs;
    int _currentBar;
    float[] _secondsPerSection;
    float _lastBarCompletition;
    bool _anim;
    float _completition;
    private void Start()
    {
        _lastBarCompletition = 0;
        _currentBar = 0;
        _completition = 0;
        _totalOBs = 0;
        int length = Enum.GetValues(typeof(OBDetection)).Length;
        _secondsPerSection = new float[length];
        _weights = new float[length];
        _detection = new int[length];
        for (int i = 0; i < length; i++)
        {
            _detection[i] = 0;
            _weights[i] = 0;
            _secondsPerSection[i] = 0;

        }
    }
    private void Update()
    {
        //! Que pasa si nos ponemos a fallar como locos y hay mas incorrectos que no detectados?
        if (!_anim)
            return;
        float speed = _weights[_currentBar] / _secondsPerSection[_currentBar];
        _completition += Time.deltaTime * speed;

        float percentage = _lastBarCompletition + _weights[_currentBar];
        int nextBar = _currentBar;
        if (_completition >= percentage)
        {
            //si nos pasamos del porcentaje de barra que estamos pintando, para el siguiente frame indicamos que queremos la siguiente
            //y ademas no pintamos el exceso
            nextBar = (nextBar + 1);
            while (nextBar < _weights.Length && _weights[nextBar] == 0)
                nextBar++;
            _completition = percentage;
            _lastBarCompletition = _completition;
            if (_completition >= 1)
                _anim = false;

        }

        //pintamos la barra
        if (_bars[_currentBar].type == Image.Type.Filled)
            _bars[_currentBar].fillAmount = _completition;

        //ponemos aqui current bar para no pintar la siguiente barra cuando no toca
        _currentBar = nextBar;

    }
    /// <summary>
    /// Cuando se emita un nuevo OB se llamara a esto. Como el jugador aun no lo ha detectado lo dejamos en undetected y sumamos 1 al total de obs
    /// </summary>
    public void newOB()
    {
        _detection[(byte)OBDetection.Undetected]++;
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
            _detection[(byte)OBDetection.Undetected]--;
            _detection[(byte)result]++;

        }
    }

    public void display()
    {
#if UNITY_EDITOR

#endif
        //queremos que cada segmento se rellene a la mitad de la velocidad que el anterior, por lo que calculamos los segundos que queremos que tarde cada 
        //segmento segun la formula 
        // {x= seconds si hay un solo segmento}
        // {x =x +2x = seconds si hay dos segmentos}
        // {x + 2x +4x =seconds si hay tres segmentos}
        //para ello primero contamos el numero de segmentos que hay que rellenar y de paso calculamos hasta donde tiene que rellenarse cada uno
        float segmentsCount = 0;
        int length = Enum.GetValues(typeof(OBDetection)).Length;
        for (int i = 0; i < length; i++)
        {
            _weights[i] = (float)_detection[i] / _totalOBs;
            if (_weights[i] > 0)
            {
                segmentsCount++;
            }
        }

        //despejamos la x
        float x = _seconds / ((float)Math.Pow(2, segmentsCount) - 1);

        //aplicamos la formula con la x despejada
        int currentSegment = 0;
        for (int i = 0; i < length; i++)
        {
            if (_weights[i] > 0)
            {
                _secondsPerSection[i] = x * (float)Math.Pow(2, currentSegment);
                currentSegment++;
            }
        }

        Invoke("initAnimation", _secondsOffset);
    }
    void initAnimation()
    {
        _anim = true;
        _panel.open();
    }
}
