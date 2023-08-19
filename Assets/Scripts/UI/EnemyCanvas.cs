using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvas : MonoBehaviour
{
    public Transform _buffGridLayout;
    public Image _frontImg; // HP front부분

    public List<GameObject> _buffList; // 버프 Prefab 리스트 => 순서는 버프매니저의 버프 타입 enum 순서.

    private Transform _camTrans;
    void Start()
    {
        _camTrans = Camera.main.transform;

        UIManager._instacne._enemyBuffEvt -= StartBuffUI;
        UIManager._instacne._enemyBuffEvt += StartBuffUI;
    }
    
    void Update()
    {
        transform.LookAt(_camTrans);
    }
    public void SetHPAmount(float value) // 남은 HP 비율
    {
        _frontImg.fillAmount = value;
    }
    void StartBuffUI(Transform obj, BuffManager.BuffEffect type, float time) // 버프 지속 시간을 인자로 받아, UI 구현.
    {
        if (obj != transform.parent) return; // 버프받을 대상이 본인이 아니라면 리턴

        BuffUIDuration ui = Instantiate(_buffList[(int)type], _buffGridLayout).GetComponent<BuffUIDuration>(); // BuffUI 스크립트를 지닌 본인이 GridLayOutGroup을 지니고 있기에 본인을 부모로 설정

        ui.Init(time);
    }
    private void OnDestroy()
    {
        UIManager._instacne._enemyBuffEvt -= StartBuffUI;
    }
}
