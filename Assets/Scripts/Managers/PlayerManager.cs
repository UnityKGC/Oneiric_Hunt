using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerOrder
{
    None = -1,
    PlayerRN,
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

    public GameObject _nowPlayer;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        //_nowPlayer = GameManager._instance.Player;
    }

    void Update()
    {
        
    }
    public void ChangePlayer(GameManager.PlayState playState, Vector3 pos, Quaternion rot) // 게임의 상태에 따라 소환되는 플레이어가 각자 다르다.
    {
        //Instantiate(_playerLst[(int)playState], pos, rot);

        // 처음 시작할 시, _nowPlayer가 null이기 때문에 비활성화 시키면 안됨..

        if (_nowPlayer != null)
        {
            _nowPlayer.SetActive(false);
        }
        GameObject player = null;
        switch (playState)
        {
            case GameManager.PlayState.Dream_Normal:
                player = _playerLst[(int)PlayerOrder.PlayerDN];
                if (_nowPlayer == player)
                {
                    _nowPlayer.SetActive(true);
                    return;
                }

                player.transform.position = pos;
                player.transform.rotation = rot;
                player.SetActive(true);
                GameManager._instance.Player = _nowPlayer = player;
                
                break;

            case GameManager.PlayState.Dream_Battle:
                player = _playerLst[(int)PlayerOrder.PlayerDB];
                if (_nowPlayer == player)
                {
                    _nowPlayer.SetActive(true);
                    return;
                }
                player.transform.position = pos;
                player.transform.rotation = rot;
                player.SetActive(true);
                GameManager._instance.Player = _nowPlayer = player;
                break;

            case GameManager.PlayState.Real_Normal:
                player = _playerLst[(int)PlayerOrder.PlayerRN];
                if (_nowPlayer == player)
                {
                    _nowPlayer.SetActive(true);
                    return;
                }
                player.transform.position = pos;
                player.transform.rotation = rot;
                player.SetActive(true);
                GameManager._instance.Player = _nowPlayer = player;
                break;
            case GameManager.PlayState.Real_Event:

                break;
            case GameManager.PlayState.MiniGame:

                break;
        }

        CameraManager._instance.ChangeCam(CameraType.PlayerCam); // 바뀐 플레이어에게 카메라 포커스
    }
}
