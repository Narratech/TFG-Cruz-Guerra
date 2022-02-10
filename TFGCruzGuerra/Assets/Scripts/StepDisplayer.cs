using Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tfg
{
    public class StepDisplayer : MonoBehaviour, Interfaces.INewStepHandler, Interfaces.IEndStepHandler
    {
        //Dialogs
        [SerializeField] private Image captainImage, firstOfficerImage;
        [SerializeField] private Text captainText, firstOfficerText;

        //Animations
        [SerializeField] private Animator captainAnimator, firstOfficerAnimator;

        //Interrupts
        [SerializeField] private InterruptManager captainInterrupt, firstOfficerInterrupt;

        void Start()
        {
#if UNITY_EDITOR
            if(captainImage == null || firstOfficerImage == null || 
                captainText == null || firstOfficerText == null || 
                captainAnimator == null || firstOfficerAnimator == null ||
                captainInterrupt == null || firstOfficerInterrupt == null)
            {
                Debug.LogError("StepDisplayer Serialized field not setted");
            }
#endif

            LevelManager.AddNewStepHandler(this);
            LevelManager.AddEndStepHandler(this);
        }


        void OnDestroy()
        {
            LevelManager.RemoveNewStepHandler(this);
            LevelManager.RemoveEndStepHandler(this);
        }


        public void NewStep(Step step, Source source)
        {
            switch (step)
            {
                case Dialog d:
                    putText(source, d.dialog);
                    break;
                case Anim a:
                    playAnim(source, a.animName);
                    break;
                case PressButton pb:
                    playInterruptButton(source, pb.interruptName, pb.pressType);
                    break;
            }
        }


        public void EndStep(Step step, Source source)
        {
            switch (step)
            {
                case Dialog d:
                    removeText(source);
                    break;
                case Anim a:
                    stopAnim(source);
                    break;
            }
        }


        private void putText(Source source, string dialog)
        {
            switch (source)
            {
                case Source.Captain:
                    captainImage.gameObject.SetActive(true);
                    captainText.text = "captain: " + dialog;
                    break;
                case Source.First_Officer:
                    firstOfficerImage.gameObject.SetActive(true);
                    firstOfficerText.text = "first officer: " + dialog;
                    break;
                case Source.Radio:
                    break;
            }
        }

        private void removeText(Source source)
        {
            switch (source)
            {
                case Source.Captain:
                    captainImage.gameObject.SetActive(false);
                    break;
                case Source.First_Officer:
                    firstOfficerImage.gameObject.SetActive(false);
                    break;
                case Source.Radio:
                    break;
            }
        }

        private void playAnim(Source source, string animName)
        {
            switch (source)
            {
                case Source.Captain:
                    captainAnimator.Play(animName);
                    break;
                case Source.First_Officer:
                    firstOfficerAnimator.Play(animName);
                    break;
            }
        }

        private void stopAnim(Source source)
        {
            switch (source)
            {
                case Source.Captain:
                    captainAnimator.Play("Idle");
                    break;
                case Source.First_Officer:
                    firstOfficerAnimator.Play("Idle");
                    break;
            }
        }

        private void playInterruptButton(Source source, string buttonName, PressButton.PressType pressType)
        {
            switch (source)
            {
                case Source.Captain:
                    captainInterrupt.playVideo(buttonName, pressType);
                    break;
                case Source.First_Officer:
                    firstOfficerInterrupt.playVideo(buttonName, pressType);
                    break;
            }
        }
    }
}