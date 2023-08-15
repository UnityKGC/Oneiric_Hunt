using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Anim : MonoBehaviour
{
    private Player_DB_State _state;

    private Player_DB_State.DB_State _animState;
    private Animator _anim;
    void Start()
    {
        _state = GetComponent<Player_DB_State>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_state.PlayerState == _animState) return;

        switch (_state.PlayerState)
        {
            case Player_DB_State.DB_State.Idle:
                UpdateIdle();
                break;

            case Player_DB_State.DB_State.Run:
                UpdateRun();
                break;

            case Player_DB_State.DB_State.Attack_1:

                break;
            case Player_DB_State.DB_State.Attack_2:

                break;
            case Player_DB_State.DB_State.Attack_3:

                break;
            case Player_DB_State.DB_State.Attack_4:

                break;
            case Player_DB_State.DB_State.Skill:

                break;
            case Player_DB_State.DB_State.Die:

                break;
        }
    }
    private void UpdateIdle()
    {
        _animState = Player_DB_State.DB_State.Idle;
        _anim.CrossFade("Idle", 0.1f);
    }
    private void UpdateRun()
    {
        _animState = Player_DB_State.DB_State.Run;
        _anim.CrossFade("Run", 0.1f);
    }
    private void UpdateAttack_1()
    {
        _animState = Player_DB_State.DB_State.Run;
        _anim.CrossFade("Run", 0.1f);
    }
    private void UpdateAttack_2()
    {
        _animState = Player_DB_State.DB_State.Run;
        _anim.CrossFade("Run", 0.1f);
    }
    private void UpdateAttack_3()
    {
        _animState = Player_DB_State.DB_State.Run;
        _anim.CrossFade("Run", 0.1f);
    }
    private void UpdateAttack_4()
    {
        _animState = Player_DB_State.DB_State.Run;
        _anim.CrossFade("Run", 0.1f);
    }
    private void UpdateSkill()
    {
        _animState = Player_DB_State.DB_State.Run;
        _anim.CrossFade("Run", 0.1f);
    }
    private void UpdateDie()
    {
        _animState = Player_DB_State.DB_State.Run;
        _anim.CrossFade("Run", 0.1f);
    }
}
