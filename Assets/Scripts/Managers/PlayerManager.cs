using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager _instance;

    public bool IsMove { get { return _isMove; } set { _isMove = value; } } // 이동 중인가

    private bool _isMove;

    public bool IsAttack { get { return _isAttack; } set { _isAttack = value; } } // 공격 중인가
    private bool _isAttack;

    public bool IsSkill { get { return _isSkill; } set { _isSkill = value; } } // 스킬 사용 중인가
    private bool _isSkill;

    [SerializeField] List<GameObject> _playerLst; // 플레이어 리스트 (순서는 게임 상태 Enum의 순서와 매치)
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ChangePlayer(GameManager.PlayState playState, Vector3 pos, Quaternion rot) // 게임의 상태에 따라 소환되는 플레이어가 각자 다르다.
    {
        //Instantiate(_playerLst[(int)playState], pos, rot);

        switch (playState)
        {
            case GameManager.PlayState.Dream_Normal:
                
                break;
            case GameManager.PlayState.Dream_Battle:

                break;
            case GameManager.PlayState.Real_Normal:

                break;
            case GameManager.PlayState.Real_Event:

                break;
            case GameManager.PlayState.MiniGame:

                break;
        }
    }
}
