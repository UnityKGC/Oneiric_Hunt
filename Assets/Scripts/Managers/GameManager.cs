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
        Real_QTE,
        Real_Catch,
        MiniGame,
    }

    public PlayState Playstate 
    { 
        get { return _playState; } 
        set 
        { 
            _playState = value;
            UIManager._instacne.SetPlayState(_playState); // UI���� ���ӻ��� ��ȭ�ߴٰ� �˸�.
        } 
    }

    private PlayState _playState;

    public GameObject Player { get { return _player; } }

    private GameObject _player;

    public Transform PlayerFocus { get { return _playeFocus; } }

    private Transform _playeFocus;

    public bool ChasePlayerDie { get { return _isChasePlayerDie; } set { _isChasePlayerDie = value; } }
    public bool PlayerDie { get { return _isPlayerDie; } set { _isPlayerDie = value; } }
    
    private bool _isPlayerDie= false;

    private bool _isChasePlayerDie = false;

    private void Awake()
    {
        _instance = this;

        _player = GameObject.FindGameObjectWithTag("Player"); // Scene ��ȯ ��, Player �±׸� ã��, ������ ����Ѵ�.
        _playeFocus = GameObject.FindGameObjectWithTag("CamFocus").transform;
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
        _isChasePlayerDie = false;
    }
}
