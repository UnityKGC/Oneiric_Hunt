using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Run,
        Attack, // ���� ��
        Hit, // ���� ����
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
                    StopAllCoroutines(); // ���� �ڷ�ƾ, ��Ʈ �ڷ�ƾ�� ��� ���߰� �Ѵ�.
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

    private Vector3 _dir; // ���� => �÷��̾��� ���⺤��
    [SerializeField]
    private float _dist; // ���Ϳ� �÷��̾� ������ �Ÿ�
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
    void UpdateIdle() // Idle�� �����ҷ���? => ���͵��� ��ȯ�Ǵ� ���⶧ ����� ��?
    {
        if(!_isAppear)
            StartCoroutine(StartAppear());
    }
    void UpdateRun() // �÷��̾ �Ѵ´�.
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
