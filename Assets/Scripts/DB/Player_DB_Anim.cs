using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Anim : BasePlayerAnim
{
    enum WeaponAnim
    {
        None = -1,
        Sword,
        Spear,
        Axe,
    }
    
    [SerializeField] List<RuntimeAnimatorController> _animLst;
    [SerializeField] List<GameObject> _weaponLst;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public override void CrossFade(BasePlayerState.EPlayerState state, SkillManager.Skills type = SkillManager.Skills.None) // 무기 종류의 인자?
    {
        base.CrossFade(state, type); // 기본적인 Anim동작 확인

        if (state == BasePlayerState.EPlayerState.Skill)
        {
            StartSkillAnim(type);
        }
    }

    public void ChangeWeapon(WeaponType weapon)
    {
        for(int i = 0; i < (int)WeaponType.Max; i++)
        {
            if(i == (int)weapon)
            {
                _weaponLst[i].SetActive(true);
                _anim.runtimeAnimatorController = _animLst[i];
            }
            else
            {
                _weaponLst[i].SetActive(false);
            } 
        }
    }

    void StartSkillAnim(SkillManager.Skills type) // 무기, 스킬타입을 인자로 받는다.
    {
        if (!SkillManager._instance.CheckCoolTime(type)) return; // 사용하려는 스킬을 아직 사용하지 못하면 리턴
        
        switch (type)
        {
            case SkillManager.Skills.Dodge: // Dodge라면 Run으로 변경시킨다.
                _anim.CrossFade("Dodge", 0.1f);
                break;

            case SkillManager.Skills.WeaponSwap:
                _anim.CrossFade("WeaponSwap", 0.1f);
                break;

            case SkillManager.Skills.Slash:
                _anim.CrossFade("Slash", 0.1f);
                break;

            case SkillManager.Skills.SwordForce:
                _anim.CrossFade("SwordForce", 0.1f);
                break;

            case SkillManager.Skills.SpaceCut:
                _anim.CrossFade("SpaceCut", 0.1f);
                break;

            case SkillManager.Skills.Stabing:
                _anim.CrossFade("Stabing", 0.1f);
                break;

            case SkillManager.Skills.Sweep:
                _anim.CrossFade("Sweep", 0.1f);
                break;

            case SkillManager.Skills.Challenge:
                _anim.CrossFade("Challenge", 0.1f);
                break;

            case SkillManager.Skills.Takedown:
                _anim.CrossFade("Takedown", 0.1f);
                break;

            case SkillManager.Skills.WindMill:
                _anim.CrossFade("WindMill", 0.1f);
                break;

            case SkillManager.Skills.Berserk:
                _anim.CrossFade("Berserk", 0.1f);
                break;
        }
    }
    // 스킬이 종료되면 다시 Idle이나 Move로 전환시켜야 하는데, 그걸 누가 언제 판별하지?
}
