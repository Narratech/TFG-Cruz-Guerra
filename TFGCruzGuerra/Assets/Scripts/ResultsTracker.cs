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
            Undetected, Correct, Incorrect
        }
        int[] _detection;
        int _totalOBs;
        [SerializeField] ButtonsScene _resultsScene;
        [SerializeField] [Tooltip("Seconds to wait until scene changes")] float _secondsToWait = .5f;
        private void Start()
        {
            _totalOBs = 0;
            int length = Enum.GetValues(typeof(OBDetection)).Length;
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
            _detection[(byte)OBDetection.Undetected]++;
            _totalOBs++;
        }
        /// <summary>
        /// Cuando el jugador detecte uno de los OB se llamara a esto. Se quitara el OB de undetected y dependiendo del resultado aumentara el numero de 
        /// correctos o incorrectos
        /// </summary>
        /// <param name="result">El resultado</param>
        public void detect(OBDetection result, bool detectedPreviously)
        {
            //si no se habia detectado previamente, quitamos una de los undetected, ya que ahora si ha sido detectado, excepto en el caso
            //de que el resultado sea undetected, pues eso significa que el OB ni siquiera "debia" ser detectado (es un OB puesto al azar)


            //Si es undetected es que el OB seleccionado ni siquiera estaba en la lista de los que aparecen en el evento, por lo que se añade
            //como fallo y suma al total de OBs
            if (result == OBDetection.Undetected)
            {
                _totalOBs++;
                _detection[(byte)OBDetection.Incorrect]++;
            }
            //En caso contrario hay que ver si se habia detectado anteriormente. Si no se habia detectado es que este OB acaba de detectarse
            //por primera vez, independientemente de si era correcto o incorrecto, por lo que se resta de los no detectados.
            else
            {
                if (!detectedPreviously)
                    _detection[(byte)OBDetection.Undetected]--;
               //Si el OB se habia detectado previamente y es Incorrecto, quiere decir que el jugador se habia equivocado antes al evaluar el OB
               //pero no al detectarlo y ahora le ha pasado lo mismo, por lo que es un fallo mas que añadir a la media
                else if (result == OBDetection.Incorrect)
                    _totalOBs++;
                _detection[(byte)result]++;

            }
        }
        void changeScene()
        {

            GameManager.Instance.goToScene(_resultsScene);
        }
        public void informAndGoToResults()
        {
            GameManager.ResultsData rd = new GameManager.ResultsData(_detection, _totalOBs);
            GameManager.Instance.Results = rd;
            Invoke("changeScene", _secondsToWait);
        }
    }
}
