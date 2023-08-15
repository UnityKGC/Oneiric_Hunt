using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Move : MonoBehaviour
{
    public Vector3 _dir;

    private PlayerStat _stat;
    private Player_DB_Anim _anim;
    private Player_DB_State _state;
    private Transform _cameTrans;

    public float _rotSpd;
    public float _h, _v;

    private float _magnitude;

    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _state = GetComponent<Player_DB_State>();
        _anim = GetComponent<Player_DB_Anim>();

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
        if (GameManager._instance.PlayerDie) // 스킬인데, Move스킬을 사용 중이라면 진행
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
        }

        Rotate();
    }
    void GetDir()
    {
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        _dir = new Vector3(_h, 0, _v);
        _dir = Quaternion.AngleAxis(_cameTrans.rotation.eulerAngles.y, Vector3.up) * _dir; // 기존 _dir * y축 기준으로 카메라의 rotation.y값만큼 Quaternion을 리턴한다.

        _dir = _dir.normalized;

        _magnitude = Mathf.Clamp01(_dir.magnitude) * _stat.MoveSpd; // 움직여야만 속도를 준다.
    }
    private void UpdateIdle()
    {
        if (_magnitude > 0)
        {
            _state.PlayerState = Player_DB_State.DB_State.Run;

            _anim.CrossFade(_state.PlayerState);
        }
    }
    private void UpdateMove()
    {
        if (_magnitude <= 0)
        {
            _state.PlayerState = Player_DB_State.DB_State.Idle;
            _anim.CrossFade(_state.PlayerState);
            return;
        }

        _state.PlayerState = Player_DB_State.DB_State.Run;

        _anim.CrossFade(_state.PlayerState);

        transform.position += _dir * _magnitude * Time.deltaTime;
    }
    private void UpdateCheck()
    {

    }
    void Rotate()
    {
        if (_dir != Vector3.zero) // _dir이 0이 아니라면, 즉! 움직이고 있다면,
        {
            Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up); // 첫번째 인자는 바라보는 방향이며, 두번째 인자는 축이다. => 첫번째 인자는 바라보고자 하는 방향벡터가 들어가야한다.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, _rotSpd * Time.deltaTime); // (첫번째) 에서 (두번째)까지 (세번째)의 속도로 회전한 결과를 리턴한다.
        }
    }
}
