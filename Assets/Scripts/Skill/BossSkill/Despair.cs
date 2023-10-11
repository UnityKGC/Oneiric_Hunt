using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despair : MonoBehaviour
{
    private PlayerStat _playerStat;

    float _startTime; // ���۽ð�
    float _remainingTime; // �����ð�

    float _duringTime = 2f; // ���ӽð�

    float _downValue = 0.3f; // �������� �� ���� ����
    float _deBuffDuringTime = 10f; // ����� ���� �ð�

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

        if(other.CompareTag("Player")) // ��ų ���� ���� �÷��̾ �����Ѵٸ�,
        {
            _playerStat = other.GetComponent<PlayerStat>();
            if (_playerStat != null)
            {
                // �����Ŵ������� ������ ����Ѵٰ� �˸�
                // BuffManager._instance.StartDeBuff(�������� ���, ���� ����, ���� ������, );
                BuffManager._instance.StartDeBuff(BuffManager.BuffEffect.AtkDown, GameManager._instance.Player, _downValue, _deBuffDuringTime);
                BuffManager._instance.StartDeBuff(BuffManager.BuffEffect.DefDown, GameManager._instance.Player, _downValue, _deBuffDuringTime);
            }
        }
    }
}
