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
    // WeaponSwap스킬에는 ChagneWeapon이라는 함수를 호출해야한다
    // 이 함수는 플레이어가 지니고 있는 무기를 확인하고, 교체되는 무기로 변경해야 하는 것이다.
    // 그리고 애니메이션도 CrossFade의 인자로 무기를 넣어주고, 애니메이터도 변경해 줘야 한다.
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
        UIManager._instacne.SetWeapon(Weapon); // 현재 무기 정보를 UI매니저에게 전달
    }
    void Update()
    {
        if (GameManager._instance.PlayerDie || SkillManager._instance._isSkilling || GameManager._instance.Playstate != GameManager.PlayState.Dream_Battle) return; // 플레이어가 죽었거나, 스킬 사용 중 이라면 리턴
        // 우선 이것부터 별로다... 차라리 변수를 새로 만들어, 이벤트가 콜백될 때 마다 변수를 갱신키기게 하는게 좋으려나?

        if (!GameManager._instance.FirstTuto) return;

        if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButtonDown("Space"))
        {
            SkillManager._instance.StartSkill(SkillManager.Skills.Dodge, 0f, transform.position, transform.rotation, transform);
            _anim.CrossFade(BasePlayerState.EPlayerState.Skill, SkillManager.Skills.Dodge);
        }

        if (Input.GetKeyDown(KeyCode.Tab) || SimpleInput.GetButtonDown("Tab"))
        {
            SkillManager._instance.StartSkill(SkillManager.Skills.WeaponSwap, 0f, transform.position, transform.rotation, transform);
            _anim.CrossFade(BasePlayerState.EPlayerState.Skill, SkillManager.Skills.WeaponSwap);
        }

        if (!GameManager._instance.SecondTuto) return;

        if (Input.GetKeyDown(KeyCode.Z) || SimpleInput.GetButtonDown("Z"))
        {
            float dmg;
            switch (Weapon)
            {
                case WeaponType.Sword:
                    dmg = Random.Range(_stat.SwordMinAtk, _stat.SwordMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Slash, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, SkillManager.Skills.Slash);
                    break;
                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Stabing, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, SkillManager.Skills.Stabing);
                    break;
                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Takedown, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, SkillManager.Skills.Takedown);
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
                    SkillManager._instance.StartSkill(SkillManager.Skills.SwordForce, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, SkillManager.Skills.SwordForce);
                    break;

                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Sweep, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, SkillManager.Skills.Sweep);
                    break;

                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.WindMill, dmg, transform.position, transform.rotation, transform);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, SkillManager.Skills.WindMill);
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
                    SkillManager._instance.StartSkill(SkillManager.Skills.SpaceCut, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, SkillManager.Skills.SpaceCut);
                    break;

                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Challenge, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, SkillManager.Skills.Challenge);
                    break;

                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(SkillManager.Skills.Berserk, dmg, transform.position, transform.rotation);
                    _anim.CrossFade(BasePlayerState.EPlayerState.Skill, SkillManager.Skills.Berserk);
                    break;
            }
        }

    }
}
