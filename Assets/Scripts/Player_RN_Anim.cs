using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RN_Anim : MonoBehaviour
{
    private Player_RN_State _state;

    private Player_RN_State.RN_State _animState;
    private Animator _anim;
    void Start()
    {
        _state = GetComponent<Player_RN_State>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_state.PlayerState == _animState) return;

        switch (_state.PlayerState)
        {
            case Player_RN_State.RN_State.Idle:
                UpdateIdle();
                break;

            case Player_RN_State.RN_State.Run:
                UpdateRun();
                break;

            case Player_RN_State.RN_State.Walk:
                UpdateWalk();
                break;
        }
    }
    private void UpdateIdle()
    {
        _animState = Player_RN_State.RN_State.Idle;
        _anim.CrossFade("Idle", 0.1f);
    }
    private void UpdateWalk()
    {
        _animState = Player_RN_State.RN_State.Walk;
        _anim.CrossFade("Walk", 0.1f);
    }
    private void UpdateRun()
    {
        _animState = Player_RN_State.RN_State.Run;
        _anim.CrossFade("Run", 0.1f);
    }
}
