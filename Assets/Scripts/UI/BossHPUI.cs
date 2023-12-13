using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    [SerializeField] Image _bossHP;
    [SerializeField] Image _midImg; // HP 데미지 받을 시, 보이는 흰색부분

    [SerializeField] private float _remainDmg; // 받은 데미지 저장하는 변수
    [SerializeField] private float _nowHP = 1f; // 현재 남은 체력 => 최대체력 비율;
    [SerializeField] private float _damage; // 받은 데미지

    private WaitForEndOfFrame _waitFrame = new WaitForEndOfFrame();

    private float _damageSpd = 0.02f;
    

    void Start()
    {
        // 이벤트 등록
        UIManager._instacne._setBossHPUI -= SetBossHP;
        UIManager._instacne._setBossHPUI += SetBossHP;

        UIManager._instacne._bossHPEvt -= SetHPAmount;
        UIManager._instacne._bossHPEvt += SetHPAmount;

        gameObject.SetActive(false); // 처음 켜지면 이벤트 등록 후, 꺼진다. 
    }
    void SetBossHP(bool value)
    {
        gameObject.SetActive(value);
    }
    void SetHPAmount(float value) // 남은 HP 비율
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);

        _bossHP.fillAmount = value;

        StopAllCoroutines();

        _damage = _nowHP - value;

        _remainDmg += _damage;

        _nowHP -= _damage;

        StartCoroutine(DamageUI());

    }
    IEnumerator DamageUI() // 데미지 받은 부분(흰색)을 감소시키는 코루틴
    {
        yield return new WaitForSeconds(0.1f);
        while (_remainDmg > 0) // 받은 비율만큼 UI가 감소한다. value를 damagedSpd만큼 감소시키게 만들어, 0보다 작거나 같아지면 멈춘다.
        {
            _midImg.fillAmount -= _damageSpd;

            _remainDmg -= _damageSpd;

            yield return _waitFrame;
        }

    }

    private void OnDestroy()
    {
        UIManager._instacne._bossHPEvt -= SetHPAmount;
    }
}
