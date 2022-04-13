using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfg
{

    public class CharacterCreator : MonoBehaviour
    {
        [SerializeField] Animator[] _JoeVariants;
        [SerializeField] Animator[] _LouiseVariants;
        [SerializeField] Transform _parent;
        [SerializeField] Transform _PilotPos;
        [SerializeField] Transform _CopilotPos;
        [SerializeField] StepDisplayer _displayer;
        const string defaultVariant = "Joe-0";
        public string Pilot
        {
            set
            {
                string[] variant = value.Split('-');
                //si la variante esta correctamente separada y no tiene ningun error de nombre o de numero
                if (variant.Length == 2 && int.TryParse(variant[1], out int numVariant) &&
                   ((variant[0].ToLower() == "joe" && numVariant < _JoeVariants.Length) || (variant[0].ToLower() == "louise" && numVariant < _LouiseVariants.Length))
                   && numVariant >= 0)
                    _pilot = value;
                else
                    _pilot = defaultVariant;
            }
        }
        public string Copilot
        {
            set
            {
                string[] variant = value.Split('-');
                //si la variante no esta correctamente separada o si la variante tiene algun error de nombre o de numero
                if (variant.Length == 2 && int.TryParse(variant[1], out int numVariant) &&
                  ((variant[0].ToLower() == "joe" && numVariant < _JoeVariants.Length) || (variant[0].ToLower() == "louise" && numVariant < _LouiseVariants.Length))
                  && numVariant >= 0)
                    _copilot = value;
                else
                    _copilot = defaultVariant;
            }
        }
        string _pilot;
        string _copilot;
#if UNITY_EDITOR
        [SerializeField] string _editorPilot;
        [SerializeField] string _editorCopilot;
#endif
        private void Start()
        {
#if UNITY_EDITOR
            string[] pilotSplitted = _editorPilot.Split('-');
            string[] copilotSplitted = _editorCopilot.Split('-');
#else
            Pilot = GameManager.Instance.PilotVariant;
            Copilot = GameManager.Instance.CoPilotVariant;
            string[] pilotSplitted = _pilot.Split('-');
            string[] copilotSplitted = _copilot.Split('-');
#endif   
            string charName = pilotSplitted[0].ToLower();
            int variant = int.Parse(pilotSplitted[1]);
            Animator go = charName == "joe" ? _JoeVariants[variant] : _LouiseVariants[variant];

            _displayer.setCaptainAnimator(Instantiate<Animator>(go, _PilotPos.position, Quaternion.identity, _parent));

            charName = copilotSplitted[0].ToLower();
            variant = int.Parse(copilotSplitted[1]);
            go = charName == "joe" ? _JoeVariants[variant] : _LouiseVariants[variant];

            _displayer.setFirstOfficerAnimator(Instantiate(go, _CopilotPos.position, Quaternion.identity, _parent));
        }
    }

}