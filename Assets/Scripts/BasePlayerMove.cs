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
        _dir = Quaternion.AngleAxis(_cameTrans.rotation.eulerAngles.y, Vector3.up) * _dir; // ���� _dir * y�� �������� ī�޶��� rotation.y����ŭ Quaternion�� �����Ѵ�.

        _dir = _dir.normalized;

        _magnitude = Mathf.Clamp01(_dir.magnitude) * _stat.MoveSpd; // �������߸� �ӵ��� �ش�.
    }
    void Rotate()
    {
        if (_dir != Vector3.zero) // _dir�� 0�� �ƴ϶��, ��! �����̰� �ִٸ�,
        {
            Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up); // ù��° ���ڴ� �ٶ󺸴� �����̸�, �ι�° ���ڴ� ���̴�. => ù��° ���ڴ� �ٶ󺸰��� �ϴ� ���⺤�Ͱ� �����Ѵ�.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, _rotSpd * Time.deltaTime); // (ù��°) ���� (�ι�°)���� (����°)�� �ӵ��� ȸ���� ����� �����Ѵ�.
        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMove() { }
}
