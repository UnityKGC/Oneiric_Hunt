using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    SkillScriptable _scriptable;
    Collider[] _colls = new Collider[10];

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

        Physics.OverlapCapsuleNonAlloc(_point0, _point1, _dmgAmount, _colls, _layerMask);

        foreach (Collider coll in _colls)
        {
            Stat stat = coll.GetComponent<Stat>();

            if (stat != null)
                stat.SetDamage(_atk);
        }
        Destroy(gameObject, _durationTime);
    }
    private void OnDestroy()
    {
        SkillManager._instance.EndSkill();
    }
}
