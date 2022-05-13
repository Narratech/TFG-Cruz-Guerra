using Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tfg
{
    public class Evaluate : MonoBehaviour, Interfaces.INewStepHandler, Interfaces.IEndStepHandler
    {
        [SerializeField] PopUpPanel panel;

        [SerializeField] Evaluator evaluator;

        [SerializeField] Button CaptainBubble, CaptainInterrupt, FOBubble, FOInterrupt;

        [SerializeField] bool _tutorial = false;

        public bool CanEvaluate { get; set; } = false;

        private void Start()
        {
            LevelManager.AddNewStepHandler(this);
            LevelManager.AddEndStepHandler(this);

            CanEvaluate = !_tutorial;
        }

        void OnDestroy()
        {
            LevelManager.RemoveNewStepHandler(this);
            LevelManager.RemoveEndStepHandler(this);
        }

        public void startEvaluating(int pilot)
        {
            if (CanEvaluate)
            {
                evaluator.setPos(pilot);
                panel.open();
                evaluator.setRandomOBs(pilot);

                if(!_tutorial)
                    GameManager.Instance.levelManager.setScaleTime(0);
                else
                    GameManager.Instance.levelManager.setScaleTime(1);
            }
        }

        public void stopEvaluating()
        {
            panel.close();
            GameManager.Instance.levelManager.restoreScaleTime();
        }

        public void OnNewStep(Step step, Source source, int remainingSteps)
        {
            switch (step)
            {
                case Dialog d:
                    if (source == Source.Captain) CaptainBubble.interactable = true;
                    else if (source == Source.First_Officer) FOBubble.interactable = true;
                    break;
                case PressButton p:
                    if (source == Source.Captain) CaptainInterrupt.interactable = true;
                    else if (source == Source.First_Officer) FOInterrupt.interactable = true;
                    break;
            }
        }

        public void OnEndStep(Step step, Source source, int remainingSteps)
        {
            switch (step)
            {
                case Dialog d:
                    if (source == Source.Captain) CaptainBubble.interactable = false;
                    else if (source == Source.First_Officer) FOBubble.interactable = false;
                    break;
                case PressButton p:
                    if (source == Source.Captain) CaptainInterrupt.interactable = false;
                    else if (source == Source.First_Officer) FOInterrupt.interactable = false;
                    break;
            }
        }
    }
}