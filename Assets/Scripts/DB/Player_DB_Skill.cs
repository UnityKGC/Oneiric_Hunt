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

public class Player_DB_Skill : MonoBehaviour
{
    // WeaponSwap��ų���� ChagneWeapon�̶�� �Լ��� ȣ���ؾ��Ѵ�
    // �� �Լ��� �÷��̾ ���ϰ� �ִ� ���⸦ Ȯ���ϰ�, ��ü�Ǵ� ����� �����ؾ� �ϴ� ���̴�.
    // �׸��� �ִϸ��̼ǵ� CrossFade�� ���ڷ� ���⸦ �־��ְ�, �ִϸ����͵� ������ ��� �Ѵ�.
    public WeaponType Weapon { get { return _weapon; } set { _weapon = value; } }

    public WeaponType _weapon = WeaponType.Sword;

    private BasePlayerAnim _anim;
    private PlayerStat _stat;
    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _anim = GetComponent<Player_DB_Anim>();
    }
    void Start()
    {
        UIManager._instacne.SetWeapon(Weapon); // ���� ���� ������ UI�Ŵ������� ����
    }
    void Update()
    {
        if (GameManager._instance.PlayerDie || SkillManager._instance._isSkilling || GameManager._instance.Playstate != GameManager.PlayState.Dream_Battle) return; // �÷��̾ �׾��ų�, ��ų ��� �� �̶�� ����
        // �켱 �̰ͺ��� ���δ�... ���� ������ ���� �����, �̺�Ʈ�� �ݹ�� �� ���� ������ ����Ű��� �ϴ°� ��������?

        if (!GameManager._instance.FirstTuto) return;

        if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButtonDown("Space"))
        {
            SkillManager._instance.StartSkill(Skills.Dodge, 0f, transform.position, transform.rotation, transform);
            _anim.CrossFade(BasePlayerState.EPlayerState.Skill, Skills.Dodge);
        }

        if (Input.GetKeyDown(KeyCode.Tab) || SimpleInput.GetButtonDown("Tab"))
        {
            SkillManager._instance.StartSkill(Skills.WeaponSwap, 0f, transform.position, transform.rotation, transform);
            //_anim.CrossFade(BasePlayerState.EPlayerState.Skill, Skills.WeaponSwap);
        }

        if (!GameManager._instance.SecondTuto) return;

        if (Input.GetKeyDown(KeyCode.Z) || SimpleInput.GetButtonDown("Z"))
        {
            float dmg;
            switch (Weapon)
            {
                case WeaponType.Sword:
                    dmg = Random.Range(_stat.SwordMinAtk, _stat.SwordMaxAtk);
                    SkillManager._instance.StartSkill(Skills.Slash, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, Skills.Slash);
                    break;
                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(Skills.Stabing, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, Skills.Stabing);
                    break;
                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(Skills.Takedown, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, Skills.Takedown);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.X) || SimpleInput.GetButtonDown("X"))
        {
            float dmg;
            switch (Weapon)
            {
                case WeaponType.Sword:
                    dmg = Random.Range(_stat.SwordMinAtk, _stat.SwordMaxAtk);
                    SkillManager._instance.StartSkill(Skills.SwordForce, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, Skills.SwordForce);
                    break;

                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(Skills.Sweep, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, Skills.Sweep);
                    break;

                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(Skills.WindMill, dmg, transform.position, transform.rotation, transform);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, Skills.WindMill);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.C) || SimpleInput.GetButtonDown("C"))
        {
            float dmg;
            switch (Weapon)
            {
                case WeaponType.Sword:
                    dmg = Random.Range(_stat.SwordMinAtk, _stat.SwordMaxAtk);
                    SkillManager._instance.StartSkill(Skills.SpaceCut, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, Skills.SpaceCut);
                    break;

                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(Skills.Challenge, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, Skills.Challenge);
                    break;

                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(Skills.Berserk, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, Skills.Berserk);
                    break;
            }
        }

    }
}
