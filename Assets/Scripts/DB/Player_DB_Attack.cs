using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Attack : MonoBehaviour
{
    [SerializeField] Collider[] _weaponColls;

    private PlayerStat _stat;
    private Player_DB_Anim _anim;
    private Player_DB_Move _move;
    private Coroutine _atkCo;
    private Collider _weaponColl;

    public bool _isAttack = false; // �ܺο��� �÷��̾ ������ �׸� �״��� Ȯ���ϴ� ����
    private bool _ischeckAttack = false; // ���ο��� �÷��̾��� ������ �ߴ��� Ȯ���ϴ� ����

    //private float _stopAtkTime;
    private float _startStopAtkTime; // ������ �׸� �� �ð�
    private float _atkAgreeTime = 0.8f; // ���� ���� �ð�
    private float _atkDelay = 0.8f;
    //private float _idleTime = 2f;

    //private bool _isStopAtk = true;
    private bool _isFirstAttack = false;
    private bool _isSecondAtk = false;
    private bool _isThirdAtk = false;

    void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _anim = GetComponent<Player_DB_Anim>();
        _move = GetComponent<Player_DB_Move>();
    }
    void Start()
    {
        _weaponColl = GameObject.FindWithTag("PlayerAtk").GetComponent<Collider>();
        _weaponColl.enabled = false; // ���� collider�� �⺻�� false
    }
    void Update()
    {
        if (GameManager._instance.PlayerDie || GameManager._instance.Playstate != GameManager.PlayState.Dream_Battle || _move._isMove || SkillManager._instance._isSkilling) return;

        if (Input.GetMouseButton(0))
        {
            _isAttack = true;

            if (_ischeckAttack) return;

            //_isStopAtk = false;

            _startStopAtkTime = 0f;

            if (Input.GetMouseButton(0) && _isFirstAttack == false)
            {
                Debug.Log("ù��° ���� ����");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_1);
                _isFirstAttack = true;
            }
            else if (Input.GetMouseButton(0) && _isSecondAtk == false)
            {
                Debug.Log("�ι�° ���� ����");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_2);
                _isSecondAtk = true;
            }
            else if (Input.GetMouseButton(0) && _isThirdAtk == false)
            {
                Debug.Log("����° ���� ����");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_3);
                _isThirdAtk = true;
            }
            else if (Input.GetMouseButton(0))
            {
                Debug.Log("�׹�° ���� ����");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_4);
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false; // �ٽ� ù��° ��������
            }

            /*
            if (_atkCo != null) // ��� �� �ʿ� ���� �� �ѵ� Ȥ�ó� �� ������ ��������
                StopCoroutine(StartAttackDelayTime());*/
            StartCoroutine(StartAttackDelayTime());
        }
        else// if (_isStopAtk == false) // ������ ���� ������, ������ �ð����� ����
        {
            /*
            if (_startStopAtkTime == 0) // �ð����
                _startStopAtkTime = Time.time;*/
            StopAllCoroutines();
            _isAttack = false;
            _weaponColl.enabled = false;
            _ischeckAttack = false;
            _isFirstAttack = _isSecondAtk = _isThirdAtk = false;
            /*
            if (Time.time - _startStopAtkTime > _atkAgreeTime) // ��Ŭ���� 0.8�� ���� ���� ������
            {
                //_isAttack = false;  // ������ �ʱ�ȭ
                

                //_stopAtkTime = Time.time;
                //_isStopAtk = true;

                
                _startStopAtkTime = 0f; // �ð��� �ʱ�ȭ
            }*/
        }
    }
    
    IEnumerator StartAttackDelayTime() // ���� ������
    {
        _ischeckAttack = true;
        AttackStart();

        yield return new WaitForSeconds(_atkDelay);
        
        AttackEnd();
        _ischeckAttack = false;
    }
    void FixedUpdate()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonsterAtk"))
        {
            float dmg = other.GetComponentInParent<MonsterStat>().GetDamage();
            _stat.SetDamage(dmg);
        }
        else if (other.CompareTag("BossAtk"))
        {
            float dmg = other.GetComponentInParent<BossStat>().GetDamage();
            _stat.SetDamage(dmg);
        }
    }
    public void ChangeWeapon(WeaponType weapon) // ���� Collider�� ����;
    {
        for (int i = 0; i < (int)WeaponType.Max; i++)
        {
            if (i == (int)weapon)
            {
                _weaponColl = _weaponColls[i];
                return;
            }
        }
    }

    public void AttackStart()
    {
        _weaponColl.enabled = true;
    }
    public void AttackEnd()
    {
        _weaponColl.enabled = false;
    }
}
