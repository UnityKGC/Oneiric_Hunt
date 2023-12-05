using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{
    float _maxHP;

    public Image _hpImg;

    //private Transform _camTrans;
    void Start()
    {
        //_camTrans = Camera.main.transform;

        UIManager._instacne._hpEvt -= SetUI;
        UIManager._instacne._hpEvt += SetUI;

        _maxHP = GameObject.FindWithTag("Player").GetComponent<PlayerStat>().MaxHp; // 시작할 때 플레이어를 찾은 후, 최대체력을 가져온다. => 최대체력은 변하지 않음
    }

    void Update()
    {
        //transform.LookAt(_camTrans);
    }
    void SetUI(float value) // 플레이어의 HP에 증감 발생
    {
        _hpImg.fillAmount = value / _maxHP; // 현재체력 / 최대체력의 비율을 _hpImg의 fillAmount에 적용시킨다.
    }
}
