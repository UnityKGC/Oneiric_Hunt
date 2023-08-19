using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    float _upMinValue; // 따로 만든 이유 => Min Max가 증가한 수치를 저장하고 시간이 지난 후 그 수치만큼 감소해야함.
    float _upMaxValue;

    float _upValue;
    float _value;

    private Stat _stat;
    private float _duration;
    void Start()
    {

    }
    public void StartBuff(BuffManager.BuffEffect type, float value, float time)
    {
       _stat = GetComponentInParent<Stat>(); // 자신이 속한 객체의 Stat을 찾는다.

        gameObject.name = type.ToString(); // 버프 오브젝트의 이름은 버프 타입의 이름과 동일하게 한다.

        _duration = time; // 버프 지속 시간

        _value = value; // 증가하는 배율 설정.

        switch(_stat.Type) // 타입에 따라서 생성하는 UI가 다르다.
        {
            case Stat.TypeEnum.Player:
                UIManager._instacne.StartPlayerBuffUI(type, time);
                break;
            case Stat.TypeEnum.Enemy:
                UIManager._instacne.StartEnemyBuffUI(type, time);
                break;
        }
        
        switch (type)
        {
            case BuffManager.BuffEffect.AtkUp:
                StartCoroutine(StartAtkBuffCo());
                break;
            case BuffManager.BuffEffect.DefUp:
                StartCoroutine(StartDefBuffCo());
                break;
            case BuffManager.BuffEffect.MovSpdUp:
                StartCoroutine(StartMovSpdBuffCo());
                break;
        }
    }
    IEnumerator StartAtkBuffCo()
    {
        _upMinValue = _stat.MinAtk * _value;
        _upMaxValue = _stat.MaxAtk * _value;

        _stat.MinAtk += _upMinValue;
        _stat.MaxAtk += _upMaxValue;

        yield return new WaitForSeconds(_duration);

        _stat.MinAtk -= _upMinValue;
        _stat.MaxAtk -= _upMaxValue;

        Destroy(gameObject);
    }
    IEnumerator StartDefBuffCo()
    {
        _upValue = _stat.Defense * _value;

        _stat.Defense += _upValue;

        yield return new WaitForSeconds(_duration);

        _stat.Defense -= _upValue;

        Destroy(gameObject);
    }
    IEnumerator StartMovSpdBuffCo()
    {
        _upValue = _stat.MoveSpd * _value;

        _stat.MoveSpd += _upValue;

        yield return new WaitForSeconds(_duration);

        _stat.MoveSpd -= _upValue;

        Destroy(gameObject);
    }
    /*
    public void StartAtkBuff(GameObject target, float value, float time)
    {
        FindStat(target);
        gameObject.name = "AtkBuff";
        _duration = time;
        if (_playerStat != null)
        {
            UIManager._instacne.StartPlayerBuffUI(BuffManager.BuffEffect.AtkUp, time);
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
            UIManager._instacne.StartPlayerBuffUI(BuffManager.BuffEffect.DefUp, time);
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
            UIManager._instacne.StartPlayerBuffUI(BuffManager.BuffEffect.MovSpdUp, time);
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

    void UpAtk(float value)
    {

    }
    void UpPlayerAtk(float value)
    {
        _upMinAtk = _playerStat.MinAtk * value;
        _upMaxAtk = _playerStat.MaxAtk * value;

        _upSwordMinAtk = _playerStat.SwordMinAtk * value;
        _upSwordMaxAtk = _playerStat.SwordMaxAtk * value;

        _upSpearMinAtk = _playerStat.SpearMinAtk * value;
        _upSpearMaxAtk = _playerStat.SpearMaxAtk * value;

        _upAxeMinAtk = _playerStat.AxeMinAtk * value;
        _upAxeMaxAtk = _playerStat.AxeMaxAtk * value;

        /*
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

    void FindStat(GameObject target)
    {
        _playerStat = target.GetComponent<PlayerStat>();
        _monsterStat = target.GetComponent<MonsterStat>();
        _bossStat = target.GetComponent<BossStat>();
    }
    */
}
