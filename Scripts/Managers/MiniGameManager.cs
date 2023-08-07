using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager _instance;
    public CinemachineVirtualCamera _cam;
    public GameObject _playerPrefab;

    [SerializeField] List<Transform> _checkPointList= new List<Transform>();

    int _checkPointIndex = 0;

    public Vector3 _nowCheckPointPos; // 현재 체크포인트 위치
    public Quaternion _nowCheckPointRot; // 체크 포인트를 갱신할 때 플레이어 Rotation값
    
    public GameObject Player { get { return _player; } set { _player = value; } }
    private GameObject _player;

    public float _nowHp = 100f;

    public bool _isDead = false;
    public bool _isDamaged = false;

    private bool _isResurrect = false;
    private void Awake()
    {
        _instance = this;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (_isDead) return;

        if (_isDamaged && !_isResurrect) // 맞았고, 살리고 있는 중이 아니라면,
        {
            Player = null;

            if (!_isResurrect)
                StartCoroutine(ResurrectPlayerCo());
        }

        if (_isDamaged && _isResurrect && Player != null)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Player.GetComponent<MiniGamePlayer>().enabled = true;
                _isResurrect = false;
                _isDamaged = false;
            }
        }
    }
    
    public void SetCheckPoint() // 플레이어의 현재 체크포인트 갱신
    {
        _checkPointIndex++;
        _nowCheckPointPos = _checkPointList[_checkPointIndex].position; // 리스트 복사
        _nowCheckPointRot = _checkPointList[_checkPointIndex].rotation;
    }

    IEnumerator ResurrectPlayerCo() // 플레이어 부활
    {
        _isResurrect = true;

        yield return new WaitForSeconds(2f);

        ResurrectPlayer();
    }

    public void ResurrectPlayer()
    {
        if (!_isDead) // 완전히 죽지 않았으면
        {
            Player = _player = Instantiate(_playerPrefab, _nowCheckPointPos, _nowCheckPointRot);

            Player.GetComponent<MiniGamePlayer>().enabled = false;

            _cam.Follow = Player.transform;
            _cam.LookAt = Player.transform;
        }
    }
    
    private void OnDestroy()
    {
        _instance = null;

        _nowHp = 100f;
        _isDead = false;
        _isDamaged = false;

        _nowCheckPointPos = Vector3.zero;
        _nowCheckPointRot = Quaternion.identity;
    }
}
