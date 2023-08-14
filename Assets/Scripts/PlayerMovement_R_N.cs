using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_R_N : MonoBehaviour
{
    public enum PlayerState_RN
    {
        None = -1,
        Idle,
        Walk,
        Run,
    }
    public PlayerState_RN PlayerState
    {
        get { return _playerState; }
        set
        {
            _playerState = value;
            switch(_playerState)
            {
                case PlayerState_RN.Idle:
                    _playerState = PlayerState_RN.Idle;
                    break;
                case PlayerState_RN.Walk:
                    _playerState = PlayerState_RN.Walk;
                    break;
                case PlayerState_RN.Run:
                    _playerState = PlayerState_RN.Run;
                    break;
            }
        }
    }
    private PlayerState_RN _playerState = PlayerState_RN.None;
    public float _rotSpd;
    public float _h, _v;

    private PlayerStat _stat;
    public Vector3 _dir;

    private Transform _cameTrans;

    public bool _isConfus = false;
    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _cameTrans = Camera.main.transform;
    }
    void Start()
    {

    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (GameManager._instance.PlayerDie) // ��ų�ε�, Move��ų�� ��� ���̶�� ����
            return;

        Move();
        Rotate();
    }
    void Move()
    {
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        _dir = new Vector3(_h, 0, _v);
        _dir = Quaternion.AngleAxis(_cameTrans.rotation.eulerAngles.y, Vector3.up) * _dir; // ���� _dir * y�� �������� ī�޶��� rotation.y����ŭ Quaternion�� �����Ѵ�.

        _dir = _dir.normalized;

        if (_isConfus) // �����̻� ���� => ������ ������ �ȵ�
            _dir = -_dir;

        float magnitude = Mathf.Clamp01(_dir.magnitude) * _stat.MoveSpd; // �������߸� �ӵ��� �ش�.

        if (GameManager._instance.Playstate == GameManager.PlayState.Dream_Battle)
        {
            magnitude /= 1.5f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            magnitude /= 2f;
        }

        transform.position += _dir * magnitude * Time.deltaTime;
    }
    void Rotate()
    {
        if (_dir != Vector3.zero) // _dir�� 0�� �ƴ϶��, ��! �����̰� �ִٸ�,
        {
            Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up); // ù��° ���ڴ� �ٶ󺸴� �����̸�, �ι�° ���ڴ� ���̴�. => ù��° ���ڴ� �ٶ󺸰��� �ϴ� ���⺤�Ͱ� �����Ѵ�.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, _rotSpd * Time.deltaTime); // (ù��°) ���� (�ι�°)���� (����°)�� �ӵ��� ȸ���� ����� �����Ѵ�.
        }

        /*
        float angle = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, angle, 0f);*/
    }
}
