using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    SkillScriptable _scriptable;

    private Collider[] _colls;

    float _startTime; // 시작시간
    float _remainingTime; // 남은시간
    float _duringTime = 10f; // 지속시간

    float _dmgAmount = 5f; // 스킬 범위
    float _atkPer = 7f; // 플레이어 공격력에 추가한 공격력 %

    float _atk; // 최종 스킬 공격력

    float _upAtkValue; // 올라야 할 스탯 배율
    float _upDefValue;

    float _downAtkValue;
    float _downDefValue; // 감소되야 할 스탯 비율

    float _buffDuringTime; // 버프 지속 시간

    int _layerMask = (1 << 7) | (1 << 10); // 몬스터와 보스의 Layer만 체크
    
    public void Init(SkillScriptable scriptable, float playerAtk)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _atk = playerAtk * _scriptable._damageValue;
        _dmgAmount = _scriptable._damageAmount;

        _duringTime = _scriptable._durationTime;

        _buffDuringTime = _scriptable._buffDurationTime;

        _upAtkValue = _scriptable._atkBuffValue;
        _upDefValue = _scriptable._defBuffValue;

        _downAtkValue = _scriptable._atkDeBuffValue;
        _downDefValue = _scriptable._defDeBuffValue;
    }

    void Start()
    {
        _startTime = Time.time;

        GameObject player = GameManager._instance.Player;

        BuffManager._instance.StartBuff(BuffManager.BuffEffect.AtkUp, player, _upAtkValue, _buffDuringTime);
        BuffManager._instance.StartBuff(BuffManager.BuffEffect.DefUp, player, _upDefValue, _buffDuringTime);

        _colls = Physics.OverlapSphere(transform.position, _dmgAmount, _layerMask);

        foreach(Collider coll in _colls)
        {
            Stat stat = coll.GetComponent<Stat>();

            if (stat != null)
                stat.SetDamage(_atk);

            BuffManager._instance.StartDeBuff(BuffManager.BuffEffect.AtkDown, coll.gameObject, _downAtkValue, _buffDuringTime);
            BuffManager._instance.StartDeBuff(BuffManager.BuffEffect.DefDown, coll.gameObject, _downDefValue, _buffDuringTime);
        }
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
}
