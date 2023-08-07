using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordForce : MonoBehaviour
{
    SkillScriptable _scriptable;

    float _startTime; // 시작시간
    float _duringTime; // 지속시간

    float _amount; // 스킬 범위

    float _moveSpd;

    float _atk; // 최종 스킬 공격력

    int _layerMask = (1 << 7) | (1 << 10); // 몬스터와 보스의 Layer만 체크

    Collider[] _monsters;
    HashSet<GameObject> _damagedTargets = new HashSet<GameObject>(); // 스킬과 한번 맞닿은 적을 다룬다.

    void Start()
    {
        _startTime = Time.time;
    }
    public void Init(SkillScriptable scriptable, float playerAtk)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;
        _atk = playerAtk * _scriptable._damageValue;
        _amount = _scriptable._skillAmount;
        _duringTime = _scriptable._durationTime;
        _moveSpd = _scriptable._moveSpd;
    }
    void FixedUpdate()
    {
        if (Time.time - _startTime <= _duringTime)
        {
            transform.position += transform.forward * _moveSpd * Time.deltaTime;
            _monsters = Physics.OverlapSphere(transform.position, _amount, _layerMask);
            foreach(Collider coll in _monsters)
            {
                if (!_damagedTargets.Contains(coll.gameObject)) // 한번 맞은 적은 건너뛴다.
                {
                    MonsterStat monsterStat = coll.GetComponent<MonsterStat>();
                    BossStat bossStat = coll.GetComponent<BossStat>();

                    if (monsterStat != null)
                        monsterStat.SetDamage(_atk);
                    else
                        bossStat.SetDamage(_atk);

                    _damagedTargets.Add(coll.gameObject);
                }
            }
        }
        else
            Destroy(gameObject);
    }
}
