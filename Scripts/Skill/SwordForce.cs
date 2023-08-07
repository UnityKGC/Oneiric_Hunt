using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordForce : MonoBehaviour
{
    SkillScriptable _scriptable;

    float _startTime; // ���۽ð�
    float _duringTime; // ���ӽð�

    float _amount; // ��ų ����

    float _moveSpd;

    float _atk; // ���� ��ų ���ݷ�

    int _layerMask = (1 << 7) | (1 << 10); // ���Ϳ� ������ Layer�� üũ

    Collider[] _monsters;
    HashSet<GameObject> _damagedTargets = new HashSet<GameObject>(); // ��ų�� �ѹ� �´��� ���� �ٷ��.

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
                if (!_damagedTargets.Contains(coll.gameObject)) // �ѹ� ���� ���� �ǳʶڴ�.
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
