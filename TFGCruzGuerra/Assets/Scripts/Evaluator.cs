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
        private struct EvaluableInfo
        {
            public string OB;
            public Step.Result result;
        }
        [SerializeField] TextModifier _resultText;
        [SerializeField] Animator _resultAnimator;
        [SerializeField] Transform[] _positions;
        [SerializeField] OBSelector[] _OB;
        LinkedList<Logic.Step> _happeningSteps;
        bool _changedOB = false;
        //esto puede ser incluso de pair string y source con result por si queremos evaluar el source tambien o bien un hashset de steps
        HashSet<EvaluableInfo> _correctEvaluation;

        Logic.Table_CompetencesToOB _CompetencesToOB;
        private void Start()
        {
            _CompetencesToOB = GameManager.Instance.competencesToOB;
            _happeningSteps = new LinkedList<Step>();
            _correctEvaluation = new HashSet<EvaluableInfo>();
            LevelManager.AddEndStepHandler(this);
            LevelManager.AddNewStepHandler(this);
        }
        public void setPos(int index)
        {
            if (index < _positions.Length && index >= 0)
                transform.position = _positions[index].position;
        }

        public void setRandomOBs()
        {
            //Si los ob son exactamente los mismos que cuando clickó no hacemos nada y nos quedamos con los mismos
            //todo sabemos de antemano cuantos ob distintos estan pasando, por lo que podemos poner para cada uno el correcto y los x incorrectos
            //todo que le toquen. X sería ob.length/n de obs pasando
            if (_changedOB)
            {
                //nos quitamos los de la evaluación anterior
                _correctEvaluation.Clear();
                int[] possibleIndexes = new int[_OB.Length];
                for (int i = 0; i < possibleIndexes.Length; i++)
                {
                    possibleIndexes[i] = i;
                }
                int possiBleIndexesFirst = 0;
                LinkedListNode<Logic.Step> currentNode = _happeningSteps.First;
                while (currentNode != null)
                {

                    EvaluableInfo eval = new EvaluableInfo();
                    eval.OB = currentNode.Value.OB;
                    eval.result = currentNode.Value.result;
                    _correctEvaluation.Add(eval);
                    currentNode = currentNode.Next;
                    //lo quitamos para que si vuelve a evaluar y este paso aún no ha acabado no le salga
                    _happeningSteps.RemoveFirst();
                    int rand = UnityEngine.Random.Range(possiBleIndexesFirst, possibleIndexes.Length);
                    _OB[rand].setOB(eval.OB);
                    possibleIndexes[rand] = possibleIndexes[possiBleIndexesFirst];
                    possiBleIndexesFirst++;
                }

                //string comp = _CompetencesToOB.getCompetenceFromOB(myOB);
                //HashSet<string> OBSet = _CompetencesToOB.GetOBsFromCompetence(comp);
                //int fin = Math.Min(_OB.Length, OBSet.Count);

                ////quitamos del set el ob que no nos interesa
                //LinkedList<string> OBToRemove = new LinkedList<string>();
                //OBToRemove.AddFirst(new LinkedListNode<string>(myOB));
                //OBSet.ExceptWith(OBToRemove);
                //string[] allOBs = new string[OBSet.Count];

                ////Copia profunda
                //OBSet.CopyTo(allOBs);
                //OBToRemove.RemoveFirst();
                //int firstElement = 0;

                //cogemos uno al azar del array entre nuestro "primero" y el final
                //tras elegir el OB, ponemos nuestro "primero" en el índice del OB
                //y finalmente nuestro "primero" pasa a ser el siguiente. De esta forma
                //evitamos repeticiones y todos los OB tienen posibilidad de salir.
                //Repetimos este proceso hasta el correcto
                //int i = 0;
                //for (; i < _correct; i++)
                //{
                //    int index = UnityEngine.Random.Range(firstElement, allOBs.Length);
                //    string randOB = allOBs[index];
                //    allOBs[index] = allOBs[firstElement];
                //    firstElement++;
                //    _OB[i].setOB(randOB);
                //    _OB[i].gameObject.SetActive(true);
                //}

                ////colocamos el correcto
                //_OB[_correct].setOB(myOB);
                //_OB[_correct].gameObject.SetActive(true);
                //i++;

                ////proseguimos el proceso anterior hasta el final de las opciones
                //for (; i < fin; i++)
                //{
                //    int index = UnityEngine.Random.Range(firstElement, allOBs.Length);
                //    string randOB = allOBs[index];
                //    allOBs[index] = allOBs[firstElement];
                //    firstElement++;
                //    _OB[i].setOB(randOB);
                //    _OB[i].gameObject.SetActive(true);
                //}

                ////por si hay no hay suficientes OB
                //for (; i < _OB.Length; i++)
                //{
                //    _OB[i].gameObject.SetActive(false);
                //}
                _changedOB = false;
            }

        }
        public void evaluate(string oB, bool isPositive, Vector2 position)
        {
            //Logic.Step.Result r = _levelManager.getCurrentStep().result;
            ////todo supongo que aquí habría que hacer algo relacionado con el score pero no se muy bien como llevarlo
            //if (oB == _OB[_correct].getOB() &&
            //    ((isPositive && r == Logic.Step.Result.Good) || (!isPositive && r == Logic.Step.Result.Bad)))
            //{
            //    _resultText.setText("+1");
            //    _resultText.setColor(Color.green);

            //}
            //else
            //{
            //    _resultText.setText("-1");
            //    _resultText.setColor(Color.red);
            //}
            //_resultText.setPos(position);

            //_resultAnimator.Play("Fade Up", 0, 0);
        }

        //precondición: el paso es completamente nuevo, es decir, no es la continuación de un paso anterior (p. ej. si
        //para representar un OB x el personaje tiene que hablar y animarse aquí viene o el dialogo o la animacion, pero nunca los dos)
        public void NewStep(Step step)
        {
            if (step.posInList == 0)
            {
                _happeningSteps.AddLast(new LinkedListNode<Logic.Step>(step));
                _changedOB = true;
            }

        }

        public void EndStep(Step step)
        {
            if (step.posInList == step.numStepsInList - 1)
                _happeningSteps.Remove(step);
        }
    }
}
