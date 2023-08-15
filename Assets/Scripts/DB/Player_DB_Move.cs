using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Move : MonoBehaviour
{
    public Vector3 _dir;

    private PlayerStat _stat;
    private Player_DB_Anim _anim;
    private Player_DB_State _state;
    private Player_DB_Attack _attack;
    private Transform _cameTrans;

    public float _rotSpd;
    public float _h, _v;
    public bool _isMove = false;

    private float _magnitude;

    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();

        _state = GetComponent<Player_DB_State>();
        _anim = GetComponent<Player_DB_Anim>();
        _attack = GetComponent<Player_DB_Attack>();

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
        if (GameManager._instance.PlayerDie || _attack._isAttack || SkillManager._instance._isSkilling) // �׾��ų� �������̸� ����
            return;

        GetDir();

        switch (_state.PlayerState)
        {
            case Player_DB_State.DB_State.Idle:
                UpdateIdle();
                break;

            case Player_DB_State.DB_State.Run:
                UpdateMove();
                break;
            case Player_DB_State.DB_State.Skill:

                break;
        }

        Rotate();
    }
    void GetDir()
    {
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        _dir = new Vector3(_h, 0, _v);
        _dir = Quaternion.AngleAxis(_cameTrans.rotation.eulerAngles.y, Vector3.up) * _dir; // ���� _dir * y�� �������� ī�޶��� rotation.y����ŭ Quaternion�� �����Ѵ�.

        _dir = _dir.normalized;

        _magnitude = Mathf.Clamp01(_dir.magnitude) * _stat.MoveSpd; // �������߸� �ӵ��� �ش�.
    }
    private void UpdateIdle()
    {
        _anim.CrossFade(_state.PlayerState);
        _isMove = false;
        if (_magnitude > 0)
        {
            _state.PlayerState = Player_DB_State.DB_State.Run;
        }
    }
    private void UpdateMove()
    {
        _anim.CrossFade(_state.PlayerState);
        _isMove = true;
        if (_magnitude <= 0)
        {
            _state.PlayerState = Player_DB_State.DB_State.Idle;
            return;
        }
        transform.position += _dir * _magnitude * Time.deltaTime;
    }
    private void UpdateCheck()
    {

    }
    void Rotate()
    {
        if (_dir != Vector3.zero) // _dir�� 0�� �ƴ϶��, ��! �����̰� �ִٸ�,
        {
            Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up); // ù��° ���ڴ� �ٶ󺸴� �����̸�, �ι�° ���ڴ� ���̴�. => ù��° ���ڴ� �ٶ󺸰��� �ϴ� ���⺤�Ͱ� �����Ѵ�.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, _rotSpd * Time.deltaTime); // (ù��°) ���� (�ι�°)���� (����°)�� �ӵ��� ȸ���� ����� �����Ѵ�.
        }
    }
}
