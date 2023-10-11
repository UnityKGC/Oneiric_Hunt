using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DetectPolice : MonoBehaviour
{
    public enum DetectPoliceState
    {
        Idle,
        Walk,
        Run,
    }

    public DetectPoliceState State { get { return _state; } set { _state = value; } }

    private DetectPoliceState _state = DetectPoliceState.Idle;

    public BoxCollider _coll;

    public float _detectRadius; // 시야범위
    public float _detectAngle; // 시야각도
    public float _walkSpd = 3f;
    public float _runSpd = 10f;
    public float _rotSpd = 720f;

    private Transform _trans;
    private CharacterController _ctrl;

    private bool _isFindPlayer = false;

    private Vector3 _dir; // 경찰과 플레이어의 방향벡터

    void Start()
    {
        _trans = GetComponent<Transform>();
        _ctrl = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ... 표시
    }
    private void FixedUpdate()
    {
        if (_isFindPlayer == false) return;

        switch(State)
        {
            case DetectPoliceState.Idle:
                UpdateIdle();
                break;
            case DetectPoliceState.Walk:
                UpdateWalk();
                break;
            case DetectPoliceState.Run:
                UpdateRun();
                break;
        }
    }

    void UpdateIdle()
    {
        // 가만히 있음
        // 가끔씩 Walk로 상태전환
    }
    void UpdateWalk()
    {
        // 지정된 WayPoint들을 이리저리 뒤흔듬
        // 플레이어를 인식하면 플레이어를 향해 이동하며, 본인의 시야내부에 플레이어가 들어온 경우, Run으로 상태전환
        if(_isFindPlayer) // 플레이어를 인식했다면,
        {
            _ctrl.SimpleMove(_dir * _walkSpd * Time.deltaTime);
            Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up);
            _trans.rotation = Quaternion.RotateTowards(_trans.rotation, quat, _rotSpd * Time.deltaTime);
        }
        else // 아니라면 그냥 WayPoint대로 움직임
        {

        }
    }
    void UpdateRun()
    {
        _ctrl.SimpleMove(_dir * _runSpd * Time.deltaTime);
        Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up);
        _trans.rotation = Quaternion.RotateTowards(_trans.rotation, quat, _rotSpd * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            State = DetectPoliceState.Walk;
            _isFindPlayer = true;
        }
    }
    private void OnTriggerStay(Collider other) // 플레이어를 감지한다. => 감지에 성공하면 변수를 변경한 후, Stay에서 나머지 실행
    {
        if(other.CompareTag("Player")) // 플레이어가 Collider안에 들어오고,
        {
            // ? 표시
            _dir = other.transform.position - _trans.position; // 플레이어와 적의 방향벡터를 구한다.
            _dir = _dir.normalized;
            float angle = Vector3.Angle(_dir, _trans.forward); // 적의 정면부터, 플레이어 위치까지의 각도를 구한다.

            if (angle <= _detectAngle / 2f && _dir.magnitude <= _detectRadius) // 적의 시야각도안에 플레이어가 있고, 방향벡터의 길이(플레이어와 적 사이의 거리)가 시야범위보다 작으면 
            {
                // ! 표시
                State = DetectPoliceState.Run;
                //Debug.Log("쫓아간다.");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isFindPlayer = false;
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Handles.color = Color.red; // 색을 붉은 색으로
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, _detectAngle / 2f, _detectRadius); // 시작점, 기준 축, 방향벡터, 각도, 범위
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -_detectAngle / 2f, _detectRadius);
    }*/
}
