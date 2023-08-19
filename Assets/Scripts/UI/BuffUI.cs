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

    void StartBuffUI(BuffManager.BuffType type, float time) // ���� ���� �ð��� ���ڷ� �޾�, UI ����.
    {
        BuffUIDuration ui = Instantiate(_buffList[(int)type], transform).GetComponent<BuffUIDuration>(); // BuffUI ��ũ��Ʈ�� ���� ������ GridLayOutGroup�� ���ϰ� �ֱ⿡ ������ �θ�� ����
        
        ui.Init(time);
        /* Ȥ�ó� �����丵 �ϰ� ���� �ʿ��� �� �����ϱ� ����
        switch (type)
        {
            case BuffManager.BuffType.AtkUp:
                break;
            case BuffManager.BuffType.AtkDown:
                break;
            case BuffManager.BuffType.DefUp:
                break;
            case BuffManager.BuffType.DefDown:
                break;
            case BuffManager.BuffType.MovSpdUp:
                break;
            case BuffManager.BuffType.MovSpdDown:
                break;
        }
        */
    }
    private void OnDestroy()
    {
        UIManager._instacne._buffEvt -= StartBuffUI;
    }
}
