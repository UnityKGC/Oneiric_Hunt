using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public enum PlayState // ������ ���� ����
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
            UIManager._instacne.SetPlayState(_playState); // UI���� ���ӻ��� ��ȭ�ߴٰ� �˸�.
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

        _player = GameObject.FindGameObjectWithTag("Player"); // Scene ��ȯ ��, Player �±׸� ã��, ������ ����Ѵ�.
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void GameStart() // Scene ���� �� ���������� �ؾ� �� ��� �͵�
    {
        CameraManager._instance.AbleCinemachineCams(); // �ó׸ӽ� ī�޶�'s Ȱ��ȭ
    }
    public void GameOver() // ���ӿ��� �� �ؾ� �� ��� �͵�
    {
        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.GameOver); // ���� ���� UI ȣ��
        CameraManager._instance.DisableCinemachineCams(); // ī�޶� �ó׸ӽŵ� ��Ȱ��ȭ
    }
    
    private void OnDestroy()
    {
        _isPlayerDie = false;
    }
}
