using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ScenarioItemHandler : MonoBehaviour
{
    List<StepItem> _items;
    [SerializeField] StepItem _prefab;
    [SerializeField] PopUpPanel _panel;
    [SerializeField] Transform _parent;
    const float treshold = 10;
    private void Start()
    {
        _items = new List<StepItem>();
    }
    
    public void Add(Logic.Step step)
    {
        StepItem item = Instantiate<StepItem>(_prefab, _parent);
        item.stepInfo = step;
        item.initTime = step.startTime;
        item.endTime = item.initTime + step.duration;
        item.Panel = _panel;
        _items.Add(item);
        item.gameObject.GetComponent<TextModifier>().setText(step.OB);

        int overlaps = howManyOverlaps(item.initTime, item.endTime);
        if (overlaps > 0)
            item.transform.Translate(Vector3.down * overlaps * (((RectTransform)item.transform).rect.height + treshold));

    }
    public void changeTime(int index, float newInit, float dur)
    {
        float fin = newInit + dur;
        _items[index].initTime = newInit;
        _items[index].endTime = fin;
        int overlaps = howManyOverlaps(newInit, fin);
        if (overlaps > 0)
            _items[index].transform.Translate(Vector3.down * overlaps * (((RectTransform)_items[index].transform).rect.height + treshold));
    }
    int howManyOverlaps(float init, float fin)
    {
        int overlaps = 0;
        foreach (StepItem item in _items)
        {

            float itemFin = item.stepInfo.startTime + item.stepInfo.duration;
            if (init >= item.initTime && init <= itemFin || fin >= item.initTime && fin <= itemFin)
                overlaps++;
        }
        //asumimos que colisionamos con nosotros mismos por lo que no contamos esa
        return overlaps - 1;
    }


}
