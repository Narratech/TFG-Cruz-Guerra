using System.Collections;
using System.Collections.Generic;
using tfg.UI;
using UnityEngine;
using UnityEngine.UI;

namespace tfg
{

    public class StepModifierDisplayer : MonoBehaviour
    {
        [SerializeField] Evaluate evaluate;
        [SerializeField] StepDisplayer stepDisplayer;
        [SerializeField] Evaluator eval;
        [SerializeField] StoppedTimePanelClose close;
        [SerializeField] GameObject nextStep;
        [SerializeField] Highlighter highlighter;

        public void Modify(string modifier)
        {
            foreach (string mod in modifier.Split(','))
            {
                switch (mod)
                {
                    case "radiodontdisappear":
                        stepDisplayer.PlayRadioExitAnim = false;
                        break;
                    case "nextstepappear":
                        nextStep.SetActive(true);
                        break;
                    case "nextstepdissappear":
                        nextStep.SetActive(false);
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
                    case "canevaluate":
                        evaluate.CanEvaluate = true;
                        break;
                    case "hlradio":
                        highlighter.highLight("radio", true);
                        break;
                    case "hlstopradio":
                        highlighter.highLight("radio", false);
                        break;
                    case "hlstage":
                        highlighter.highLight("stage", true);
                        break;
                    case "hlstopstage":
                        highlighter.highLight("stage", false);
                        break;
                    case "hlabort":
                        highlighter.highLight("abort", true);
                        break;
                    case "hlstopabort":
                        highlighter.highLight("abort", false);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
