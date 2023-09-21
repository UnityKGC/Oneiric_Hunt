using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerMove : MonoBehaviour
{
    public Vector3 _dir;

    public float _rotSpd;
    public float _h, _v;

    protected PlayerStat _stat;
    protected BasePlayerAnim _anim;
    protected BasePlayerState _state;
    protected Transform _cameTrans;
    protected AudioSource _moveSound;

    protected float _magnitude;

    protected void MoveLogic()
    {
        GetDir();
        Rotate();
    }
    protected void UpdateState()
    {
        switch (_state.PlayerState)
        {
            case BasePlayerState.EPlayerState.Idle:
                UpdateIdle();
                break;

            case BasePlayerState.EPlayerState.Walk:
            case BasePlayerState.EPlayerState.Run:
                UpdateMove();
                break;
        }
    }

    void GetDir()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            _h = SimpleInput.GetAxis("Horizontal");
            _v = SimpleInput.GetAxis("Vertical");
        } 
        else
        {
            _h = Input.GetAxis("Horizontal");
            _v = Input.GetAxis("Vertical");
        }
            
        _dir = new Vector3(_h, 0, _v);
        _dir = Quaternion.AngleAxis(_cameTrans.rotation.eulerAngles.y, Vector3.up) * _dir; // 기존 _dir * y축 기준으로 카메라의 rotation.y값만큼 Quaternion을 리턴한다.

        _dir = _dir.normalized;

        _magnitude = Mathf.Clamp01(_dir.magnitude) * _stat.MoveSpd; // 움직여야만 속도를 준다.
    }
    void Rotate()
    {
        if (_dir != Vector3.zero) // _dir이 0이 아니라면, 즉! 움직이고 있다면,
        {
            Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up); // 첫번째 인자는 바라보는 방향이며, 두번째 인자는 축이다. => 첫번째 인자는 바라보고자 하는 방향벡터가 들어가야한다.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, _rotSpd * Time.deltaTime); // (첫번째) 에서 (두번째)까지 (세번째)의 속도로 회전한 결과를 리턴한다.
        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMove() { }
}
