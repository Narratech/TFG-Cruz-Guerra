using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace tfg
{

    public class Evaluator : MonoBehaviour
    {
        private void Start()
        {
            _CompetencesToOB = Logic.JsonManager.ImportFromJSON<Logic.Table_CompetencesToOB>(Application.persistentDataPath + "/Tables/TableCompetenceToOB.json");
        }
        public void setPos(int index)
        {
            if (index < _positions.Length && index >= 0)
                transform.position = _positions[index].position;
        }
        public void setRandomOBs()
        {
            //todo cambiar esto. Las tablas van en el gameManager
            string myOB = _levelManager.getCurrentStep().OB;
            string comp = _CompetencesToOB.getCompetenceFromOB(myOB);
            HashSet<string> myCompetences = _CompetencesToOB.GetOBsFromCompetence(comp);
            //quitamos del set el ob que no nos interesa
            LinkedList<string> OBToRemove = new LinkedList<string>();
            OBToRemove.AddFirst(new LinkedListNode<string>(myOB));
            myCompetences.ExceptWith(OBToRemove);

            _correct = UnityEngine.Random.Range(0, _OB.Length);
            OBToRemove.RemoveFirst();
            for (int i = 0; i < _correct; i++)
            {
                string randOB = /*myCompetences.get()*/"2";
                _OB[i].setOB(randOB);
                OBToRemove.AddFirst(new LinkedListNode<string>(randOB));
                myCompetences.ExceptWith(OBToRemove);
                OBToRemove.RemoveFirst();
            }
            _OB[_correct].setOB(myOB);
            for (int i = _correct + 1; i < _OB.Length; i++)
            {
                string randOB = /*myCompetences.get()*/"1";
                _OB[i].setOB(randOB);
                OBToRemove.AddFirst(new LinkedListNode<string>(randOB));
                myCompetences.ExceptWith(OBToRemove);
                OBToRemove.RemoveFirst();
            }
        }
        public void evaluate(string oB, bool isPositive,Vector2 position)
        {
            Logic.Step.Result r = _levelManager.getCurrentStep().result;
            //todo supongo que aquí habría que hacer algo relacionado con el score pero no se muy bien como llevarlo
            if (oB == _OB[_correct].getOB() &&
                (isPositive && r == Logic.Step.Result.Good) || (!isPositive && r == Logic.Step.Result.Bad))
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
            //_resultAnimator.Play("Fade Up");

        }
        int _correct;
        [SerializeField] TextModifier _resultText;
        [SerializeField] Animator _resultAnimator;
        [SerializeField] Transform[] _positions;
        [SerializeField] LevelManager _levelManager;
        [SerializeField] OBSelector[] _OB;
        Logic.Table_CompetencesToOB _CompetencesToOB;

    }
}
