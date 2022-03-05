using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace tfg
{

    public class ResultsTracker : MonoBehaviour
    {
        public enum OBDetection : byte
        {
            Correct, Incorrect, Undetected
        }
        int[] _detection;
        int _totalOBs;

        private void Start()
        {
            _totalOBs = 0;
            int length = Enum.GetValues(typeof(OBDetection)).Length - 1;
            _detection = new int[length];
            for (int i = 0; i < length; i++)
            {
                _detection[i] = 0;
            }
        }

        /// <summary>
        /// Cuando se emita un nuevo OB se llamara a esto. Como el jugador aun no lo ha detectado lo dejamos en undetected y sumamos 1 al total de obs
        /// </summary>
        public void newOB()
        {
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
            {
                Debug.LogWarning("Evaluation can't be Undetected");
            }
            else
            {
                _detection[(byte)result]++;
            }
        }

        public void informAndGoToResults()
        {
            int totalDetection = 0;
            for (int i = 0; i <= (byte)OBDetection.Incorrect; i++)
            {
                totalDetection += _detection[i];
            }
            //al hacer este maximo estamos teniendo tambien en cuenta los no detectados ya que si es mayor total obs es que hay no detectados y el jugador 
            //no ha fallado demasiado pero si la suma es mayor es que el jugador ha fallado tanto que ha sobrepasado a los no detectados
            GameManager.ResultsData rd = new GameManager.ResultsData(_detection, Math.Max(_totalOBs,totalDetection));
            GameManager.Instance.Results = rd;
        }
    }
}
