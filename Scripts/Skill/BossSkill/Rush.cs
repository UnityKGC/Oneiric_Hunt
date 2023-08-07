using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : MonoBehaviour
{
    Vector3 _playerPos; // 플레이어 위치
    Vector3 _startBossPos; // 스킬 시작 시, 보스의 위치

    Vector3 _dir; // 돌진할 방향 벡터

    float _startTime; // 시작시간
    float _remainingTime; // 남은시간
    float _duringTime = 3f; // 지속시간

    float _bossDmg; // 보스의 공격력

    float _dmgValue = 5f; // 스킬 데미지 배율

    float _atk; // 최종 스킬 데미지
    public void SetBossDmg(float dmg) // 외부에서 호출해 줘야 함
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
