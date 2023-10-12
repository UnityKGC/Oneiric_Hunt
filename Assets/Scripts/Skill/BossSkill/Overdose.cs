using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overdose : MonoBehaviour
{
    float _startTime; // 시작시간
    float _remainingTime; // 남은시간

    float _duringTime = 10f; // 지속시간

    float _downHpValue = 0.05f; // 초당 깎이는 체력 비율(현재 체력)
    float _downHp; // 실제로 깎이는 체력
    float _upValue = 0.5f; // 올라야 할 공격력 스탯 배율
    float _buffDuringTime = 10f; // 버프 지속 시간

    BossStat _bossStat;
    void Start()
    {
        _startTime = Time.time;
        _bossStat = GetComponentInParent<BossStat>();
        _downHp = _bossStat.HP * _downHpValue;
        StartCoroutine(StartDamage());
        
        BuffManager._instance.StartBuff(BuffManager.BuffEffect.AtkUp, transform.parent.gameObject, _upValue, _buffDuringTime);

        BossSkillManager._instance._isSkilling = false;
        BossSkillManager._instance.EndSkill();
    }

    void Update()
    {
        _remainingTime = _duringTime - (Time.time - _startTime);
        if (_remainingTime >= 0f)
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
    IEnumerator StartDamage()
    {
        while(true)
        {
            _bossStat.SetDamage(_downHp);
            //_bossStat.HP -= _downHp;

            yield return new WaitForSeconds(1f);
        }
    }
}
