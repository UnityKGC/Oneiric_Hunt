using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RN_State : MonoBehaviour
{
    public enum RN_State
    {
        None = -1,
        Idle,
        Walk,
        Run,
    }
    public RN_State PlayerState { get { return _playerState; } set { _playerState = value; } }

    private RN_State _playerState;
}
