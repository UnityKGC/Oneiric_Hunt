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
    public EnemyCanvas _canvas;
    public MonsterState State { get { return _state; } set { _state = value; } }

    [SerializeField]
    private MonsterState _state = MonsterState.Idle;

    public Collider _hand;
    //private CharacterController _ctrl;
    private Rigidbody _rb;
    private MonsterStat _stat;

    private GameObject _player;

    private Vector3 _dir; // 몬스터 => 플레이어의 방향벡터
    [SerializeField]
    private float _dist; // 몬스터와 플레이어 사이의 거리
    private Quaternion quat;

    private bool _isAppear = false;
    private bool _isAttack = false;

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

        transform.LookAt(_player.transform);
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
            case MonsterState.Die:
                UpdateDie();
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

    }
    void UpdateDie()
    {

    }
    IEnumerator StartAppear()
    {
        _isAppear = true;
        yield return new WaitForSeconds(2f);
        State = MonsterState.Run;
    }
    IEnumerator StartAttackCo()
    {
        _isAttack = true;
        _hand.enabled = true;
        yield return new WaitForSeconds(1f);
        _hand.enabled = false;
        _isAttack = false;

        if (_dist <= 1.5f)
            State = MonsterState.Attack;
        else
            State = MonsterState.Run;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerAtk"))
        {
            _stat.SetDamage(other.GetComponentInParent<PlayerStat>().GetDamage());
            _canvas.SetHPAmount(_stat.HP / _stat.MaxHp);
        }
    }
}
