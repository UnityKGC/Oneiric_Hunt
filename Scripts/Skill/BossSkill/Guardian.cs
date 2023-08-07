using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour
{
    private float _skillRange = 10f;

    private MonsterStat _monsterStat;

    float _startTime; // 시작시간
    float _remainingTime; // 남은시간

    float _duringTime = 3f; // 지속시간

    float _upHpValue = 0.2f; // 회복하는 체력 비율
    float _upDefValue = 0.2f; // 올라야 할 스탯 배율
    float _buffDuringTime = 10f; // 버프 지속 시간

    bool _isApplySkill = false;

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
        if (_isApplySkill) return; // 스킬을 적용시켰으면 리턴

        if (other.CompareTag("Monster")) // 스킬 범위 내에 몬스터가 존재한다면,
        {
            _monsterStat = other.GetComponent<MonsterStat>();
            if (_monsterStat != null)
            {
                float hp = _monsterStat.HP * _upHpValue; // 체력 회복
                _monsterStat.HP += hp;

                BuffManager._instance.StartDefBuff(other.gameObject, _upDefValue, _buffDuringTime);
            }
        }
    }
}
