using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic;
using UnityEngine.UI;

public class PilotCompetencesPanelGenerator : MonoBehaviour
{
    private void Start()
    {
        _table = JsonManager.ImportFromJSON<Table_CompetencesToOB>(Application.persistentDataPath + "/Tables/TableCompetenceToOB.json");
        _creator.setTable(_table);       
        generate();
    }
    void generate()
    {
        float w = 0;
        foreach (string key in _table.getCompetences())
        {
            TextModifier modifier = Instantiate<TextModifier>(_CompetenceDifficultySelectorPrefab, _parent);
            modifier.setText(key);
            RectTransform rect = modifier.gameObject.GetComponent<RectTransform>();
            w += rect.rect.width + _layout.padding.left + _layout.spacing;
            CompetencesDifficultySelector selector = modifier.gameObject.GetComponent<CompetencesDifficultySelector>();
            selector.setCompetence(key);
            selector.setCreator(_creator);
        }
        _parent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
    }
    [SerializeField] TextModifier _CompetenceDifficultySelectorPrefab;
    [SerializeField] RectTransform _parent;
    [SerializeField] HorizontalLayoutGroup _layout;
    [SerializeField] PilotCreator _creator;
    Table_CompetencesToOB _table;

}
