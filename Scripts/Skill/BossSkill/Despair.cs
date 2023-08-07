using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despair : MonoBehaviour
{

    private float _skillRange = 10f;

    private int _mask = 1 << 3; // Overlap�Ҷ� ����� LayerMask => �÷��̾ �����Ѵ�.

    private PlayerStat _playerStat;

    float _startTime; // ���۽ð�
    float _remainingTime; // �����ð�

    float _duringTime = 2f; // ���ӽð�

    float _downValue = 0.3f; // �������� �� ���� ����
    float _deBuffDuringTime = 10f; // ����� ���� �ð�

    float _downDefense; // ���ҵ� ����

    float _downSwordMinAtk; // ���ҵ� �� �ּҰ��ݷ�
    float _downSwordMaxAtk; // ���ҵ� �� �ּҰ��ݷ�

    float _downSpearMinAtk;
    float _downSpearMaxAtk;

    float _downAxeMinAtk;
    float _downAxeMaxAtk;

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
                BuffManager._instance.StartAtkDeBuff(GameManager._instance.Player, _downValue, _deBuffDuringTime);
                BuffManager._instance.StartDefDeBuff(GameManager._instance.Player, _downValue, _deBuffDuringTime);
            }
        }
    }
}
