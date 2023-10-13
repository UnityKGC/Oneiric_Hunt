using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceCut : MonoBehaviour
{
    SkillScriptable _scriptable;

    Collider[] _monsterColls = new Collider[10]; // ������̴� ���� ��
    Collider[] _monsterDamageColls = new Collider[10]; // �������� ������ ���� ��

    float _startTime; // ���۽ð�
    float _duringTime = 10f; // ���ӽð�

    float _attractAmount; // ������̴� ����
    float _dmgAmount; // �������� ������ ����

    float _dmgStart; // ������ �ð����� start
    float _dmgDelay; // ������ �ִ� ������ �ð�

    float _atk; // ���� ��ų ���ݷ�

    int _layerMask = (1 << 7) | (1 << 10); // ���Ϳ� ������ Layer�� üũ

    List<GameObject> _damagedTargets = new List<GameObject>(); // ��ų�� �ѹ� �´��� ���� �ٷ��.
    public void Init(SkillScriptable scriptable, float playerAtk)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _atk = playerAtk * _scriptable._damageValue;
        _attractAmount = _scriptable._skillAmount;
        _dmgAmount = _scriptable._damageAmount;

        _dmgDelay = _scriptable._damageDelay;

        _duringTime = _scriptable._durationTime;

        _startTime = Time.time; // ��ų ������ڸ��� ��ȯ�ǹǷ�, �ٷ� �ð�üũ
        _dmgStart = Time.time;
    }
    void Start()
    {
        //_startTime = Time.time; // ��ų ������ڸ��� ��ȯ�ǹǷ�, �ٷ� �ð�üũ
        //_dmgStart = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate() // 0.5�ʸ��� ��ų �������� ��� ������ �������� �ش�.
    {
        if (Time.time - _startTime <= _duringTime)
        {
            Physics.OverlapSphereNonAlloc(transform.position, _attractAmount, _monsterColls, _layerMask);
            Physics.OverlapSphereNonAlloc(transform.position, _dmgAmount, _monsterDamageColls, _layerMask);

            foreach (Collider coll in _monsterColls)  // ������� ����
            {
                Vector3 dir = transform.position - coll.transform.position;
                coll.transform.position += dir * Time.deltaTime;
            }

            foreach(Collider coll in _monsterDamageColls)  // ������ ������ ����
            {
                if (!_damagedTargets.Contains(coll.gameObject))
                {
                    Stat stat = coll.GetComponent<Stat>();

                    if (stat != null)
                        stat.SetDamage(_atk);

                    _damagedTargets.Add(coll.gameObject);
                }
            }

            if (Time.time - _dmgStart >= _dmgDelay) // ������ ������ �ð��� ������ ����, ���� �� ����Ʈ �ʱ�ȭ
            {
                _dmgStart = Time.time;
                _damagedTargets.Clear();
            }
        }
        else
            Destroy(gameObject);
    }
}
