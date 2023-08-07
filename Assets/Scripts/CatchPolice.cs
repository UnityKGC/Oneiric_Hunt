using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatchPolice : MonoBehaviour
{
    public enum CatchPoliceState
    {
        None,
        Idle,
        Jump,
        Catch,
        Fight,
        Die,
    }
    public CatchPoliceState State { get { return _state; } set{ _state = value; } }
    public CatchPoliceState _state = CatchPoliceState.Idle;

    public GameObject _player = null;

    public float _moveSpd = 5f;
    public float _rotSpd = 720f;

    private CharacterController _ctrl;

    public Vector3 _dir;
    private Vector3 _jumpDest;
    private Quaternion _quatDest;

    public bool _isJumping;
    public bool _isCatch = true;
    public bool _isFight;

    private void Awake()
    {
        _ctrl = GetComponent<CharacterController>();
    }
    void Start()
    {
        
    }
    
    void Update()
    {
        if (GameManager._instance.ChasePlayerDie) return;

        switch (State)
        {
            case CatchPoliceState.Idle:
                UpdateIdle();
                break;
            case CatchPoliceState.Jump:
                UpdateJump();
                break;
            case CatchPoliceState.Catch:
                UpdateCatch();
                break;
            case CatchPoliceState.Fight:
                UpdateFight();
                break;
            case CatchPoliceState.Die:
                UpdateDie();
                break;
        }
    }
    private void UpdateIdle()
    {
        _ctrl.SimpleMove(Vector3.zero * _moveSpd * Time.deltaTime);
        if (QTEManager._instance.CheckQTEStart) // QTE�̺�Ʈ�� ����������,
            State = CatchPoliceState.Jump;
    }
    private void UpdateJump()
    {
        // ���� �ִϸ��̼� ����
        // ��ǥ�� �÷��̾��, �ӵ��� ������.
        if(_jumpDest == Vector3.zero)
        {
            _jumpDest = _player.transform.position;
            _quatDest = Quaternion.LookRotation(_jumpDest - transform.position, Vector3.up);
        }
        
        transform.position = Vector3.MoveTowards(transform.position, _jumpDest, 10f * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _quatDest, 0.1f);

        // �Ǵ� => �÷��̾ QTE �̺�Ʈ�� �����ߴ��� ���� �ߴ���
        if(QTEManager._instance.CheckQTEEnd) // QTE�̺�Ʈ�� �����ٸ�,
        {
            if (QTEManager._instance.CheckQTESuccess) // �÷��̾ QTE �̺�Ʈ�� �����ߴٸ�,
                State = CatchPoliceState.Die; // ������ ���
            else
                State = CatchPoliceState.Catch; // �÷��̾ ����������, ������ �÷��̾� ����

            _isJumping = false;
        }
    }
    private void UpdateCatch()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 10f * Time.deltaTime);
        Vector3 dir = _player.transform.position - transform.position;
        Debug.Log("��Ҵ�!");
        if (dir.magnitude < 1.5f)
        {
            // �ִϸ��̼� ����
            State = CatchPoliceState.Fight;
        }
    }
    private void UpdateFight()
    {
        // ��Ÿ�̺�Ʈ ����
        if (CatchManager._instance.CheckCatchSuccess)
            State = CatchPoliceState.Die;
            
    }
    private void UpdateDie()
    {
        // �÷��̾ ȸ�� ���� �� => �÷��̾ ���� ���� ��
        // �÷��̾ ���ο��Լ� �������� ��
        transform.position = Vector3.MoveTowards(transform.position, _jumpDest, 10f * Time.deltaTime);
        Destroy(gameObject, 5f);
        Debug.Log("�׾���!");
    }
}
