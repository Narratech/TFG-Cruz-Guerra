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
            public Logic.Source source;
        }
        [SerializeField] TextModifier _resultText;
        [SerializeField] Animator _resultAnimator;
        [SerializeField] Transform[] _positions;
        [SerializeField] OBSelector[] _OB;
        Dictionary<KeyValuePair<string, Logic.Source>, int> _happeningOBs;
        bool _changedOB = true;
        //esto puede ser incluso de pair string y source con result por si queremos evaluar el source tambien o bien un hashset de steps
        HashSet<EvaluableInfo> _correctEvaluation;

        Logic.Table_CompetencesToOB _CompetencesToOB;
        private void Start()
        {
            _happeningOBs = new Dictionary<KeyValuePair<string, Source>, int>();
            _CompetencesToOB = GameManager.Instance.competencesToOB;
            _correctEvaluation = new HashSet<EvaluableInfo>();
            LevelManager.AddEndStepHandler(this);
            LevelManager.AddNewStepHandler(this);
        }
        public void setPos(int index)
        {
            if (index < _positions.Length && index >= 0)
                transform.position = _positions[index].position;
        }

        public void setRandomOBs(int Source)
        {
            //Si los ob son exactamente los mismos que cuando clickó no hacemos nada y nos quedamos con los mismos
            //todo sabemos de antemano cuantos ob distintos estan pasando, por lo que podemos poner para cada uno el correcto y los x incorrectos
            //todo que le toquen. X sería ob.length/n de obs pasando
            if (_changedOB)
            {
                //nos quitamos los de la evaluación anterior
                _correctEvaluation.Clear();
                //todo como los arrays tienen los valores por referencia (básicamente son arrays dinámicos, con punteros, se puede hacer un diccionario de competencias a array de obs y un
                //todo diccionario de competencias a primer índice y sacar el random de ahí
                Dictionary<string, string[]> competenceToFakeOptions = new Dictionary<string, string[]>();
                Dictionary<string, int> competenceToFirstIndex = new Dictionary<string, int>();

                foreach (var OBandSource in _happeningOBs.Keys)
                {
                    if (((int)OBandSource.Value) == Source)
                    {
                        string comp = _CompetencesToOB.getCompetenceFromOB(OBandSource.Key);
                        //si el ob es de una competencia que ya ha salido debemos "quitarlo de la lista de opciones falsas" poniendolo al principio
                        if (competenceToFakeOptions.ContainsKey(comp)){
                            int index = Utils.ArrayUtils.getIndex(competenceToFakeOptions[comp], OBandSource.Key, competenceToFirstIndex[comp]);
                            if (index != -1)
                            {
                                int first = competenceToFirstIndex[comp];
                                competenceToFakeOptions[comp][index] = competenceToFakeOptions[comp][first];
                                competenceToFirstIndex[comp]++;
                            }
                        }
                        else
                        {
                            //HashSet<>
                            //competenceToFirstIndex[comp] = 1;
                        }
                    }
                }


                //LinkedListNode<Logic.Step> currentNode = _happeningSteps.First;
                //List<string> happeningOB = new List<string>();
                //Dictionary<string, HashSet<string>> competencesToFakeOptions = new Dictionary<string, HashSet<string>>();
                ////nos guardamos en un conjunto de respuestas falsas todos los OB de las competencias que están ocurriendo ahora mismo
                ////y luego quitamos los OB que están ocurriendo de ese conjunto
                //for (int i = 0; i < _happeningSteps.Count; i++)
                //{
                //    string competence = _CompetencesToOB.getCompetenceFromOB(currentNode.Value.OB);
                //    if (!competencesToFakeOptions.ContainsKey(competence))
                //    {
                //        //ExceptWith es O(n) en el número de elementos a quitar, por lo que sería O(1) en estos casos
                //        HashSet<string> auxComp = _CompetencesToOB.GetOBsFromCompetence(competence);
                //        auxComp.ExceptWith(new string[] { currentNode.Value.OB });
                //        competencesToFakeOptions.Add(competence, auxComp);

                //    }
                //    else
                //    {
                //        competencesToFakeOptions[competence].ExceptWith(new string[] { currentNode.Value.OB });
                //    }
                //    happeningOB.Add(currentNode.Value.OB);
                //    currentNode = currentNode.Next;
                //}

                //currentNode = _happeningSteps.First;
                ////obtenemos de esta forma una serie de respuestas incorrectas de las que sacar de forma aleatoria y sin repeticiones
                ////fakeOptions.CopyTo(fakeOptionsArray);
                //int fakeOptionsFirst = 0;


                //hay una pequenia posibilidad de que entre respuestas correctas e incorrectas sumen menos que las opciones que damos
                //por lo que hay que rellenar el mínimo entre estos dos valores
                //int maxOptions = Math.Min(fakeOptionsNum + _happeningSteps.Count, _OB.Length);
                //int[] possibleIndexes = new int[maxOptions];
                //for (int i = 0; i < possibleIndexes.Length; i++)
                //{
                //    possibleIndexes[i] = i;
                //}
                //int possiBleIndexesFirst = 0;
                //int falseOBPerCompetence = (maxOptions - _happeningSteps.Count) / _happeningSteps.Count;

                //while (currentNode != null)
                //{

                //EvaluableInfo eval = new EvaluableInfo();
                //eval.OB = currentNode.Value.OB;
                //eval.result = currentNode.Value.result;
                //_correctEvaluation.Add(eval);
                //currentNode = currentNode.Next;
                ////lo quitamos para que si vuelve a evaluar y este paso aún no ha acabado no le salga
                //_happeningSteps.RemoveFirst();
                //int rand = UnityEngine.Random.Range(possiBleIndexesFirst, possibleIndexes.Length);
                //_OB[rand].setOB(eval.OB);
                //possibleIndexes[rand] = possibleIndexes[possiBleIndexesFirst];
                //possiBleIndexesFirst++;


                //for (int j = 0; j < falseOBPerCompetence; j++)
                //{
                //    rand = UnityEngine.Random.Range(possiBleIndexesFirst, possibleIndexes.Length);
                //    eval.OB =
                //    //_OB[rand].setOB(eval.OB);
                //    possibleIndexes[rand] = possibleIndexes[possiBleIndexesFirst];
                //    possiBleIndexesFirst++;

                //}
                //}

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
        public void NewStep(Step step, Source source)
        {
            //? ESTO SIGUE TENIENDO EL PROBLEMA DE QUE SI NOS PONEN UN OB POSITIVO Y EL MISMO OB NEGATIVO A LA VEZ NO VA A IR BIEN
            if (step.result != Step.Result.Neutral)
            {

                KeyValuePair<string, Logic.Source> pair = new KeyValuePair<string, Source>(step.OB, source);
                int sum = step.result == Step.Result.Good ? 1 : -1;
                if (!_happeningOBs.ContainsKey(pair))
                {
                    _happeningOBs[pair] = sum;
                    _changedOB = true;
                }
                else _happeningOBs[pair] += sum;
            }

        }

        public void EndStep(Step step, Source source)
        {
            if (step.result != Step.Result.Neutral)
            {
                KeyValuePair<string, Source> pair = new KeyValuePair<string, Source>(step.OB, source);
                if (_happeningOBs.ContainsKey(pair))
                {
                    int sum = step.result == Step.Result.Good ? -1 : 1;
                    _happeningOBs[pair] += sum;
                    if (_happeningOBs[pair] == 0)
                        _happeningOBs.Remove(pair);
                }
            }
        }
    }
}
