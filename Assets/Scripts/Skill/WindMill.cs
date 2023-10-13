using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : MonoBehaviour
{
    SkillScriptable _scriptable;

    Collider[] _monsterColls = new Collider[10]; // ������̴� ���� ��
    Collider[] _monsterDamageColls = new Collider[10]; // �������� ������ ���� ��

    HashSet<GameObject> _damagedTargets = new HashSet<GameObject>(); // ��ų�� �ѹ� �´��� ���� �ٷ��.

    float _startTime; // ���۽ð�
    float _duringTime; // ���ӽð�

    float _attractAmount; // ������̴� ����
    float _dmgAmount; // �������� ������ ����

    float _dmgStart; // ������ �ð����� start
    float _dmgDelay; // ������ �ִ� ������ �ð�

    float _atk; // ���� ��ų ���ݷ�

    int _layerMask = (1 << 7) | (1 << 10); // ���Ϳ� ������ Layer�� üũ
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
        _startTime = Time.time; // ��ų ������ڸ��� ��ȯ�ǹǷ�, �ٷ� �ð�üũ
        _dmgStart = Time.time;

        CameraManager._instance.StartEffectCam(CameraType.PlayerCam, 2f, _duringTime);
    }
    void FixedUpdate()
    {
        if (Time.time - _startTime <= _duringTime)
        {
            Physics.OverlapSphereNonAlloc(transform.position, _attractAmount, _monsterColls, _layerMask); // Layer�� Monster�� ������Ʈ���� Collider�� _monsterColls�� ��´�.
            Physics.OverlapSphereNonAlloc(transform.position, _dmgAmount, _monsterDamageColls, _layerMask);

            foreach (Collider coll in _monsterColls) // ������� ����
            {
                if (coll)
                {
                    Vector3 dir = transform.position - coll.transform.position;
                    coll.transform.position += dir * Time.deltaTime;
                }
            }

            foreach (Collider coll in _monsterDamageColls) // ������ ������ ����
            {
                if (coll)
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

            if (Time.time - _dmgStart >= _dmgDelay) // ������ ������ �ð��� ������ ����, ���� �� ����Ʈ �ʱ�ȭ
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
