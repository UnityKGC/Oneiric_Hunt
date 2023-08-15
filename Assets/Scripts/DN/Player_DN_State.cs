using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DN_State : MonoBehaviour
{
    public enum DN_State
    {
        None = -1,
        Idle,
        Walk,
        Run,
        Check, // 단서 상호작용 애니메이션 전용?
    }
    public DN_State PlayerState { get { return _playerState; } set { _playerState = value; } }

    private DN_State _playerState;
}
