using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchUI : MonoBehaviour
{
    float _max = 800;
    float _now = 0;

    public RectTransform _gaugeValue;

    public List<Image> _btnLst;
    void Awake()
    {
        UIManager._instacne._catchUIEvt -= SetCatchUI;
        UIManager._instacne._catchUIEvt += SetCatchUI;
    }
    void SetCatchUI(float value) // Catch�� ��ü�� ���� ������ ������ value�� �޴´�.
    {
        _now = _max * value;

        Vector2 temp = _gaugeValue.sizeDelta;
        temp.x = _now;
        _gaugeValue.sizeDelta = temp;
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void OnDestroy()
    {
        
    }
}
