using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ScenarioItemHandler : MonoBehaviour
{
    List<StepItem> _items;
    [SerializeField] StepItem _prefab;
    [SerializeField] PopUpPanel _panel;
    [SerializeField] RectTransform _scrollContent;
    float _furthestLimit = 0;
    const float treshold = 10;
    const float unitSize = 20;
    private void Start()
    {
        _items = new List<StepItem>();
    }

    public void Add(Logic.Step step,StepCreator creator)
    {
        StepItem item = Instantiate<StepItem>(_prefab, _scrollContent);
        item.stepInfo = step;
        item.initTime = step.startTime;
        item.endTime = item.initTime + step.duration;
        item.Panel = _panel;
        item.Creator = creator;
        float translate = item.initTime * unitSize;
        item.transform.Translate(Vector3.right * translate);
        RectTransform tr = item.transform as RectTransform;
        float size = unitSize * (item.endTime - item.initTime);
        tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        float limit = translate + size;
        if (limit > _furthestLimit) {
            _furthestLimit = limit;
            _scrollContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, /*_scrollContent.rect.width + */_furthestLimit);
        
        }
        _items.Add(item);
        item.gameObject.GetComponent<TextModifier>().setText(step.OB);

        int overlaps = howManyOverlaps(item.initTime, item.endTime);
        if (overlaps > 0)
        {
            item.transform.Translate(Vector3.down * overlaps * (((RectTransform)item.transform).rect.height + treshold));
            _scrollContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _scrollContent.rect.height
                + (item.transform as RectTransform).rect.height * overlaps);
        }

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
