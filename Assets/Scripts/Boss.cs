using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum BossState
    {
        None = -1,
        Appear,
        Idle,
        Run,
        Attack, // 공격 중
        Skill, // 스킬 사용 중
        Die,
    }
    
    public BossState State 
    { 
        get { return _state; } 
        set 
        { 
            _state = value;
            switch(_state)
            {
                case BossState.Appear:
                    _anim.CrossFade("Appear", 0.1f);
                    break;
                case BossState.Idle:
                    _anim.CrossFade("Idle",0.1f);
                    break;
                case BossState.Run:
                    _anim.CrossFade("Move",0.1f);
                    break;
                case BossState.Attack:
                    _anim.CrossFade("Attack",0.1f, -1, 0);
                    break;
                case BossState.Skill:
                    break;
                case BossState.Die:
                    break;
            }
        } 
    }

    [SerializeField]
    private BossState _state = BossState.Idle;

    private BossSkill.SkillType _skillType = BossSkill.SkillType.All;

    public Collider _hand;
    //private CharacterController _ctrl;

    private BossStat _stat;
    private BossSkill _skill;
    private Rigidbody _rb;
    private GameObject _player;
    private Animator _anim;

    private Vector3 _dir; // 몬스터 => 플레이어의 방향벡터
    [SerializeField]
    private float _dist; // 몬스터와 플레이어 사이의 거리
    private Quaternion quat;

    private int _allSkillCount;
    private int _nearSkillCount;
    private int _farSkillCount;

    private bool _isAppear = false;
    private bool _isAttack = false;

    private bool _isDie = false;

    [SerializeField]
    private bool _ableSkill = true; // 스킬 사용가능함

    private float _skillStartTime;
    private float _skillDelayTime = 10f;

    private void Awake()
    {
        
        _stat = GetComponent<BossStat>();
        //_ctrl = GetComponent<CharacterController>();
        _skill = GetComponent<BossSkill>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }
    void Start()
    {
        _player = GameManager._instance.Player;

        _allSkillCount = System.Enum.GetValues(typeof(BossSkill.AllSkill)).Length;
        _nearSkillCount = System.Enum.GetValues(typeof(BossSkill.NearSkill)).Length;
        _farSkillCount = System.Enum.GetValues(typeof(BossSkill.FarSkill)).Length;

        transform.LookAt(_player.transform);

        //CameraManager._instance.StartBossCam();

        StartCoroutine(StartCo());
    }

    void Update()
    {
        if (_isDie || GameManager._instance.PlayerDie) return;

        if (!_player.activeSelf)
            _player = GameManager._instance.Player;

        if (_ableSkill == false)
        {
            if (_skillDelayTime <= Time.time - _skillStartTime)
            {
                _ableSkill = true;
            }
        }

        _dir = (_player.transform.position - transform.position).normalized;
        _dist = Vector3.Distance(_player.transform.position, transform.position);
    }
    private void FixedUpdate()
    {
        if (_isDie || GameManager._instance.PlayerDie) return;

        switch (State)
        {
            case BossState.Appear:
                if(!_isAppear) // 최초 상태라면
                    StartCoroutine(StartAppearCo());
                break;
            case BossState.Run:
                UpdateRun();
                break;
            case BossState.Attack:
                UpdateAttack();
                break;
            case BossState.Skill:
                UpdateSkill();
                break;
            case BossState.Die:
                UpdateDie();
                break;
        }
    }
    IEnumerator StartCo()
    {
        yield return new WaitForSeconds(1f); // 카메라 이동 시간

        State = BossState.Appear; // 상태변경 => 등장 애니메이션 실행
    }
    IEnumerator StartAppearCo()
    {
        _isAppear = true; // 등장했으니 true로
        
        yield return new WaitForSeconds(3.5f); // 등장 연출

        _player = GameManager._instance.Player;

        State = BossState.Run;
    }
    void UpdateRun() // 플레이어를 쫓는다.
    {
        transform.position += _dir * _stat.MoveSpd * Time.deltaTime;
        transform.LookAt(_player.transform);

        // 플레이어와의 거리가 5f 이상이다 => 분노, 돌진, 회복, 수호, 과다복용 중 랜덤사용

        if(_dist >= 5f && _ableSkill) // 거리가 5이상이면서, 스킬이 사용가능하면 스킬 사용
        {
            State = BossState.Skill;
            _skillType = BossSkill.SkillType.Far; // 먼 거리 스킬 사용 가능
        }

        if (_dist <= 2.5f)
            State = BossState.Attack;

    }
    void UpdateAttack()
    {
        transform.LookAt(_player.transform);

        if (_ableSkill)
        {
            State = BossState.Skill;
            _skillType = BossSkill.SkillType.Near;
        }
        else if (!_isAttack)
            StartCoroutine(StartAttackCo());

    }
    void UpdateSkill()
    {
        if (BossSkillManager._instance._isSkilling) // 스킬 사용중이면 리턴
        {
            return;
        }

        _skillStartTime = Time.time;
        _ableSkill = false;

        int value = 0;
        switch (_skillType)
        {
            case BossSkill.SkillType.All:
                value = Random.Range(0, _allSkillCount);
                switch (value)
                {
                    case (int)BossSkill.AllSkill.Despair:
                        _skill.Despair();
                        break;
                    case (int)BossSkill.AllSkill.Guardian:
                        _skill.Guardian();
                        break;
                    case (int)BossSkill.AllSkill.Anger:
                        _skill.Anger();
                        break;
                    case (int)BossSkill.AllSkill.Overdose:
                        _skill.Overdose();
                        break;
                    case (int)BossSkill.AllSkill.Rush:
                        _skill.Rush();
                        break;
                    case (int)BossSkill.AllSkill.Delirium:
                        _skill.Delirium();
                        break;
                    case (int)BossSkill.AllSkill.Stench:
                        _skill.Stench();
                        break;
                }
                break;

            case BossSkill.SkillType.Near:
                value = Random.Range(0, _nearSkillCount);
                switch (value)
                {
                    case (int)BossSkill.NearSkill.Despair:
                        _skill.Despair();
                        break;
                    case (int)BossSkill.NearSkill.Guardian:
                        _skill.Guardian();
                        break;
                    case (int)BossSkill.NearSkill.Overdose:
                        _skill.Overdose();
                        break;
                    case (int)BossSkill.NearSkill.Delirium:
                        _skill.Delirium();
                        break;
                    case (int)BossSkill.NearSkill.Stench:
                        _skill.Stench();
                        break;
                }
                break;

            case BossSkill.SkillType.Far:
                value = Random.Range(0, _farSkillCount);
                switch (value)
                {
                    case (int)BossSkill.FarSkill.Anger:
                        _skill.Anger();
                        break;
                    case (int)BossSkill.FarSkill.Overdose:
                        _skill.Overdose();
                        break;
                    case (int)BossSkill.FarSkill.Rush:
                        _skill.Rush();
                        break;
                    case (int)BossSkill.FarSkill.Stench:
                        _skill.Stench();
                        break;
                }
                break;
        }
    }
    void UpdateDie()
    {

    }
    IEnumerator StartAttackCo()
    {
        _isAttack = true;
        _hand.enabled = true;

        yield return new WaitForSeconds(2.5f);

        _hand.enabled = false;
        _isAttack = false;

        if (_dist <= 3f && !_ableSkill) // 공격이 끝났는데, 아직 범위 내에 있다면
            State = BossState.Attack; // 공격 계속
        else if(_dist <= 3f && _ableSkill)
        {
            State = BossState.Skill;
        }
        else if(_dist <= 5f && _ableSkill)
        {
            _skillType = BossSkill.SkillType.All;
            _ableSkill = false;
            State = BossState.Skill;
        }
        else
            State = BossState.Run; // 플레이어 쫓아가기

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAtk"))
        {
            _stat.SetDamage(other.GetComponentInParent<PlayerStat>().GetDamage());
        }
    }
}
