using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public enum PlayState // 게임의 현재 상태
    {
        None,
        Dream_Normal,
        Dream_Battle,
        Real_Normal,
        Real_Event,
        MiniGame,
    }

    public PlayState Playstate 
    { 
        get { return _playState; } 
        set
        {
            _playState = value;
            UIManager._instacne.SetPlayState(_playState); // UI에게 게임상태 변화했다고 알림.
            PlayerManager._instance.ChangePlayer(_playState, Player.transform.position, Player.transform.rotation);
        }
    }

    private PlayState _playState;

    public GameObject Player { get { return _player; } set { _player = value; } }

    private GameObject _player;

    public Transform PlayerFocus { get { return GameObject.FindGameObjectWithTag("CamFocus").transform; } }

    public bool PlayerDie { get { return _isPlayerDie; } set { _isPlayerDie = value; } }
    
    private bool _isPlayerDie= false;

    public bool FirstTuto { get { return _isFirst; } set { _isFirst = value; } }
    public bool SecondTuto { get { return _isSecond; } set { _isSecond = value; } }

    private bool _isFirst = false;
    private bool _isSecond = false;
    private void Awake()
    {
        _instance = this;

        _player = GameObject.FindGameObjectWithTag("Player"); // Scene 전환 후, Player 태그를 찾아, 변수에 등록한다.
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void GameStart() // Scene 시작 시 공통적으로 해야 할 모든 것들
    {
        CameraManager._instance.AbleCinemachineCams(); // 시네머신 카메라's 활성화
    }
    public void GameOver() // 게임오버 시 해야 할 모든 것들
    {
        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.GameOver); // 게임 오버 UI 호출
        CameraManager._instance.DisableCinemachineCams(); // 카메라 시네머신들 비활성화
    }
    
    private void OnDestroy()
    {
        _isPlayerDie = false;
    }
}
