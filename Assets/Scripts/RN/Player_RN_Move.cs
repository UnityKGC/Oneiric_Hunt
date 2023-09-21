using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RN_Move : BasePlayerMove
{
    [SerializeField] MoveEffectSound _type = MoveEffectSound.None;

    RaycastHit _hit, _previousHit;

    int _layerMask = 1 << 13 | 1 << 14;//(int)MoveEffectSound.Grass | (int)MoveEffectSound.Wood;

    private bool _isWalk = false;

    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _state = GetComponent<BasePlayerState>();
        _anim = GetComponent<Player_RN_Anim>();

        _cameTrans = Camera.main.transform;
    }
    void Start()
    {
        SoundManager._instance.PlayMoveSound(MoveEffectSound.None, _isWalk); // NoneÀº À½¾ÇÀ» ²ô´Â °Í.

        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 5f, _layerMask))
        {
            _previousHit = _hit;
            _type = (MoveEffectSound)_hit.collider.gameObject.layer;
        }
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 5f, _layerMask))
        {
            int hitLayer = _hit.collider.gameObject.layer;
            if (hitLayer != _previousHit.collider.gameObject.layer)
            {
                _type = (MoveEffectSound)hitLayer;
                _previousHit = _hit;

                if (hitLayer == (int)MoveEffectSound.Grass)
                {
                    _type = MoveEffectSound.Grass;
                }
                else if (hitLayer == (int)MoveEffectSound.Wood)
                {
                    _type = MoveEffectSound.Wood;
                }
            }
        }
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
        if (_magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _state.PlayerState = BasePlayerState.EPlayerState.Walk;
                _isWalk = true;
            }
            else
            {
                _state.PlayerState = BasePlayerState.EPlayerState.Run;
                _isWalk = false;
            }
            SoundManager._instance.PlayMoveSound(_type, _isWalk);
            _anim.CrossFade(_state.PlayerState);
        }
    }
    protected override void UpdateMove()
    {
        if (_magnitude <= 0)
        {
            SoundManager._instance.PlayMoveSound(MoveEffectSound.None, _isWalk); // NoneÀº À½¾ÇÀ» ²ô´Â °Í.
            
            _state.PlayerState = BasePlayerState.EPlayerState.Idle;
            _anim.CrossFade(_state.PlayerState);
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _magnitude /= 2f;
            if(_state.PlayerState != BasePlayerState.EPlayerState.Walk)
            {
                _isWalk = true;
                _state.PlayerState = BasePlayerState.EPlayerState.Walk;
                SoundManager._instance.PlayMoveSound(_type, _isWalk);
            }
        }
        else
        {
            if(_state.PlayerState != BasePlayerState.EPlayerState.Run)
            {
                _isWalk = false;
                _state.PlayerState = BasePlayerState.EPlayerState.Run;
                SoundManager._instance.PlayMoveSound(_type, _isWalk);
            }
        }

        _anim.CrossFade(_state.PlayerState);

        transform.position += _dir * _magnitude * Time.deltaTime;
    }
}
