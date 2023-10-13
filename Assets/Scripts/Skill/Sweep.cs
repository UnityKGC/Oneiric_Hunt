using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep : MonoBehaviour
{
    SkillScriptable _scriptable;
    Collider[] _colls = new Collider[10];

    float _dmgAmount; // ��ų ����

    float _atk; // ���� ��ų ���ݷ�

    float _duration;

    int _layerMask = (1 << 7) | (1 << 10); // ���Ϳ� ������ Layer�� üũ

    public void Init(SkillScriptable scriptable, float playerAtk)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _atk = playerAtk * _scriptable._damageValue;
        _dmgAmount = _scriptable._damageAmount;
        _duration = _scriptable._durationTime;
    }

    void Start()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _dmgAmount, _colls, _layerMask);

        foreach (Collider coll in _colls)
        {
            if (coll)
            {
                Stat stat = coll.GetComponent<Stat>();

                if (stat != null)
                    stat.SetDamage(_atk);
            }
        }
        Destroy(gameObject, _duration);
    }
    private void OnDestroy()
    {
        SkillManager._instance.EndSkill();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _dmgAmount);
    }
}
