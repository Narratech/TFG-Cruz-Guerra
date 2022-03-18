using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfg
{

    public class StepModifierDisplayer : MonoBehaviour
    {
        [SerializeField] Evaluate evaluate;
        [SerializeField] StepDisplayer stepDisplayer;
        [SerializeField] Evaluator eval;
        [SerializeField] StoppedTimePanelClose close;
        public void Modify(string modifier)
        {
            foreach (string mod in modifier.Split(','))
            {

                switch (mod)
                {
                    case "dontstop":
                        Time.timeScale = 1;
                        break;
                    case "continueafterclick":
                        evaluate.CountinueAfterClick = true;
                        break;
                    case "stop":
                        Time.timeScale = 0;
                        break;
                    case "radiodontdisappear":
                        stepDisplayer.PlayRadioExitAnim = false;
                        break;
                    case "evalDisable":
                        eval.cannotEvaluate();
                        break;
                    case "evalEnable":
                        eval.canEvaluate();
                        break;
                    case "cantexitevaluator":
                        close.enableExit();
                        break;
                    case "canexitevaluator":
                        close.DisableExit();
                        break;
                    case "cantevaluate":
                        evaluate.CanEvaluate = false;
                        break;


                    default:
                        break;
                }
            }
        }
    }
}
