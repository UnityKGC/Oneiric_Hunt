using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overdose : MonoBehaviour
{
    float _startTime; // ���۽ð�
    float _remainingTime; // �����ð�

    float _duringTime = 10f; // ���ӽð�

    float _downHpValue = 0.05f; // �ʴ� ���̴� ü�� ����(���� ü��)
    float _downHp; // ������ ���̴� ü��
    float _upValue = 0.5f; // �ö�� �� ���ݷ� ���� ����
    float _buffDuringTime = 10f; // ���� ���� �ð�

    BossStat _bossStat;
    void Start()
    {
        _startTime = Time.time;
        _bossStat = GetComponentInParent<BossStat>();
        _downHp = _bossStat.HP * _downHpValue;
        StartCoroutine(StartDamage());
        
        BuffManager._instance.StartBuff(BuffManager.BuffEffect.AtkUp, transform.parent.gameObject, _upValue, _buffDuringTime);

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
    IEnumerator StartDamage()
    {
        while(true)
        {
            _bossStat.SetDamage(_downHp);
            //_bossStat.HP -= _downHp;

            yield return new WaitForSeconds(1f);
        }
    }
}
