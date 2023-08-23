using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerAnim : MonoBehaviour
{
    protected Animator _anim;
    BasePlayerState.EPlayerState _animState;

    public virtual void CrossFade(BasePlayerState.EPlayerState state, SkillManager.Skills type = SkillManager.Skills.None) // 무기 종류의 인자?
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
                _anim.CrossFade("Walk", 0.1f);
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
            case BasePlayerState.EPlayerState.Die:

                break;
        }
    }
}
