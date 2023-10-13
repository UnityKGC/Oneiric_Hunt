using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : MonoBehaviour
{
    SkillScriptable _scriptable;

    Collider[] _monsterColls = new Collider[10]; // 끌어들이는 몬스터 들
    Collider[] _monsterDamageColls = new Collider[10]; // 데미지를 입히는 몬스터 들

    HashSet<GameObject> _damagedTargets = new HashSet<GameObject>(); // 스킬과 한번 맞닿은 적을 다룬다.

    float _startTime; // 시작시간
    float _duringTime; // 지속시간

    float _attractAmount; // 끌어들이는 범위
    float _dmgAmount; // 데미지를 입히는 범위

    float _dmgStart; // 데미지 시간측정 start
    float _dmgDelay; // 데미지 주는 딜레이 시간

    float _atk; // 최종 스킬 공격력

    int _layerMask = (1 << 7) | (1 << 10); // 몬스터와 보스의 Layer만 체크
    public void Init(SkillScriptable scriptable, float playerAtk, Transform parent = null)
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

        CameraManager._instance.StartEffectCam(CameraType.PlayerCam, 2f, _duringTime);
    }
    void FixedUpdate()
    {
        if (Time.time - _startTime <= _duringTime)
        {
            Physics.OverlapSphereNonAlloc(transform.position, _attractAmount, _monsterColls, _layerMask); // Layer가 Monster인 오브젝트들의 Collider를 _monsterColls에 담는다.
            Physics.OverlapSphereNonAlloc(transform.position, _dmgAmount, _monsterDamageColls, _layerMask);

            foreach (Collider coll in _monsterColls) // 빨려드는 범위
            {
                if (coll)
                {
                    Vector3 dir = transform.position - coll.transform.position;
                    coll.transform.position += dir * Time.deltaTime;
                }
            }

            foreach (Collider coll in _monsterDamageColls) // 데미지 입히는 범위
            {
                if (coll)
                {
                    if (!_damagedTargets.Contains(coll.gameObject)) // _dmgDelay마다 데미지를 받도록 하는 로직.
                    {
                        Stat stat = coll.GetComponent<Stat>();

                        if (stat != null)
                            stat.SetDamage(_atk);

                        _damagedTargets.Add(coll.gameObject);
                    }
                }
            }

            if (Time.time - _dmgStart >= _dmgDelay) // 데미지 딜레이 시간이 지날때 마다, 맞은 적 리스트 초기화
            {
                _dmgStart = Time.time;
                _damagedTargets.Clear();
            }
        }
        else
        {
            SkillManager._instance.EndSkill();
            Destroy(gameObject);
        }
            
    }
}
