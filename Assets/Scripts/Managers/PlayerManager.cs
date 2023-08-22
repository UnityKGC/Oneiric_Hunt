using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager _instance;

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

    private EPlayerState _playerState;


    [SerializeField] List<GameObject> _playerLst; // 플레이어 리스트 (꿈 기본, 꿈 전투, 현실기본, 현실 이벤트,  미니게임, )
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
    public void ChangePlayer(GameManager.PlayState playState)
    {
        switch(playState)
        {
            case GameManager.PlayState.Dream_Normal:
                break;
            case GameManager.PlayState.Dream_Battle:
                break;
            case GameManager.PlayState.Real_Normal:
                break;
            case GameManager.PlayState.MiniGame:
                break;
        }
    }
}
