using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tfg.Interfaces;
using Logic;
using System.Linq;
using UnityEngine.Events;

namespace tfg
{
    public class Evaluator : MonoBehaviour, INewStepHandler, IEndStepHandler
    {
        public struct EvaluableInfo
        {
            public EvaluableInfo(string ob, Step.Result r)
            {
                OB = ob;
                result = r;
            }
            public string OB;
            public Step.Result result;
        }
        [SerializeField] bool _tutorial;
        [SerializeField] UnityEvent _onTutorialMistake;
        [SerializeField] TextModifier _resultText;
        [SerializeField] Animator _resultAnimator;
        [SerializeField] Transform[] _positions;
        [SerializeField] OBSelector[] _OB;
        [SerializeField] ResultsTracker _resultsTracker;
        Dictionary<KeyValuePair<string, Logic.Source>, int> _happeningOBs;
        Dictionary<Source, bool> _changedOB;
        //usamos Dictionary porque queremos que saber si está o no sea O(1) y guardamos en el bool si previamente se habia detectado ese OB( es decir, 
        //el usuario habia visto el OB pero lo habia calificado incorrectamente)
        Dictionary<Logic.Source, HashSet<EvaluableInfo>> _correctEvaluation;
        Logic.Source _currentSource;
        Logic.Table_CompetencesToOB _CompetencesToOB;

