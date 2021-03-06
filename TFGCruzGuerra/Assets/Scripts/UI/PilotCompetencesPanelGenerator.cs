using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic;
using UnityEngine.UI;
namespace tfg
{

    public class PilotCompetencesPanelGenerator : MonoBehaviour
    {
        [SerializeField] TextModifier _CompetenceDifficultySelectorPrefab;
        [SerializeField] RectTransform _parent;
        [SerializeField] HorizontalLayoutGroup _layout;
        [SerializeField] PilotCreator _creator;
        Table_CompetencesToOB _table;
        CompetencesDifficultySelector[] _selectors;

        private void Start()
        {
            _table = GameManager.Instance.competencesToOB;
            _selectors = new CompetencesDifficultySelector[_table.getNumCompetences()];
            generate();
        }
        void generate()
        {
            float w = 0;
            int i = 0;
            foreach (string key in _table.getCompetences())
            {
                TextModifier modifier = Instantiate<TextModifier>(_CompetenceDifficultySelectorPrefab, _parent);
                modifier.setText(key);
                RectTransform rect = modifier.gameObject.GetComponent<RectTransform>();
                w += rect.rect.width + _layout.padding.left + _layout.spacing;
                CompetencesDifficultySelector selector = modifier.gameObject.GetComponent<CompetencesDifficultySelector>();
                selector.setCompetence(key);
                selector.setCreator(_creator);
                _selectors[i] = selector;
                i++;
            }
            _parent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        }
        public void reset()
        {
            foreach (CompetencesDifficultySelector selector in _selectors)
            {
                selector.reset();
            }
        }

    }
}
