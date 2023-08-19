using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour
{
    public List<GameObject> _buffList; // ���� Prefab ����Ʈ => ������ �����Ŵ����� ���� Ÿ�� enum ����.
    
    void Start()
    {
        UIManager._instacne._buffEvt -= StartBuffUI;
        UIManager._instacne._buffEvt += StartBuffUI;
    }

    void StartBuffUI(BuffManager.BuffEffect type, float time) // ���� ���� �ð��� ���ڷ� �޾�, UI ����.
    {
        BuffUIDuration ui = Instantiate(_buffList[(int)type], transform).GetComponent<BuffUIDuration>(); // BuffUI ��ũ��Ʈ�� ���� ������ GridLayOutGroup�� ���ϰ� �ֱ⿡ ������ �θ�� ����
        
        ui.Init(time);
        /* Ȥ�ó� �����丵 �ϰ� ���� �ʿ��� �� �����ϱ� ����
        switch (type)
        {
            case BuffManager.BuffEffect.AtkUp:
                break;
            case BuffManager.BuffEffect.AtkDown:
                break;
            case BuffManager.BuffEffect.DefUp:
                break;
            case BuffManager.BuffEffect.DefDown:
                break;
            case BuffManager.BuffEffect.MovSpdUp:
                break;
            case BuffManager.BuffEffect.MovSpdDown:
                break;
        }
        */
    }
    private void OnDestroy()
    {
        UIManager._instacne._buffEvt -= StartBuffUI;
    }
}
