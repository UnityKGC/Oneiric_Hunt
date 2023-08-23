using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Move : BasePlayerMove
{
    private Player_DB_Attack _attack;

    private void Awake()
    {
        
    }
    void Start()
    {
        _stat = GetComponent<PlayerStat>();

        _state = GetComponent<BasePlayerState>();
        _anim = GetComponent<Player_DB_Anim>();
        _attack = GetComponent<Player_DB_Attack>();

        _cameTrans = Camera.main.transform;
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {

        if (GameManager._instance.PlayerDie || SkillManager._instance._isSkilling)
            return;

        base.MoveLogic();

        if (_attack._isAttack) return; // 공격중이면 이동은 불가능하게

        base.UpdateState();
    }
    protected override void UpdateIdle()
    {
        _anim.CrossFade(_state.PlayerState);
        PlayerManager._instance.IsMove = false;
        if (_magnitude > 0)
        {
            _state.PlayerState = BasePlayerState.EPlayerState.Run;
        }
    }
    protected override void UpdateMove()
    {
        _anim.CrossFade(_state.PlayerState);
        PlayerManager._instance.IsMove = true;
        if (_magnitude <= 0)
        {
            _state.PlayerState = BasePlayerState.EPlayerState.Idle;
            return;
        }
        transform.position += _dir * _magnitude * Time.deltaTime;
    }
}
