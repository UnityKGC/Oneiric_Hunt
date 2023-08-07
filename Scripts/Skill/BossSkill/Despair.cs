using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despair : MonoBehaviour
{

    private float _skillRange = 10f;

    private int _mask = 1 << 3; // Overlap할때 사용할 LayerMask => 플레이어를 지정한다.

    private PlayerStat _playerStat;

    float _startTime; // 시작시간
    float _remainingTime; // 남은시간

    float _duringTime = 2f; // 지속시간

    float _downValue = 0.3f; // 떨어져야 할 스탯 배율
    float _deBuffDuringTime = 10f; // 디버프 지속 시간

    float _downDefense; // 감소된 방어력

    float _downSwordMinAtk; // 감소된 검 최소공격력
    float _downSwordMaxAtk; // 감소된 검 최소공격력

    float _downSpearMinAtk;
    float _downSpearMaxAtk;

    float _downAxeMinAtk;
    float _downAxeMaxAtk;

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

        if(other.CompareTag("Player")) // 스킬 범위 내에 플레이어가 존재한다면,
        {
            _playerStat = other.GetComponent<PlayerStat>();
            if (_playerStat != null)
            {
                // 버프매니저에게 버프를 사용한다고 알림
                // BuffManager._instance.StartDeBuff(버프받을 대상, 버프 종류, 증감 비율값, );
                BuffManager._instance.StartAtkDeBuff(GameManager._instance.Player, _downValue, _deBuffDuringTime);
                BuffManager._instance.StartDefDeBuff(GameManager._instance.Player, _downValue, _deBuffDuringTime);
            }
        }
    }
}
