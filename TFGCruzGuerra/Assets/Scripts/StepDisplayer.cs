using Logic;
using System;
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

        //Flight Stage
        [SerializeField] private Text flightStateText;

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


        public void OnNewStep(Step step, Source source,int remainingSteps)
        {
            switch (step)
            {
                case Dialog d:
                    putText(source, d.dialog);
                    //Debug.Log("dialogo");
                    break;
                case Anim a:
                    playAnim(source, a.animName);
                    //Debug.Log("anim");
                    break;
                case PressButton pb:
                    playInterruptButton(source, pb.interruptName, pb.pressType);
                    //Debug.Log("boton");
                    break;
                case FlightStageChange fSC:
                    playEnteredFlightStage(fSC.flightSection);
                    break;
            }
        }

        private void playEnteredFlightStage(FlightSections flightSection)
        {
            flightStateText.text = flightSection.ToString();
        }

        public void OnEndStep(Step step, Source source,int remainingSteps)
        {
            switch (step)
            {
                case Dialog d:
                    removeText(source);
                    break;
                case Anim a:
                    stopAnim(source);
                    break;
                case PressButton pb:
                    stopInterruptButton(source, pb.interruptName, pb.pressType);
                    break;
            }
        }


        private void putText(Source source, string dialog)
        {
            switch (source)
            {
                case Source.Captain:
                    captainImage.gameObject.SetActive(true);
                    captainText.text = dialog;
                    break;
                case Source.First_Officer:
                    firstOfficerImage.gameObject.SetActive(true);
                    firstOfficerText.text = dialog;
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

        private void stopInterruptButton(Source source, string buttonName, PressButton.PressType pressType)
        {
            switch (source)
            {
                case Source.Captain:
                    captainInterrupt.stopVideo();
                    break;
                case Source.First_Officer:
                    firstOfficerInterrupt.stopVideo();
                    break;
            }
        }
    }
}