using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep : MonoBehaviour
{
    SkillScriptable _scriptable;
    Collider[] colls;

    float _dmgAmount; // ��ų ����

    float _atk; // ���� ��ų ���ݷ�

    int _layerMask = (1 << 7) | (1 << 10); // ���Ϳ� ������ Layer�� üũ

    public void Init(SkillScriptable scriptable, float playerAtk)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _atk = playerAtk * _scriptable._damageValue;
        _dmgAmount = _scriptable._damageAmount;
    }

    void Start()
    {
        colls = Physics.OverlapSphere(transform.position, _dmgAmount, _layerMask);
        foreach(Collider coll in colls)
        {
            MonsterStat monsterStat = coll.GetComponent<MonsterStat>();
            BossStat bossStat = coll.GetComponent<BossStat>();

            if (monsterStat != null)
                monsterStat.SetDamage(_atk);
            else
                bossStat.SetDamage(_atk);
        }
        Destroy(gameObject, 1f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _dmgAmount);
    }
}
