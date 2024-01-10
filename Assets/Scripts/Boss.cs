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
        Attack, // ���� ��
        Skill, // ��ų ��� ��
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

    private Vector3 _dir; // ���� => �÷��̾��� ���⺤��
    [SerializeField]
    private float _dist; // ���Ϳ� �÷��̾� ������ �Ÿ�
    private Quaternion quat;

    private int _allSkillCount;
    private int _nearSkillCount;
    private int _farSkillCount;

    private bool _isAppear = false;
    private bool _isAttack = false;

    private bool _isDie = false;

    [SerializeField]
    private bool _ableSkill = true; // ��ų ��밡����

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
                if(!_isAppear) // ���� ���¶��
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
        yield return new WaitForSeconds(1f); // ī�޶� �̵� �ð�

        State = BossState.Appear; // ���º��� => ���� �ִϸ��̼� ����
    }
    IEnumerator StartAppearCo()
    {
        _isAppear = true; // ���������� true��
        
        yield return new WaitForSeconds(3.5f); // ���� ����

        _player = GameManager._instance.Player;

        State = BossState.Run;
    }
    void UpdateRun() // �÷��̾ �Ѵ´�.
    {
        transform.position += _dir * _stat.MoveSpd * Time.deltaTime;
        transform.LookAt(_player.transform);

        // �÷��̾���� �Ÿ��� 5f �̻��̴� => �г�, ����, ȸ��, ��ȣ, ���ٺ��� �� �������

        if(_dist >= 5f && _ableSkill) // �Ÿ��� 5�̻��̸鼭, ��ų�� ��밡���ϸ� ��ų ���
        {
            State = BossState.Skill;
            _skillType = BossSkill.SkillType.Far; // �� �Ÿ� ��ų ��� ����
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
        if (BossSkillManager._instance._isSkilling) // ��ų ������̸� ����
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

        if (_dist <= 3f && !_ableSkill) // ������ �����µ�, ���� ���� ���� �ִٸ�
            State = BossState.Attack; // ���� ���
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
            State = BossState.Run; // �÷��̾� �Ѿư���

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAtk"))
        {
            _stat.SetDamage(other.GetComponentInParent<PlayerStat>().GetDamage());
        }
    }
}
