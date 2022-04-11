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
        private void Start()
        {
            string[] pilot = GameManager.Instance.PilotVariant.Split('-');
            string[] copilot = GameManager.Instance.CoPilotVariant.Split('-');
        
            string charName = pilot[0].ToLower();
            int variant = int.Parse(pilot[1]);
            Animator go = charName == "joe" ? _JoeVariants[variant] : _LouiseVariants[variant];
            
            _displayer.setCaptainAnimator(Instantiate<Animator>(go, _PilotPos.position, Quaternion.identity, _parent));

            charName = copilot[0].ToLower();
            variant = int.Parse(copilot[1]);
            go= charName == "joe" ? _JoeVariants[variant] : _LouiseVariants[variant];

            _displayer.setFirstOfficerAnimator(Instantiate(go, _CopilotPos.position, Quaternion.identity, _parent));
        }
    }

}