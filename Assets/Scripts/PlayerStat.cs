using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    
    public float SwordMinAtk { get { return MinAtk * _swordMinAtk; } }
    [SerializeField]
    private float _swordMinAtk = 1f;

    public float SwordMaxAtk { get { return MaxAtk * _swordMaxAtk; } }
    [SerializeField]
    private float _swordMaxAtk = 1.25f;

    public float SpearMinAtk { get { return MinAtk * _spearMinAtk; } }
    [SerializeField]
    private float _spearMinAtk = 0.7f;

    public float SpearMaxAtk { get { return MaxAtk * _spearMaxAtk; } }
    [SerializeField]
    private float _spearMaxAtk = 1.33f;

    public float AxeMinAtk { get { return MinAtk * _axeMinAtk; } }
    [SerializeField]
    private float _axeMinAtk = 1.5f;

    public float AxeMaxAtk { get { return MaxAtk * _axeMaxAtk; } }
    [SerializeField]
    private float _axeMaxAtk = 1.75f;

    private Player_DB_Skill _player;
    private void Start()
    {
        _player = gameObject.GetComponent<Player_DB_Skill>();

        Type = TypeEnum.Player;

        UIManager._instacne.SetPlayerHP(HP); // ���� HP�� UI�Ŵ������� ����
    }
    public override float GetDamage() // ���������� �������� �� ��
    {
        if (_player == null)
            return 0f;

        float dmg = 0f;
        switch (_player.Weapon)
        {
            case WeaponType.Sword:
                dmg = Random.Range(SwordMinAtk, SwordMaxAtk);
                break;
            case WeaponType.Spear:
                dmg = Random.Range(SpearMinAtk, SpearMaxAtk);
                break;
            case WeaponType.Axe:
                dmg = Random.Range(AxeMinAtk, AxeMaxAtk);
                break;
        }
        return dmg;
    }
    public override void SetDamage(float value)
    {
        float dmg = value - Defense;
        HP -= dmg;

        UIManager._instacne.SetPlayerHP(HP); // ���� HP�� UI�Ŵ������� ����

        if (HP <= 0 && !GameManager._instance.PlayerDie)
        {
            GameManager._instance.PlayerDie = true;

            GameManager._instance.GameOver(); // ���ӸŴ������� ���ӿ������ �˸�

            Debug.Log("�÷��̾� ���");
        }
    }
}
