using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takedown : MonoBehaviour
{
    SkillScriptable _scriptable;
    Collider[] _colls;

    float _dmgAmount; // ��ų ����
    float _durationTime;
    float _atk; // ���� ��ų ���ݷ�

    int _layerMask = (1 << 7) | (1 << 10); // ���Ϳ� ������ Layer�� üũ

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
        _colls = Physics.OverlapSphere(transform.position, _dmgAmount, _layerMask);

        foreach (Collider coll in _colls)
        {
            Stat stat = coll.GetComponent<Stat>();

            if (stat != null)
                stat.SetDamage(_atk);
        }
        Destroy(gameObject, _durationTime);
    }
}
