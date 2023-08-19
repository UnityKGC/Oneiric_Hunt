using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stench : MonoBehaviour
{
    float _startTime; // 시작시간
    float _remainingTime; // 남은시간

    float _duringTime = 10f; // 지속시간

    float _deBuffValue = 0.3f; // 디버프 감소 값

    float _deBuffDuringTime = 10f; // 디버프 지속시간

    float _statusDmgValue = 0.01f; // 상태이상(독) 감소 값 => 초당 1%씩 감소

    float _statusDuringTime = 10f; // 상태이상 지속시간
    void Start()
    {
        _startTime = Time.time;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StatusManager._instance.StartPoison(other.gameObject, _statusDmgValue, _statusDuringTime);

            BuffManager._instance.StartDeBuff(BuffManager.BuffEffect.MovSpdDown, other.gameObject, _deBuffValue, _deBuffDuringTime);
        }
    }
}
