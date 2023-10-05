using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RN_Move : BasePlayerMove
{
    [SerializeField] MoveEffectSound _type = MoveEffectSound.Wood;
    
    RaycastHit _hit, _previousHit;

    int _layerMask = 1 << 13 | 1 << 14; //(int)MoveEffectSound.Grass | (int)MoveEffectSound.Wood;

    private bool _isWalk = false;

    private AudioSource _stepSound;

    [SerializeField] private AudioClip _nowClip;

    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _state = GetComponent<BasePlayerState>();
        _anim = GetComponent<Player_RN_Anim>();
        
        _cameTrans = Camera.main.transform;
    }
    void Start()
    {
        _stepSound = SoundManager._instance._stepSound;
        _stepSound.volume = SoundManager._instance.EffectVolume;

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
        if (GameManager._instance.PlayerDie) return;
        if (DialogueManager._instance._isTalk && _state.PlayerState != BasePlayerState.EPlayerState.Idle)
        {
            _state.PlayerState = BasePlayerState.EPlayerState.Idle;
            _anim.CrossFade(_state.PlayerState);
            return;
        }

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

        if (Input.GetKey(KeyCode.LeftShift) || (Mathf.Abs(_h) <= 0.5f && Mathf.Abs(_v) <= 0.5f)) // 모바일 환경에서는 조이스틱을 살짝 움직이면, Walk가 되도록 변경
        {
            _magnitude /= 2f;
            if(_state.PlayerState != BasePlayerState.EPlayerState.Walk)
            {
                _isWalk = true;
                _state.PlayerState = BasePlayerState.EPlayerState.Walk;
            }
        }
        else
        {
            if(_state.PlayerState != BasePlayerState.EPlayerState.Run)
            {
                _isWalk = false;
                _state.PlayerState = BasePlayerState.EPlayerState.Run;
            }
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
