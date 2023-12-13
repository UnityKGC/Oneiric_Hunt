using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvas : MonoBehaviour
{
    public Transform _buffGridLayout;
    public Image _midImg; // HP 데미지 받을 시, 보이는 흰색부분
    public Image _frontImg; // HP front부분

    public List<GameObject> _buffList; // 버프 Prefab 리스트 => 순서는 버프매니저의 버프 타입 enum 순서.

    private Transform _camTrans;

    private WaitForEndOfFrame _waitFrame = new WaitForEndOfFrame();

    private float _damageSpd = 0.02f;
    [SerializeField] private float _remainDmg; // 받은 데미지 저장하는 변수

    [SerializeField] private float _nowHP = 1f; // 현재 남은 체력 => 최대체력 비율;
    [SerializeField] private float _damage; // 받은 데미지
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
    public void SetHPAmount(float value) // 남은 HP 비율, value가 (현재체력 / 최대체력)의 값이다.
    {
        _frontImg.fillAmount = value;

        StopAllCoroutines();

        _damage = _nowHP - value;
        _nowHP -= _damage;

        StartCoroutine(DamageUI());
    }
    void StartBuffUI(Transform obj, BuffManager.BuffEffect type, float time) // 버프 지속 시간을 인자로 받아, UI 구현.
    {
        if (obj != transform.parent) return; // 버프받을 대상이 본인이 아니라면 리턴

        BuffUIDuration ui = Instantiate(_buffList[(int)type], _buffGridLayout).GetComponent<BuffUIDuration>(); // BuffUI 스크립트를 지닌 본인이 GridLayOutGroup을 지니고 있기에 본인을 부모로 설정

        ui.Init(time);
    }
    IEnumerator DamageUI() // 데미지 받은 부분(흰색)을 감소시키는 코루틴
    {
        while(_damage > 0) // 받은 비율만큼 UI가 감소한다. value를 damagedSpd만큼 감소시키게 만들어, 0보다 작거나 같아지면 멈춘다.
        {
            _midImg.fillAmount -= _damageSpd;

            _damage -= _damageSpd;

            yield return _waitFrame;
        }
        
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
        UIManager._instacne._enemyBuffEvt -= StartBuffUI;
    }
}
