using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Anim : MonoBehaviour
{
    enum WaponAnimator
    {
        None = -1,
        Sword,
        Spear,
        Axe,
    }

    [SerializeField] List<RuntimeAnimatorController> _animLst;

    private Animator _anim;

    Player_DB_State.DB_State _animState;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void CrossFade(Player_DB_State.DB_State state) // 무기 종류의 인자?
    {
        if (_animState == state)
            return;


        _animState = state;

        switch (_animState)
        {
            case Player_DB_State.DB_State.Idle:
                _anim.CrossFade("Idle", 0.1f);
                break;
            case Player_DB_State.DB_State.Run:
                _anim.CrossFade("Run", 0.1f);
                break;
            case Player_DB_State.DB_State.Attack_1:
                _anim.CrossFade("Attack_1", 0.1f);
                break;
            case Player_DB_State.DB_State.Attack_2:
                _anim.CrossFade("Attack_2", 0.1f);
                break;
            case Player_DB_State.DB_State.Attack_3:
                _anim.CrossFade("Attack_3", 0.1f);
                break;
            case Player_DB_State.DB_State.Attack_4:
                _anim.CrossFade("Attack_4", 0.1f);
                break;
            case Player_DB_State.DB_State.Skill:

                break;
            case Player_DB_State.DB_State.Die:

                break;
        }
    }
}
