using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerState : MonoBehaviour
{
    public enum EPlayerState
    {
        None = -1,
        Idle,
        Walk,
        Run,
        Check,
        Attack_1,
        Attack_2,
        Attack_3,
        Attack_4,
        Skill,
        Die,
    }

    public EPlayerState PlayerState { get { return _playerState; } set { _playerState = value; } }

    private EPlayerState _playerState = EPlayerState.Idle;
}
