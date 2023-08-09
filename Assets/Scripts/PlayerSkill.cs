using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword,
    Spear,
    Axe,
    Max,
}

public class PlayerSkill : MonoBehaviour
{

    public WeaponType Weapon { get { return _weapon; } set { _weapon = value; } }

    public WeaponType _weapon = WeaponType.Sword;

    private PlayerStat _stat;

    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
    }
    void Start()
    {
        UIManager._instacne.SetWeapon(Weapon); // ���� ���� ������ UI�Ŵ������� ����
    }

    void Update()
    {
        if (GameManager._instance.PlayerDie || SkillManager._instance._isSkilling || GameManager._instance.Playstate != GameManager.PlayState.Dream_Battle) return; // �÷��̾ �׾��ų�, ��ų ��� �� �̶�� ����
        // �켱 �̰ͺ��� ���δ�... ���� ������ ���� �����, �̺�Ʈ�� �ݹ�� �� ���� ������ ����Ű��� �ϴ°� ��������?

        Debug.Log("���� ���� : " + Weapon.ToString());

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SkillManager._instance.StartSkill(SkillManager.Skills.WeaponSwap, 0f, transform.position, transform.rotation, transform);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ȸ��
            // 0.5��? �� ĳ������ Collider�� ���� => ��������
            // �̵��ӵ��� ��õ��� 2��� �ø���.
            if (SkillManager._instance.CheckCoolTime(SkillManager.Skills.Dodge))
            {
                SkillManager._instance.StartSkill(SkillManager.Skills.Dodge, 0f, transform.position, transform.rotation, transform);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (Weapon)
            {
                case WeaponType.Sword:

                    break;
                case WeaponType.Spear:

                    break;
                case WeaponType.Axe:

                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            float dmg;
            switch (Weapon)
            {
                case WeaponType.Sword:
                    dmg = Random.Range(_stat.SwordMinAtk, _stat.SwordMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.SwordForce, dmg, transform.position, transform.rotation);
                    break;

                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Sweep, dmg, transform.position, transform.rotation);
                    break;

                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.WindMill, dmg, transform.position, transform.rotation, transform);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            float dmg;
            switch (Weapon)
            {
                case WeaponType.Sword:
                    dmg = Random.Range(_stat.SwordMinAtk, _stat.SwordMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.SpaceCut, dmg, transform.position, transform.rotation);
                    break;

                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Challenge, dmg, transform.position, transform.rotation);
                    break;

                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Berserk, dmg, transform.position, transform.rotation);
                    break;
            }
        }
    }
}
