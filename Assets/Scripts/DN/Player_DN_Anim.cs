using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DN_Anim : MonoBehaviour
{
    private Animator _anim;

    Player_DN_State.DN_State _animState;
    void Start()
    {
        _anim = GetComponent<Animator>();

    }

    public void CrossFade(Player_DN_State.DN_State state)
    {
        if (_animState == state)
            return;

        _animState = state;

        switch(_animState)
        {
            case Player_DN_State.DN_State.Idle:
                _anim.CrossFade("Idle", 0.1f);
                break;
            case Player_DN_State.DN_State.Walk:
                _anim.CrossFade("Walk", 0.1f);
                break;
            case Player_DN_State.DN_State.Run:
                _anim.CrossFade("Run", 0.1f);
                break;
            case Player_DN_State.DN_State.Check:
                _anim.CrossFade("Check", 0.1f);
                break;

        }
    }
}
