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

    public float _detectRadius; // �þ߹���
    public float _detectAngle; // �þ߰���
    public float _walkSpd = 3f;
    public float _runSpd = 10f;
    public float _rotSpd = 720f;

    private Transform _trans;
    private CharacterController _ctrl;

    private bool _isFindPlayer = false;

    private Vector3 _dir; // ������ �÷��̾��� ���⺤��

    void Start()
    {
        _trans = GetComponent<Transform>();
        _ctrl = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ... ǥ��
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
        // ������ ����
        // ������ Walk�� ������ȯ
    }
    void UpdateWalk()
    {
        // ������ WayPoint���� �̸����� �����
        // �÷��̾ �ν��ϸ� �÷��̾ ���� �̵��ϸ�, ������ �þ߳��ο� �÷��̾ ���� ���, Run���� ������ȯ
        if(_isFindPlayer) // �÷��̾ �ν��ߴٸ�,
        {
            _ctrl.SimpleMove(_dir * _walkSpd * Time.deltaTime);
            Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up);
            _trans.rotation = Quaternion.RotateTowards(_trans.rotation, quat, _rotSpd * Time.deltaTime);
        }
        else // �ƴ϶�� �׳� WayPoint��� ������
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
    private void OnTriggerStay(Collider other) // �÷��̾ �����Ѵ�. => ������ �����ϸ� ������ ������ ��, Stay���� ������ ����
    {
        if(other.CompareTag("Player")) // �÷��̾ Collider�ȿ� ������,
        {
            // ? ǥ��
            _dir = other.transform.position - _trans.position; // �÷��̾�� ���� ���⺤�͸� ���Ѵ�.
            _dir = _dir.normalized;
            float angle = Vector3.Angle(_dir, _trans.forward); // ���� �������, �÷��̾� ��ġ������ ������ ���Ѵ�.

            if (angle <= _detectAngle / 2f && _dir.magnitude <= _detectRadius) // ���� �þ߰����ȿ� �÷��̾ �ְ�, ���⺤���� ����(�÷��̾�� �� ������ �Ÿ�)�� �þ߹������� ������ 
            {
                // ! ǥ��
                State = DetectPoliceState.Run;
                //Debug.Log("�Ѿư���.");
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
        Handles.color = Color.red; // ���� ���� ������
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, _detectAngle / 2f, _detectRadius); // ������, ���� ��, ���⺤��, ����, ����
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -_detectAngle / 2f, _detectRadius);
    }*/
}
