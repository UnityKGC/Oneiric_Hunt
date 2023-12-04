using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceCut : MonoBehaviour // Skill��ũ��Ʈ�� ��ӹް� �����丵 �� ����
{
    SkillScriptable _scriptable;

    [SerializeField] Collider[] _monsterColls = new Collider[10]; // ������̴� ���� ��
    [SerializeField] Collider[] _monsterDamageColls = new Collider[10]; // �������� ������ ���� ��

    float _startTime; // ���۽ð�
    float _duringTime = 10f; // ���ӽð�

    float _attractAmount; // ������̴� ����
    float _dmgAmount; // �������� ������ ����

    float _dmgStart; // ������ �ð����� start
    float _dmgDelay; // ������ �ִ� ������ �ð�

    float _atk; // ���� ��ų ���ݷ�

    int _layerMask = (1 << 7) | (1 << 10); // ���Ϳ� ������ Layer�� üũ

    HashSet<GameObject> _damagedTargets = new HashSet<GameObject>(); // ��ų�� �ѹ� �´��� ���� �ٷ��.
    public void Init(SkillScriptable scriptable, float playerAtk) // �ʱ�ȭ
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _atk = playerAtk * _scriptable._damageValue;
        _attractAmount = _scriptable._skillAmount;
        _dmgAmount = _scriptable._damageAmount;

        _dmgDelay = _scriptable._damageDelay;

        _duringTime = _scriptable._durationTime;

        _startTime = Time.time; // �ð�üũ
        _dmgStart = Time.time;
    }

    void FixedUpdate() // 0.5�ʸ��� ��ų �������� ��� ������ �������� �ش�.
    {
        if (Time.time - _startTime <= _duringTime)
        {
            Physics.OverlapSphereNonAlloc(transform.position, _attractAmount, _monsterColls, _layerMask);
            Physics.OverlapSphereNonAlloc(transform.position, _dmgAmount, _monsterDamageColls, _layerMask);

            foreach (Collider coll in _monsterColls)  // ������� ����
            {
                if(coll)
                {
                    Vector3 dir = transform.position - coll.transform.position;
                    coll.transform.position += dir * Time.deltaTime;
                }
            }

            foreach(Collider coll in _monsterDamageColls)  // ������ ������ ����
            {
                if(coll)
                {
                    if (!_damagedTargets.Contains(coll.gameObject)) // _dmgDelay���� �������� �޵��� �ϴ� ����.
                    {
                        Stat stat = coll.GetComponent<Stat>();

                        if (stat != null)
                            stat.SetDamage(_atk);

                        _damagedTargets.Add(coll.gameObject);
                    }
                }
            }
            if (Time.time - _dmgStart >= _dmgDelay) // ������ ������ �ð��� ������ ����, ���� �� ����Ʈ �ʱ�ȭ => ���߿� �׾��ų�, ���� ������ �������� ���͵� ������ �� ������ ���ش�.
            {
                _dmgStart = Time.time;
                _damagedTargets.Clear();
            }
        }
        else
            Destroy(gameObject);
    }
}
