using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DN_Move : BasePlayerMove
{
    [SerializeField] MoveEffectSound _type = MoveEffectSound.Wood;
    RaycastHit _hit, _previousHit;
    int _layerMask = 1 << 13 | 1 << 14; //(int)MoveEffectSound.Grass | (int)MoveEffectSound.Wood;
    private AudioSource _stepSound;
    [SerializeField] private AudioClip _nowClip;

    void Start()
    {
        _stat = GetComponent<PlayerStat>();
        _state = GetComponent<BasePlayerState>();
        _anim = GetComponent<Player_DN_Anim>();
        _stepSound = SoundManager._instance._stepSound;
        _stepSound.volume = SoundManager._instance.EffectVolume;

        _cameTrans = Camera.main.transform;


        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 5f, _layerMask))
        {
            _previousHit = _hit;
            _type = (MoveEffectSound)_hit.collider.gameObject.layer;
            _nowClip = SoundManager._instance.GetMoveClip((int)_type);
        }
    }

    void Update()
    {
        CheckLayer();
    }
    private void FixedUpdate()
    {
        if (GameManager._instance.PlayerDie || SkillManager._instance._isSkilling)
            return;
        base.MoveLogic();
        base.UpdateState();
    }
    
    protected override void UpdateIdle()
    {
        if(_magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _state.PlayerState = BasePlayerState.EPlayerState.Walk;
            }
            else
            {
                _state.PlayerState = BasePlayerState.EPlayerState.Run;
            }
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
    void CheckLayer() // Layer(플레이어가 서 있는 위치)에 따라 Type을 변경하여, 발 사운드(풀, 나무, 등등)의 사운드로 변경
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

                _nowClip = SoundManager._instance.GetMoveClip((int)_type);
            }
        }
    }
    public void StepSound()
    {
        _stepSound.PlayOneShot(_nowClip);
    }
}
