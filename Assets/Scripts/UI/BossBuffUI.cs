using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBuffUI : MonoBehaviour
{
    public List<GameObject> _buffList; // 버프 Prefab 리스트 => 순서는 버프매니저의 버프 타입 enum 순서.

    void Start()
    {
        UIManager._instacne._bossBuffEvt -= StartBuffUI;
        UIManager._instacne._bossBuffEvt += StartBuffUI;
    }

    void StartBuffUI(BuffManager.BuffEffect type, float time) // 버프 지속 시간을 인자로 받아, UI 구현.
    {
        BuffUIDuration ui = Instantiate(_buffList[(int)type], transform).GetComponent<BuffUIDuration>(); // BuffUI 스크립트를 지닌 본인이 GridLayOutGroup을 지니고 있기에 본인을 부모로 설정

        ui.Init(time);
       
    }
    private void OnDestroy()
    {
        UIManager._instacne._buffEvt -= StartBuffUI;
    }
}
