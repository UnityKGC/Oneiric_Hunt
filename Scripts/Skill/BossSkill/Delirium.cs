using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Monster;

public class Delirium : MonoBehaviour
{
    float _startTime; // ���۽ð�
    float _remainingTime; // �����ð�

    float _duringTime = 5f; // ���ӽð�

    float _statusDuringTime = 10f; // �����̻� ���ӽð�
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
        if (other.CompareTag("Player")) // ��ų ���� ���� ���Ͱ� �����Ѵٸ�,
        {
            StatusManager._instance.StartConfusion(other.gameObject, _statusDuringTime);
        }
    }
}
