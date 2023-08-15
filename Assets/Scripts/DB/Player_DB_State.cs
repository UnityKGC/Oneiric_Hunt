using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_State : MonoBehaviour
{
    public enum DB_State
    {
        None = -1,
        Idle,
        Run,
        Attack_1,
        Attack_2,
        Attack_3,
        Attack_4,
        Skill,
        Die,
    }
    public DB_State PlayerState { get { return _playerState; } set { _playerState = value; } }

    private DB_State _playerState;
}
