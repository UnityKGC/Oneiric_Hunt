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
        if (QTEManager._instance.CheckQTEStart) // QTE이벤트가 시작했으면,
            State = CatchPoliceState.Jump;
    }
    private void UpdateJump()
    {
        // 점프 애니메이션 실행
        // 좌표는 플레이어에게, 속도도 빠르다.
        if(_jumpDest == Vector3.zero)
        {
            _jumpDest = _player.transform.position;
            _quatDest = Quaternion.LookRotation(_jumpDest - transform.position, Vector3.up);
        }
        
        transform.position = Vector3.MoveTowards(transform.position, _jumpDest, 10f * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _quatDest, 0.1f);

        // 판단 => 플레이어가 QTE 이벤트를 성공했는지 실패 했는지
        if(QTEManager._instance.CheckQTEEnd) // QTE이벤트가 끝났다면,
        {
            if (QTEManager._instance.CheckQTESuccess) // 플레이어가 QTE 이벤트에 성공했다면,
                State = CatchPoliceState.Die; // 경찰은 사망
            else
                State = CatchPoliceState.Catch; // 플레이어가 실패했으면, 경찰은 플레이어 잡음

            _isJumping = false;
        }
    }
    private void UpdateCatch()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 10f * Time.deltaTime);
        Vector3 dir = _player.transform.position - transform.position;
        Debug.Log("잡았다!");
        if (dir.magnitude < 1.5f)
        {
            // 애니메이션 실행
            State = CatchPoliceState.Fight;
        }
    }
    private void UpdateFight()
    {
        // 연타이벤트 실행
        if (CatchManager._instance.CheckCatchSuccess)
            State = CatchPoliceState.Die;
            
    }
    private void UpdateDie()
    {
        // 플레이어가 회피 성공 시 => 플레이어를 잡지 못할 시
        // 플레이어가 본인에게서 빠져나올 시
        transform.position = Vector3.MoveTowards(transform.position, _jumpDest, 10f * Time.deltaTime);
        Destroy(gameObject, 5f);
        Debug.Log("죽었다!");
    }
}
