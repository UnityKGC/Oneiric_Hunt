using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceCut : MonoBehaviour
{
    SkillScriptable _scriptable;

    Collider[] _monsterColls; // 끌어들이는 몬스터 들
    Collider[] _monsterDamageColls; // 데미지를 입히는 몬스터 들

    float _startTime; // 시작시간
    float _duringTime = 10f; // 지속시간

    float _attractAmount; // 끌어들이는 범위
    float _dmgAmount; // 데미지를 입히는 범위

    float _dmgStart; // 데미지 시간측정 start
    float _dmgDelay; // 데미지 주는 딜레이 시간

    float _atk; // 최종 스킬 공격력

    int _layerMask = (1 << 7) | (1 << 10); // 몬스터와 보스의 Layer만 체크

    HashSet<GameObject> _damagedTargets = new HashSet<GameObject>(); // 스킬과 한번 맞닿은 적을 다룬다.
    public void Init(SkillScriptable scriptable, float playerAtk)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _atk = playerAtk * _scriptable._damageValue;
        _attractAmount = _scriptable._skillAmount;
        _dmgAmount = _scriptable._damageAmount;

        _dmgDelay = _scriptable._damageDelay;

        _duringTime = _scriptable._durationTime;
    }
    void Start()
    {
        _startTime = Time.time; // 스킬 사용하자마자 소환되므로, 바로 시간체크
        _dmgStart = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate() // 0.5초마다 스킬 범위내의 모든 적에게 데미지를 준다.
    {
        if (Time.time - _startTime <= _duringTime)
        {
            _monsterColls = Physics.OverlapSphere(transform.position, _attractAmount, _layerMask); // Layer가 Monster인 오브젝트들의 Collider를 _monsterColls에 담는다.
            _monsterDamageColls = Physics.OverlapSphere(transform.position, _dmgAmount, _layerMask);

            foreach (Collider coll in _monsterColls)
            {
                Vector3 dir = transform.position - coll.transform.position;
                coll.transform.position += dir * 1f * Time.deltaTime;
            }

            foreach(Collider coll in _monsterDamageColls)
            {
                if (!_damagedTargets.Contains(coll.gameObject))
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

            if (Time.time - _dmgStart >= _dmgDelay) // 데미지 딜레이 시간이 지날때 마다, 맞은 적 리스트 초기화
            {
                _dmgStart = Time.time;
                _damagedTargets.Clear();
            }
        }
        else
            Destroy(gameObject);
    }
}
