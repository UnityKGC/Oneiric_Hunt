using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    SkillScriptable _scriptable;
    Collider[] _colls;

    float _dmgAmount; // ��ų ����
    float _durationTime;
    float _atk; // ���� ��ų ���ݷ�

    int _layerMask = (1 << 7) | (1 << 10); // ���Ϳ� ������ Layer�� üũ

    Vector3 _point0, _point1;

    public void Init(SkillScriptable scriptable, float playerAtk)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _atk = playerAtk * _scriptable._damageValue;

        _dmgAmount = _scriptable._damageAmount;
        _durationTime = _scriptable._durationTime;
    }

    void Start()
    {
        _point0 = transform.position - Vector3.left;
        _point1 = transform.position - Vector3.right;

        _colls = Physics.OverlapCapsule(_point0, _point1, _dmgAmount, _layerMask);

        foreach (Collider coll in _colls)
        {
            MonsterStat monsterStat = coll.GetComponent<MonsterStat>();
            BossStat bossStat = coll.GetComponent<BossStat>();

            if (monsterStat != null)
                monsterStat.SetDamage(_atk);
            else
                bossStat.SetDamage(_atk);
        }
        Destroy(gameObject, _durationTime);
    }

    void Update()
    {
        
    }
}
