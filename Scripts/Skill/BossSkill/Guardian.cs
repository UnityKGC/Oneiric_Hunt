using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour
{
    private float _skillRange = 10f;

    private MonsterStat _monsterStat;

    float _startTime; // ���۽ð�
    float _remainingTime; // �����ð�

    float _duringTime = 3f; // ���ӽð�

    float _upHpValue = 0.2f; // ȸ���ϴ� ü�� ����
    float _upDefValue = 0.2f; // �ö�� �� ���� ����
    float _buffDuringTime = 10f; // ���� ���� �ð�

    bool _isApplySkill = false;

    void Start()
    {
        _startTime = Time.time;
    }

    void Update()
    {
        _remainingTime = _duringTime - (Time.time - _startTime);
        if (_remainingTime >= 0f)
        {
            return;
        }
        else
        {
            BossSkillManager._instance._isSkilling = false;
            BossSkillManager._instance.EndSkill();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) // ��ų ����
    {
        if (_isApplySkill) return; // ��ų�� ����������� ����

        if (other.CompareTag("Monster")) // ��ų ���� ���� ���Ͱ� �����Ѵٸ�,
        {
            _monsterStat = other.GetComponent<MonsterStat>();
            if (_monsterStat != null)
            {
                float hp = _monsterStat.HP * _upHpValue; // ü�� ȸ��
                _monsterStat.HP += hp;

                BuffManager._instance.StartDefBuff(other.gameObject, _upDefValue, _buffDuringTime);
            }
        }
    }
}
