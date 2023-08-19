using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stench : MonoBehaviour
{
    float _startTime; // ���۽ð�
    float _remainingTime; // �����ð�

    float _duringTime = 10f; // ���ӽð�

    float _deBuffValue = 0.3f; // ����� ���� ��

    float _deBuffDuringTime = 10f; // ����� ���ӽð�

    float _statusDmgValue = 0.01f; // �����̻�(��) ���� �� => �ʴ� 1%�� ����

    float _statusDuringTime = 10f; // �����̻� ���ӽð�
    void Start()
    {
        _startTime = Time.time;
        BossSkillManager._instance._isSkilling = false;
        BossSkillManager._instance.EndSkill();
    }

    void Update()
    {
        _remainingTime = _duringTime - (Time.time - _startTime);
        if (_remainingTime >= 0f)
        {

        }
        else
        {
            
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StatusManager._instance.StartPoison(other.gameObject, _statusDmgValue, _statusDuringTime);

            BuffManager._instance.StartDeBuff(BuffManager.BuffEffect.MovSpdDown, other.gameObject, _deBuffValue, _deBuffDuringTime);
        }
    }
}
