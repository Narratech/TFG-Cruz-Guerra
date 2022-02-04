using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tfg.Interfaces;
using Logic;

namespace tfg
{
    public class Evaluator : MonoBehaviour, INewStepHandler, IEndStepHandler
    {
        int _correct;
        [SerializeField] TextModifier _resultText;
        [SerializeField] Animator _resultAnimator;
        [SerializeField] Transform[] _positions;
        [SerializeField] LevelManager _levelManager;
        [SerializeField] OBSelector[] _OB;
        List<Logic.Step> _happeningSteps;

        Logic.Table_CompetencesToOB _CompetencesToOB;
        //queremos que los cambios se den mientras el evaluator no se vea
        private void OnDisable()
        {
            LevelManager.AddEndStepHandler(this);
            LevelManager.AddNewStepHandler(this);
        }
        private void OnEnable()
        {
            LevelManager.RemoveEndStepHandler(this);
            LevelManager.RemoveNewStepHandler(this);
        }
        private void Start()
        {
            _happeningSteps = new List<string>();

        }
        public void setPos(int index)
        {
            if (index < _positions.Length && index >= 0)
                transform.position = _positions[index].position;
        }

        public void setRandomOBs()
        {

            _CompetencesToOB = GameManager.Instance.competencesToOB;

            string myOB = _levelManager.getCurrentStep().OB;
            string comp = _CompetencesToOB.getCompetenceFromOB(myOB);
            HashSet<string> OBSet = _CompetencesToOB.GetOBsFromCompetence(comp);
            int fin = Math.Min(_OB.Length, OBSet.Count);

            //quitamos del set el ob que no nos interesa
            LinkedList<string> OBToRemove = new LinkedList<string>();
            OBToRemove.AddFirst(new LinkedListNode<string>(myOB));
            OBSet.ExceptWith(OBToRemove);
            string[] allOBs = new string[OBSet.Count];

            //Copia profunda
            OBSet.CopyTo(allOBs);
            _correct = UnityEngine.Random.Range(0, fin);
            OBToRemove.RemoveFirst();
            int firstElement = 0;

            //cogemos uno al azar del array entre nuestro "primero" y el final
            //tras elegir el OB, ponemos nuestro "primero" en el índice del OB
            //y finalmente nuestro "primero" pasa a ser el siguiente. De esta forma
            //evitamos repeticiones y todos los OB tienen posibilidad de salir.
            //Repetimos este proceso hasta el correcto
            int i = 0;
            for (; i < _correct; i++)
            {
                int index = UnityEngine.Random.Range(firstElement, allOBs.Length);
                string randOB = allOBs[index];
                allOBs[index] = allOBs[firstElement];
                firstElement++;
                _OB[i].setOB(randOB);
                _OB[i].gameObject.SetActive(true);
            }

            //colocamos el correcto
            _OB[_correct].setOB(myOB);
            _OB[_correct].gameObject.SetActive(true);
            i++;

            //proseguimos el proceso anterior hasta el final de las opciones
            for (; i < fin; i++)
            {
                int index = UnityEngine.Random.Range(firstElement, allOBs.Length);
                string randOB = allOBs[index];
                allOBs[index] = allOBs[firstElement];
                firstElement++;
                _OB[i].setOB(randOB);
                _OB[i].gameObject.SetActive(true);
            }

            //por si hay no hay suficientes OB
            for (; i < _OB.Length; i++)
            {
                _OB[i].gameObject.SetActive(false);
            }


        }
        public void evaluate(string oB, bool isPositive, Vector2 position)
        {
            Logic.Step.Result r = _levelManager.getCurrentStep().result;
            //todo supongo que aquí habría que hacer algo relacionado con el score pero no se muy bien como llevarlo
            if (oB == _OB[_correct].getOB() &&
                ((isPositive && r == Logic.Step.Result.Good) || (!isPositive && r == Logic.Step.Result.Bad)))
            {
                _resultText.setText("+1");
                _resultText.setColor(Color.green);

            }
            else
            {
                _resultText.setText("-1");
                _resultText.setColor(Color.red);
            }
            _resultText.setPos(position);

            _resultAnimator.Play("Fade Up", 0, 0);
        }

        //precondición: el paso es completamente nuevo, es decir, no es la continuación de un paso anterior (p. ej. si
        //para representar un OB x el personaje tiene que hablar y animarse aquí viene o el dialogo o la animacion, pero nunca los dos)
        public void NewStep(Step step)
        {
            _happeningSteps.Add(step);

        }

        public void EndStep(Step step)
        {
            _happeningSteps.Remove(step);
        }
    }
}
