using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RN_Move : BasePlayerMove
{
    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _state = GetComponent<BasePlayerState>();
        _anim = GetComponent<Player_RN_Anim>();

        _cameTrans = Camera.main.transform;
    }
    void Start()
    {

    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (GameManager._instance.PlayerDie)
            return;
        MoveLogic();
        UpdateState();
    }

    protected override void UpdateIdle()
    {
        if(_magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                _state.PlayerState = BasePlayerState.EPlayerState.Walk;
            else
                _state.PlayerState = BasePlayerState.EPlayerState.Run;

            _anim.CrossFade(_state.PlayerState);
        }
    }
    protected override void UpdateMove()
    {
        if (_magnitude <= 0)
        {
            _state.PlayerState = BasePlayerState.EPlayerState.Idle;
            _anim.CrossFade(_state.PlayerState);
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _magnitude /= 2f;
            _state.PlayerState = BasePlayerState.EPlayerState.Walk;
        }
        else
        {
            _state.PlayerState = BasePlayerState.EPlayerState.Run;
        }

        _anim.CrossFade(_state.PlayerState);

        transform.position += _dir * _magnitude * Time.deltaTime;
    }
}
