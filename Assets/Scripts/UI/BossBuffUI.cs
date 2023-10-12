using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBuffUI : MonoBehaviour
{
    public List<GameObject> _buffList; // ���� Prefab ����Ʈ => ������ �����Ŵ����� ���� Ÿ�� enum ����.

    void Start()
    {
        UIManager._instacne._bossBuffEvt -= StartBuffUI;
        UIManager._instacne._bossBuffEvt += StartBuffUI;
    }

    void StartBuffUI(BuffManager.BuffEffect type, float time) // ���� ���� �ð��� ���ڷ� �޾�, UI ����.
    {
        BuffUIDuration ui = Instantiate(_buffList[(int)type], transform).GetComponent<BuffUIDuration>(); // BuffUI ��ũ��Ʈ�� ���� ������ GridLayOutGroup�� ���ϰ� �ֱ⿡ ������ �θ�� ����

        ui.Init(time);
       
    }
    private void OnDestroy()
    {
        UIManager._instacne._buffEvt -= StartBuffUI;
    }
}
