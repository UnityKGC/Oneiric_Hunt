using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : MonoBehaviour
{
    Vector3 _playerPos; // �÷��̾� ��ġ
    Vector3 _startBossPos; // ��ų ���� ��, ������ ��ġ

    Vector3 _dir; // ������ ���� ����

    float _startTime; // ���۽ð�
    float _remainingTime; // �����ð�
    float _duringTime = 3f; // ���ӽð�

    float _bossDmg; // ������ ���ݷ�

    float _dmgValue = 5f; // ��ų ������ ����

    float _atk; // ���� ��ų ������
    public void SetBossDmg(float dmg) // �ܺο��� ȣ���� ��� ��
    {
        _bossDmg = dmg;
    }

    void Start()
    {
        _playerPos = GameManager._instance.Player.transform.position;
        _startBossPos = transform.parent.position;

        _startTime = Time.time;
        _atk = _bossDmg * _dmgValue;

        _dir = (_playerPos - _startBossPos).normalized;
        transform.parent.LookAt(_playerPos);
    }

    void Update()
    {
        _remainingTime = _duringTime - (Time.time - _startTime);
        if (_remainingTime >= 0f)
        {
            StartRush();
        }
        else
        {
            BossSkillManager._instance._isSkilling = false;
            BossSkillManager._instance.EndSkill();
            Destroy(gameObject);
        }
    }
    private void StartRush()
    {
        transform.parent.position += _dir * 15 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStat playerStat = other.GetComponent<PlayerStat>();
            if (playerStat != null)
            {
                playerStat.HP -= _atk;
            }
        }
    }
}
