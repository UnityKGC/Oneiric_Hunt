using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Run,
        Attack, // 공격 중
        Hit, // 공격 받음
        Die,
    }
    public MonsterState State 
    { 
        get { return _state; } 
        set 
        { 
            _state = value;
            switch(_state)
            {
                case MonsterState.Idle:
                    _anim.CrossFade("Idle", 0.1f);
                    break;
                case MonsterState.Run:
                    _anim.CrossFade("Run", 0.1f);
                    break;
                case MonsterState.Attack:
                    _anim.CrossFade("Attack", 0.1f, -1, 0);
                    break;
                case MonsterState.Hit:
                    _anim.CrossFade("Hit", 0.1f);
                    break;
                case MonsterState.Die:
                    _isDie = true;
                    StopAllCoroutines(); // 공격 코루틴, 히트 코루틴을 모두 멈추게 한다.
                    Destroy(gameObject, 2f);
                    _anim.CrossFade("Die", 0.1f);
                    break;
            }
        } 
    }

    [SerializeField]
    private MonsterState _state = MonsterState.Idle;

    public GameObject _hand;
    private Rigidbody _rb;
    private MonsterStat _stat;
    private Animator _anim;

    private GameObject _player;

    [SerializeField] GameObject _atkTrail;

    private Vector3 _dir; // 몬스터 => 플레이어의 방향벡터
    [SerializeField]
    private float _dist; // 몬스터와 플레이어 사이의 거리
    private Quaternion quat;

    private bool _isAppear = false;
    private bool _isAttack = false;

    private bool _isHit = false;
    private bool _isDie = false;
    
    private void Awake()
    {
        _stat = GetComponent<MonsterStat>();
        _rb = GetComponent<Rigidbody>();
        //_ctrl = GetComponent<CharacterController>();
    }
    void Start()
    {
        _player = GameManager._instance.Player;
        _anim = GetComponent<Animator>();
        transform.LookAt(_player.transform);

        _atkTrail.SetActive(false);
    }

    void Update()
    {
        if (_isDie || GameManager._instance.PlayerDie) return;
        _dir = (_player.transform.position - transform.position).normalized;
        _dist = Vector3.Distance(_player.transform.position, transform.position);
        quat = Quaternion.LookRotation(_dir, Vector3.up);
    }
    private void FixedUpdate()
    {
        if (_isDie || GameManager._instance.PlayerDie) return;

        switch (State)
        {
            case MonsterState.Idle:
                UpdateIdle();
                break;
            case MonsterState.Run:
                UpdateRun();
                break;
            case MonsterState.Attack:
                UpdateAttack();
                break;
            case MonsterState.Hit:
                UpdateHit();
                break;
        }
    }
    void UpdateIdle() // Idle이 존재할려나? => 몬스터들이 소환되는 연출때 사용할 듯?
    {
        if(!_isAppear)
            StartCoroutine(StartAppear());
    }
    void UpdateRun() // 플레이어를 쫓는다.
    {
        transform.position += _dir * _stat.MoveSpd * Time.deltaTime;
        //_ctrl.SimpleMove(_dir * _stat.MoveSpd * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 720f * Time.deltaTime);

        if (_dist <= 1.5f)
            State = MonsterState.Attack;
    }
    void UpdateAttack()
    {
        if(!_isAttack)
            StartCoroutine(StartAttackCo());
    }
    void UpdateHit()
    {
        if(!_isHit)
            StartCoroutine(StartHitCo());
    }
    IEnumerator StartAppear()
    {
        _isAppear = true;
        yield return new WaitForSeconds(1f);
        State = MonsterState.Run;
    }
    IEnumerator StartAttackCo()
    {
        _isAttack = true;
        _hand.SetActive(true);
        _atkTrail.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        _atkTrail.SetActive(false);
        _hand.SetActive(false);
        _isAttack = false;

        if (_dist <= 1.5f)
            State = MonsterState.Attack;
        else
            State = MonsterState.Run;
    }
    IEnumerator StartHitCo()
    {
        _isHit = true;

        yield return new WaitForSeconds(0.5f);

        if (_dist <= 1.5f)
            State = MonsterState.Attack;
        else
            State = MonsterState.Run;

        _isHit = false;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerAtk"))
        {
            _stat.SetDamage(other.GetComponentInParent<PlayerStat>().GetDamage());
            
        }
    }
}