        private void Start()
        {
            _changedOB = new Dictionary<Source, bool>();
            _changedOB[Source.Captain] = true;
            _changedOB[Source.First_Officer] = true;
            _happeningOBs = new Dictionary<KeyValuePair<string, Source>, int>();
            _CompetencesToOB = GameManager.Instance.competencesToOB;
            _correctEvaluation = new Dictionary<Source, HashSet<EvaluableInfo>>();
            _correctEvaluation[Source.Captain] = new HashSet<EvaluableInfo>();
            _correctEvaluation[Source.First_Officer] = new HashSet<EvaluableInfo>();
            LevelManager.AddEndStepHandler(this);
            LevelManager.AddNewStepHandler(this);
            foreach (OBSelector oBSelector in _OB)
            {
                oBSelector.Tutorial = _tutorial;
                oBSelector.OnTutorialMistake = _onTutorialMistake;
            }
        }
        public void setPos(int index)
        {
            if (index < _positions.Length && index >= 0)
                transform.position = _positions[index].position;
        }
        public bool getTutorial()
        {
            return _tutorial;
        }
        public void cannotEvaluate()
        {
            foreach (OBSelector selector in _OB)
            {
                selector.CanEvaluate = false;
            }
        }
        public void canEvaluate()
        {
            foreach (OBSelector selector in _OB)
            {
                selector.CanEvaluate = true;
            }
        }
        public void setRandomOBs(int Source)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            //! Precondicion: Hay como maximo 3 obs buenos del source adecuado
            //Si los ob son exactamente los mismos que cuando clickó no hacemos nada y nos quedamos con los mismos
            Logic.Source parsedSource = (Logic.Source)(Source);
            if (_changedOB[parsedSource])
            {
                _currentSource = parsedSource;
                //nos quitamos los de la evaluación anterior
                _correctEvaluation[_currentSource].Clear();


                prepareFakeOptions(out Dictionary<string, string[]> competenceToFakeOptions, out Dictionary<string, int> competenceToFirstIndex, Source);


                int totalOBs = 0;

                //contamos los OB malos
                foreach (var comp in competenceToFakeOptions)
                {
                    totalOBs += (comp.Value.Length - competenceToFirstIndex[comp.Key]);
                }

                //le añadimos los OB buenos
                int correctOB = 0;
                foreach (var ob in _happeningOBs.Keys)
                {
                    if (ob.Value == _currentSource)
                        correctOB++;
                }
                totalOBs += correctOB;

                int maxObsToSelect = Math.Min(_OB.Length, totalOBs);
                int OBGroups = correctOB == 0 ? _OB.Length : _OB.Length / correctOB;

                int[] indexes = new int[maxObsToSelect];
                for (int j = 0; j < maxObsToSelect; j++)
                {
                    indexes[j] = j;
                }

                int firstIndex = 0;
                //si solo hay uno significa que no había nada que evaluar y se han metido todas falsas.
                //En caso de que solo hubiera una competencia para evaluar habría dos elementos porque se habría metido otra random
                //para despistar
                if (competenceToFakeOptions.Keys.Count == 1)
                {
                    int fakesFirst = 0;
                    string[] fakes = competenceToFakeOptions.ElementAt(0).Value;
                    for (int i = 0; i < _OB.Length; i++)
                    {
                        int rand = UnityEngine.Random.Range(firstIndex, fakes.Length);
                        string falseOb = fakes[rand];
                        putOBinRandomIndex(falseOb, indexes, ref firstIndex);
                        fakes[rand] = fakes[fakesFirst];
                        fakesFirst++;
                    }
                }
                else
                {
                    foreach (var obs in _happeningOBs)
                    {
                        //si el ob no es de nuestro source lo ignoramos
                        if (((int)obs.Key.Value) != Source)
                            continue;

                        //colocamos el correcto
                        EvaluableInfo info = new EvaluableInfo();
                        info.OB = obs.Key.Key;
                        info.result = obs.Value > 0 ? Step.Result.Good : Step.Result.Bad;
                        _correctEvaluation[_currentSource].Add(info);
                        putOBinRandomIndex(info.OB, indexes, ref firstIndex);

                        //rellenamos con obs de pega y empezamos por 1 porque contamos el correcto
                        string competence = _CompetencesToOB.getCompetenceFromOB(info.OB);
                        for (int i = 1; i < OBGroups; i++)
                        {
                            int first = competenceToFirstIndex[competence];
                            int rand = UnityEngine.Random.Range(first, competenceToFakeOptions[competence].Length);
                            putOBinRandomIndex(competenceToFakeOptions[competence][rand], indexes, ref firstIndex);
                            competenceToFakeOptions[competence][rand] = competenceToFakeOptions[competence][first];
                            competenceToFirstIndex[competence]++;
                        }


                    }
                }

                _changedOB[_currentSource] = false;
            }

        }
        /// <summary>
        /// comprueba si la evaluacion dada por el jugador es correcta
        /// </summary>
        /// <param name="oB">OB que ha seleccionado el jugador</param>
        /// <param name="isPositive">Si ha seleccionado positivo</param>
        /// <param name="info">un EvaluableInfo ya formado. Si se pasa null se creara uno </param>
        /// <returns>True si el jugador ha acertado</returns>
        public bool evaluate(EvaluableInfo info)
        {

            return _correctEvaluation[_currentSource].Contains(info);
        }
        public void evaluate(string oB, bool isPositive)
        {
            EvaluableInfo info = new EvaluableInfo();
            info.OB = oB;
            info.result = isPositive ? Step.Result.Good : Step.Result.Bad;
            bool correct = evaluate(info);
            if (correct)
            {
                //si ha acertado lo quitamos para la próxima de las buenas (aunque seguira apareciendo como opción)
                _correctEvaluation[_currentSource].Remove(info);
                _resultText.setText("+1");
                _resultText.setColor(Color.green);
                _resultsTracker.detect(ResultsTracker.OBDetection.Correct);
            }
            else
            {
                _resultText.setText("-1");
                _resultText.setColor(Color.red);
                Step.Result opposite = info.result == Step.Result.Good ? Step.Result.Bad : Step.Result.Good;
                info.result = opposite;
                //si el OB que ha marcado no estaba de ninguna forma, es imposible haberlo detectado anteriormente
                _resultsTracker.detect(ResultsTracker.OBDetection.Incorrect);
            }

            _resultAnimator.Play("Fade Up", 0, 0);

            GameManager.Instance.levelManager.setScaleTime(1);
        }

        public void OnNewStep(Step step, Source source, int remainingSteps)
        {
            //? ESTO SIGUE TENIENDO EL PROBLEMA DE QUE SI NOS PONEN UN OB POSITIVO Y EL MISMO OB NEGATIVO A LA VEZ NO VA A IR BIEN
            //! NO SE PUEDE PERMITIR QUE SE SUPERPONGAN OBS
            if (step.result != Step.Result.Neutral && step.OB != null)
            {
                KeyValuePair<string, Logic.Source> pair = new KeyValuePair<string, Source>(step.OB, source);
                int sum = step.result == Step.Result.Good ? 1 : -1;
                if (!_happeningOBs.ContainsKey(pair))
                {
                    _happeningOBs[pair] = sum;
                    _changedOB[source] = true;
                }
                else _happeningOBs[pair] += sum;
                if (step.OB != null && step.OB != "")
                    _resultsTracker.newOB();
            }

        }

