using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace tfg
{

    public class ResultsDisplayer : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text _correctPercText;
        [SerializeField] TMPro.TMP_Text _incorrectPercText;
        [SerializeField] TMPro.TMP_Text _resultSentence;
        [SerializeField] float _passTreshold;
        [SerializeField] string _passSentence;
        [SerializeField] string _failSentence;
        void Start()
        {
            GameManager.ResultsData data = GameManager.Instance.Results;
            int correct = data.Detection[(byte)ResultsTracker.OBDetection.Correct] * 100 / data.TotalOBs;
            _correctPercText.text = correct.ToString() + "%";
            _incorrectPercText.text = (100 - correct).ToString() + "%";
            if (correct >= _passTreshold)
                _resultSentence.text = _passSentence;
            else
                _resultSentence.text = _failSentence;

        }
    }


}
