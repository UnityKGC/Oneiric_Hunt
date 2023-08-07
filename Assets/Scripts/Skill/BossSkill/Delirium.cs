using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Monster;

public class Delirium : MonoBehaviour
{
    float _startTime; // 시작시간
    float _remainingTime; // 남은시간

    float _duringTime = 5f; // 지속시간

    float _statusDuringTime = 10f; // 상태이상 지속시간
    void Start()
    {
        _startTime = Time.time;
    }

    void Update()
    {
        _remainingTime = _duringTime - (Time.time - _startTime);
        if (_remainingTime >= 0f)
        {
            return;
        }
        else
        {
            BossSkillManager._instance._isSkilling = false;
            BossSkillManager._instance.EndSkill();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) // 스킬 범위
    {
        if (other.CompareTag("Player")) // 스킬 범위 내에 몬스터가 존재한다면,
        {
            StatusManager._instance.StartConfusion(other.gameObject, _statusDuringTime);
        }
    }
}
