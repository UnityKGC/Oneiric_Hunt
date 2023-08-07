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

    public Vector3 _nowCheckPointPos; // ���� üũ����Ʈ ��ġ
    public Quaternion _nowCheckPointRot; // üũ ����Ʈ�� ������ �� �÷��̾� Rotation��
    
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

        if (_isDamaged && !_isResurrect) // �¾Ұ�, �츮�� �ִ� ���� �ƴ϶��,
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
    
    public void SetCheckPoint() // �÷��̾��� ���� üũ����Ʈ ����
    {
        _checkPointIndex++;
        _nowCheckPointPos = _checkPointList[_checkPointIndex].position; // ����Ʈ ����
        _nowCheckPointRot = _checkPointList[_checkPointIndex].rotation;
    }

    IEnumerator ResurrectPlayerCo() // �÷��̾� ��Ȱ
    {
        _isResurrect = true;

        yield return new WaitForSeconds(2f);

        ResurrectPlayer();
    }

    public void ResurrectPlayer()
    {
        if (!_isDead) // ������ ���� �ʾ�����
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
