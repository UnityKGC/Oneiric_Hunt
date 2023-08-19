using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    float _upSwordMinAtk; // 상승된 검 최소공격력
    float _upSwordMaxAtk; // 상승된 검 최소공격력

    float _upSpearMinAtk;
    float _upSpearMaxAtk;

    float _upAxeMinAtk;
    float _upAxeMaxAtk;

    float _upDefense; // 상승된 방어력

    float _upMonsterMinAtk;
    float _upMonsterMaxAtk;

    float _upBossMinAtk;
    float _upBossMaxAtk;

    float _upMonsterDefense;

    float _upBossDefense;

    float _upPlayerMoveSpd;
    float _upMonsterMoveSpd;
    float _upBossMoveSpd;

    float _value; // 적용해야할 비율

    private PlayerStat _playerStat;
    private MonsterStat _monsterStat;
    private BossStat _bossStat;

    private float _duration;
    void Start()
    {

    }
    public void StartAtkBuff(GameObject target, float value, float time)
    {
        FindStat(target);
        gameObject.name = "AtkBuff";
        _duration = time;
        if (_playerStat != null)
        {
            UIManager._instacne.StartPlayerBuffUI(BuffManager.BuffType.AtkUp, time);
            StartCoroutine(StartPlayerAtkBuffCo(value));
        }
        else if (_monsterStat != null)
        {
            StartCoroutine(StartMonsterAtkBuffCo(value));
        }
        else
        {
            StartCoroutine(StartBossAtkBuffCo(value));
        }
    }
    IEnumerator StartPlayerAtkBuffCo(float value)
    {
        UpPlayerAtk(value);

        yield return new WaitForSeconds(_duration);

        ReturnPlayerAtk();

        Destroy(gameObject);
    }
    IEnumerator StartMonsterAtkBuffCo(float value)
    {
        UpMonsterAtk(value);

        yield return new WaitForSeconds(_duration);

        ReturnMonsterAtk();

        Destroy(gameObject);
    }
    IEnumerator StartBossAtkBuffCo(float value)
    {
        UpBossAtk(value);

        yield return new WaitForSeconds(_duration);

        ReturnBossAtk();

        Destroy(gameObject);
    }
    public void StartDefBuff(GameObject target, float value, float time)
    {
        FindStat(target);
        gameObject.name = "DefBuff";
        _duration = time;
        if (_playerStat != null)
        {
            UIManager._instacne.StartPlayerBuffUI(BuffManager.BuffType.DefUp, time);
            StartCoroutine(StartPlayerDefBuffCo(value));
        }
        else if (_monsterStat != null)
        {
            StartCoroutine(StartMonsterDefBuffCo(value));
        }
        else
        {
            StartCoroutine(StartBossDefBuffCo(value));
        }
    }
    IEnumerator StartPlayerDefBuffCo(float value)
    {
        UpPlayerDef(value);

        yield return new WaitForSeconds(_duration);

        ReturnPlayerDef();

        Destroy(gameObject);
    }
    IEnumerator StartMonsterDefBuffCo(float value)
    {
        UpMonsterDef(value);

        yield return new WaitForSeconds(_duration);

        ReturnMonsterDef();

        Destroy(gameObject);
    }
    IEnumerator StartBossDefBuffCo(float value)
    {
        UpBossDef(value);

        yield return new WaitForSeconds(_duration);

        ReturnBossDef();

        Destroy(gameObject);
    }

    public void StartMovSpdBuff(GameObject target, float value, float time)
    {
        FindStat(target);
        gameObject.name = "MovSpdBuff";
        _duration = time;
        if (_playerStat != null)
        {
            UIManager._instacne.StartPlayerBuffUI(BuffManager.BuffType.MovSpdUp, time);
            StartCoroutine(StartPlayerMovSpdBuffCo(value));
        }
        else if (_monsterStat != null)
        {
            StartCoroutine(StartMonsterMovSpdBuffCo(value));
        }
        else
        {
            StartCoroutine(StartBossMovSpdBuffCo(value));
        }
    }

    IEnumerator StartPlayerMovSpdBuffCo(float value)
    {
        UpPlayerMovSpd(value);

        yield return new WaitForSeconds(_duration);

        ReturnPlayerMovSpd();

        Destroy(gameObject);
    }
    IEnumerator StartMonsterMovSpdBuffCo(float value)
    {
        UpMonsterMovSpd(value);

        yield return new WaitForSeconds(_duration);

        ReturnMonsterMovSpd();

        Destroy(gameObject);
    }
    IEnumerator StartBossMovSpdBuffCo(float value)
    {
        UpBossMovSpd(value);

        yield return new WaitForSeconds(_duration);

        ReturnBossMovSpd();

        Destroy(gameObject);
    }

    void UpPlayerAtk(float value)
    {
        _upSwordMinAtk = _playerStat.SwordMinAtk * value;
        _upSwordMaxAtk = _playerStat.SwordMaxAtk * value;

        _upSpearMinAtk = _playerStat.SpearMinAtk * value;
        _upSpearMaxAtk = _playerStat.SpearMaxAtk * value;

        _upAxeMinAtk = _playerStat.AxeMinAtk * value;
        _upAxeMaxAtk = _playerStat.AxeMaxAtk * value;

        _playerStat.SwordMinAtk += _upSwordMinAtk;
        _playerStat.SwordMaxAtk += _upSwordMaxAtk;

        _playerStat.SpearMinAtk += _upSpearMinAtk;
        _playerStat.SpearMaxAtk += _upSpearMaxAtk;

        _playerStat.AxeMinAtk += _upAxeMinAtk;
        _playerStat.AxeMaxAtk += _upAxeMaxAtk;
    }
    void UpMonsterAtk(float value)
    {
        _upMonsterMinAtk = _monsterStat.MinAtk * value;
        _upMonsterMaxAtk = _monsterStat.MaxAtk * value;

        _monsterStat.MinAtk += _upMonsterMinAtk;
        _monsterStat.MaxAtk += _upMonsterMaxAtk;
    }
    void UpBossAtk(float value)
    {
        _upBossMinAtk = _bossStat.MinAtk * value;
        _upBossMaxAtk = _bossStat.MaxAtk * value;

        _bossStat.MinAtk += _upBossMinAtk;
        _bossStat.MaxAtk += _upBossMaxAtk;
    }
    void ReturnPlayerAtk()
    {
        _playerStat.SwordMinAtk -= _upSwordMinAtk;
        _playerStat.SwordMaxAtk -= _upSwordMaxAtk;

        _playerStat.SpearMinAtk -= _upSpearMinAtk;
        _playerStat.SpearMaxAtk -= _upSpearMaxAtk;

        _playerStat.AxeMinAtk -= _upAxeMinAtk;
        _playerStat.AxeMaxAtk -= _upAxeMaxAtk;
    }
    void ReturnMonsterAtk()
    {
        _monsterStat.MinAtk -= _upMonsterMinAtk;
        _monsterStat.MaxAtk -= _upMonsterMaxAtk;
    }
    void ReturnBossAtk()
    {
        _bossStat.MinAtk -= _upBossMinAtk;
        _bossStat.MaxAtk -= _upBossMaxAtk;
    }

    void UpPlayerDef(float value)
    {
        _upDefense = _playerStat.Defense * value;
        _playerStat.Defense += _upDefense;
    }
    void UpMonsterDef(float value)
    {
        _upMonsterDefense = _monsterStat.Defense * value;
        _monsterStat.Defense += _upMonsterDefense;
    }
    void UpBossDef(float value)
    {
        _upBossDefense = _bossStat.Defense * value;
        _bossStat.Defense += _upBossDefense;
    }
    void ReturnPlayerDef()
    {
        _playerStat.Defense -= _upDefense;
    }
    void ReturnMonsterDef()
    {
        _monsterStat.Defense -= _upMonsterDefense;
    }
    void ReturnBossDef()
    {
        _bossStat.Defense -= _upBossDefense;
    }

    void UpPlayerMovSpd(float value)
    {
        _upPlayerMoveSpd = _playerStat.MoveSpd * value;
        _playerStat.MoveSpd += _upPlayerMoveSpd;
    }
    void UpMonsterMovSpd(float value)
    {
        _upMonsterMoveSpd = _monsterStat.MoveSpd * value;
        _monsterStat.MoveSpd += _upMonsterMoveSpd;
    }
    void UpBossMovSpd(float value)
    {
        _upBossMoveSpd = _monsterStat.MoveSpd * value;
        _monsterStat.MoveSpd += _upBossMoveSpd;
    }
    void ReturnPlayerMovSpd()
    {
        _playerStat.MoveSpd -= _upPlayerMoveSpd;
    }
    void ReturnMonsterMovSpd()
    {
        _monsterStat.MoveSpd -= _upMonsterMoveSpd;
    }
    void ReturnBossMovSpd()
    {
        _monsterStat.MoveSpd -= _upBossMoveSpd;
    }

    void FindStat(GameObject target) // 내가 누구에게 와 있는가 찾는 것.
    {
        _playerStat = target.GetComponent<PlayerStat>();
        _monsterStat = target.GetComponent<MonsterStat>();
        _bossStat = target.GetComponent<BossStat>();
    }
}
