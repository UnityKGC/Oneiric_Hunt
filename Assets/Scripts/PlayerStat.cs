using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float MaxHp { get { return _maxHp; }set { _maxHp = value; } }
    [SerializeField]
    private float _maxHp;
    public float HP { get { return _hp; } set { _hp = value; } }
    [SerializeField]
    private float _hp;

    public float MaxMp { get { return _maxMp; } set { _maxMp = value; } }
    [SerializeField]
    private float _maxMp;

    public float MP { get { return _mp; } set { _mp = value; } }
    [SerializeField]
    private float _mp;

    public float SwordMinAtk { get { return _swordMinAtk; } set { _swordMinAtk = value; } }
    [SerializeField]
    private float _swordMinAtk;

    public float SwordMaxAtk { get { return _swordMaxAtk; } set { _swordMaxAtk = value; } }
    [SerializeField]
    private float _swordMaxAtk;

    public float SpearMinAtk { get { return _spearMinAtk; } set { _spearMinAtk = value; } }
    [SerializeField]
    private float _spearMinAtk;

    public float SpearMaxAtk { get { return _spearMaxAtk; } set { _spearMaxAtk = value; } }
    [SerializeField]
    private float _spearMaxAtk;

    public float AxeMinAtk { get { return _axeMinAtk; } set { _axeMinAtk = value; } }
    [SerializeField]
    private float _axeMinAtk;

    public float AxeMaxAtk { get { return _axeMaxAtk; } set { _axeMaxAtk = value; } }
    [SerializeField]
    private float _axeMaxAtk;

    public float Defense { get { return _defense; } set { _defense = value; } }
    [SerializeField]
    private float _defense;

    public float MoveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    [SerializeField]
    private float _moveSpd;

    private PlayerSkill _player;

    private void Start()
    {
        _player = gameObject.GetComponent<PlayerSkill>();

        UIManager._instacne.SetPlayerHP(HP); // 현재 HP를 UI매니저에게 전달
    }
    public float GetDamage() // 누군가에게 데미지를 줄 때
    {
        if (_player == null)
            return 0f;

        float dmg = 0f;
        switch (_player.Weapon)
        {
            case PlayerSkill.WeaponType.Sword:
                dmg = Random.Range(_swordMinAtk, _swordMaxAtk);
                break;
            case PlayerSkill.WeaponType.Spear:
                dmg = Random.Range(_spearMinAtk, _spearMaxAtk);
                break;
            case PlayerSkill.WeaponType.Axe:
                dmg = Random.Range(_axeMinAtk, _axeMaxAtk);
                break;
        }
        return dmg;
    }
    public void SetDamage(float value)
    {
        float dmg = value - Defense;
        HP -= dmg;

        UIManager._instacne.SetPlayerHP(HP); // 현재 HP를 UI매니저에게 전달

        if (HP <= 0)
        {
            GameManager._instance.PlayerDie = true;

            GameManager._instance.GameOver(); // 게임매니저에게 게임오버라고 알림

            Debug.Log("플레이어 사망");
        }
    }
    public void AddBuff()
    {

    }
    public void AddDeBuff()
    {

    }
}