        public void OnEndStep(Step step, Source source, int remainingSteps)
        {
            if (step.result != Step.Result.Neutral && step.OB != null)
            {
                KeyValuePair<string, Source> pair = new KeyValuePair<string, Source>(step.OB, source);
                if (_happeningOBs.ContainsKey(pair))
                {
                    int sum = step.result == Step.Result.Good ? -1 : 1;
                    _happeningOBs[pair] += sum;
                    if (_happeningOBs[pair] == 0)
                        _happeningOBs.Remove(pair);
                }
                if (remainingSteps == 0)
                    _resultsTracker.inform();
            }
        }
        void prepareFakeOptions(out Dictionary<string, string[]> competenceToFakeOptions, out Dictionary<string, int> competenceToFirstIndex, int Source)
        {
            competenceToFakeOptions = new Dictionary<string, string[]>();
            competenceToFirstIndex = new Dictionary<string, int>();

            foreach (var OBandSource in _happeningOBs.Keys)
            {
                if (((int)OBandSource.Value) == Source)
                {
                    string comp = _CompetencesToOB.getCompetenceFromOB(OBandSource.Key);
                    //si el ob es de una competencia que ya ha salido debemos "quitarlo de la lista de opciones falsas" poniendolo al principio
                    if (competenceToFakeOptions.ContainsKey(comp))
                    {
                        int index = Utils.ArrayUtils.getIndex(competenceToFakeOptions[comp], OBandSource.Key, competenceToFirstIndex[comp]);
                        if (index != -1)
                        {
                            int first = competenceToFirstIndex[comp];
                            competenceToFakeOptions[comp][index] = competenceToFakeOptions[comp][first];
                            competenceToFirstIndex[comp]++;
                        }
                    }
                    //si no se le quita del todo el ob, ya que no queremos cogerlo por error en las falsas y metemos la lista
                    //Aqui no buscamos ya que exceptWith es O(n) en Other, que en este caso siempre es 1, mientras que hacer la busqueda
                    //y colocar al principio es O(n) en la propia lista, por lo cual el exceptWith va a ser mas rapido
                    else
                    {
                        HashSet<string> obs = _CompetencesToOB.GetOBsFromCompetence(comp);
                        obs.ExceptWith(new string[] { OBandSource.Key });
                        competenceToFakeOptions[comp] = new string[obs.Count];
                        obs.CopyTo(competenceToFakeOptions[comp]);
                        competenceToFirstIndex[comp] = 0;
                    }
                }
            }
            //si solo hay una competencia metemos otra para despistar
            if (competenceToFakeOptions.Keys.Count <= 1)
            {
                var competences = _CompetencesToOB.getCompetences();
                string[] competencesArray = new string[competences.Count];
                competences.CopyTo(competencesArray, 0);
                string myComp = competenceToFakeOptions.Keys.Count == 0 ? "" : competenceToFakeOptions.ElementAt(0).Key;
                string randComp = myComp;
                do
                {
                    randComp = competencesArray[UnityEngine.Random.Range(0, competencesArray.Length)];
                } while (randComp == myComp && competenceToFakeOptions.Keys.Count == 1);
                HashSet<string> randOBs = _CompetencesToOB.GetOBsFromCompetence(randComp);
                competenceToFakeOptions[randComp] = new string[randOBs.Count];
                randOBs.CopyTo(competenceToFakeOptions[randComp]);
                competenceToFirstIndex[randComp] = 0;
            }

        }
        void putOBinRandomIndex(string ob, int[] indexes, ref int firstIndex)
        {
            int rand = UnityEngine.Random.Range(firstIndex, indexes.Length);
            int myIndex = indexes[rand];
            indexes[rand] = indexes[firstIndex];
            firstIndex++;
            _OB[myIndex].setOB(ob);
        }

    }
}
