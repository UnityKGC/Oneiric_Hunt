using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerAnim : MonoBehaviour
{
    protected Animator _anim;
    BasePlayerState.EPlayerState _animState;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void CrossFade(BasePlayerState.EPlayerState state, SkillManager.Skills type = SkillManager.Skills.None) // 무기 종류의 인자?
    {
        if (_animState == state)
            return;

        _animState = state;

        switch (_animState)
        {
            case BasePlayerState.EPlayerState.Idle:
                _anim.CrossFade("Idle", 0.1f);
                break;
            case BasePlayerState.EPlayerState.Walk:

                break;
            case BasePlayerState.EPlayerState.Run:
                _anim.CrossFade("Run", 0.1f);
                break;
            case BasePlayerState.EPlayerState.Check:

                break;
            case BasePlayerState.EPlayerState.Attack_1:
                _anim.CrossFade("Attack_1", 0.1f);
                break;
            case BasePlayerState.EPlayerState.Attack_2:
                _anim.CrossFade("Attack_2", 0.1f);
                break;
            case BasePlayerState.EPlayerState.Attack_3:
                _anim.CrossFade("Attack_3", 0.1f);
                break;
            case BasePlayerState.EPlayerState.Attack_4:
                _anim.CrossFade("Attack_4", 0.1f);
                break;
            case BasePlayerState.EPlayerState.Skill:
                StartSkillAnim(type);
                break;
            case BasePlayerState.EPlayerState.Die:

                break;
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
}
