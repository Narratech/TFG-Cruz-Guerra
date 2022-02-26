using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace tfg
{

    public class ResultsDisplayer : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text[] _percentageTexts;
        [SerializeField] TMPro.TMP_Text _resultSentence;
        [SerializeField] float _passTreshold;
        [SerializeField] string _passSentence;
        [SerializeField] string _failSentence;
        void Start()
        {
            GameManager.ResultsData data = GameManager.Instance.Results;
            for (int i = 0; i < data.Detection.Length; i++)
            {
                _percentageTexts[i].text = (data.Detection[i]*100 / data.TotalOBs).ToString() + "%";
            }
            if (data.Detection[(byte)ResultsTracker.OBDetection.Correct] * 100 / data.TotalOBs >= _passTreshold)
                _resultSentence.text = _passSentence;
            else
                _resultSentence.text = _failSentence;

        }
    }


}
