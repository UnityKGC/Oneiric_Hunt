using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    PlayerStat _playerStat;
    MonsterStat _monsterStat;
    BossStat _bossStat;

    float _startTime; // ���۽ð�
    public float _remainingTime; // �����ð�
    float _duration; // ���ӽð�

    float _dmgValue; // �ʴ� ������ ������ ����
    float _dmg; // ���� �ʴ� ������ ������

    private void Update()
    {
        _remainingTime = _duration - (Time.time - _startTime);
        if (_remainingTime >= 0f)
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetValues(GameObject target, float dmg, float time)
    {
        Poison posion = transform.parent.GetComponentInChildren<Poison>();
        if (posion != this && posion != null) // �� �����̻��� �̹� �����Ѵٸ�
            Destroy(posion.gameObject); // ������ ���� �ı���Ų��. => ������ ����

        FindStat(target);

        _dmgValue = dmg;
        _duration = time;
        _startTime = Time.time;

        if (_playerStat != null)
        {
            _dmg = _playerStat.MaxHp * _dmgValue;
        }
        else if (_monsterStat != null)
        {
            _dmg = _monsterStat.MaxHp * _dmgValue;
        }
        else
        {
            _dmg = _bossStat.MaxHp * _dmgValue;
        }

        StartCoroutine(StartPosion());
    }
    void FindStat(GameObject target)
    {
        _playerStat = target.GetComponent<PlayerStat>();
        _monsterStat = target.GetComponent<MonsterStat>();
        _bossStat = target.GetComponent<BossStat>();
    }
    IEnumerator StartPosion()
    {
        while(_duration - _remainingTime > 0)
        {
            if (_playerStat != null)
            {
                _playerStat.HP -= _dmg;
            }
            else if (_monsterStat != null)
            {
                _monsterStat.HP -= _dmg;
            }
            else
            {
                _bossStat.HP -= _dmg;
            }
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }
}
