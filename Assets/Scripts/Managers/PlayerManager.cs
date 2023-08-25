using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerOrder
{
    None = -1,
    PlayerDN,
    PlayerDB,
}
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

    GameObject _nowPlayer;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        _nowPlayer = GameManager._instance.Player;
    }

    void Update()
    {
        
    }
    public void ChangePlayer(GameManager.PlayState playState, Vector3 pos, Quaternion rot) // 게임의 상태에 따라 소환되는 플레이어가 각자 다르다.
    {
        //Instantiate(_playerLst[(int)playState], pos, rot);
        if (_nowPlayer == null) return; // 처음에는 null이니까 리턴

        _nowPlayer.SetActive(false);
        GameObject player = null;
        switch (playState)
        {
            case GameManager.PlayState.Dream_Normal:
                player = _playerLst[(int)PlayerOrder.PlayerDN];
                player.transform.position = pos;
                player.transform.rotation = rot;
                player.SetActive(true);
                GameManager._instance.Player = _nowPlayer = player;
                
                break;

            case GameManager.PlayState.Dream_Battle:
                player = _playerLst[(int)PlayerOrder.PlayerDB];
                player.transform.position = pos;
                player.transform.rotation = rot;
                player.SetActive(true);
                GameManager._instance.Player = _nowPlayer = player;
                break;

            case GameManager.PlayState.Real_Normal:

                break;
            case GameManager.PlayState.Real_Event:

                break;
            case GameManager.PlayState.MiniGame:

                break;
        }

        CameraManager._instance.SetFreeLookCam(); // 바뀐 플레이어에게 카메라 포커스
    }
}
