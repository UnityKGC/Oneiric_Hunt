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

    public bool _isAttack = false; // 외부에서 플레이어가 공격을 그만 뒀는지 확인하는 변수
    private bool _ischeckAttack = false; // 내부에서 플레이어의 공격을 했는지 확인하는 변수

    //private float _stopAtkTime;
    private float _startStopAtkTime; // 공격을 그만 둔 시간
    private float _atkAgreeTime = 0.8f; // 공격 인정 시간
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
        _weaponColl.enabled = false; // 무기 collider의 기본은 false
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
                Debug.Log("첫번째 공격 시작");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_1);
                _isFirstAttack = true;
            }
            else if (Input.GetMouseButton(0) && _isSecondAtk == false)
            {
                Debug.Log("두번째 공격 시작");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_2);
                _isSecondAtk = true;
            }
            else if (Input.GetMouseButton(0) && _isThirdAtk == false)
            {
                Debug.Log("세번째 공격 시작");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_3);
                _isThirdAtk = true;
            }
            else if (Input.GetMouseButton(0))
            {
                Debug.Log("네번째 공격 시작");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_4);
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false; // 다시 첫번째 공격으로
            }

            /*
            if (_atkCo != null) // 사실 할 필요 없는 듯 한데 혹시나 모를 변수를 막기위해
                StopCoroutine(StartAttackDelayTime());*/
            StartCoroutine(StartAttackDelayTime());
        }
        else// if (_isStopAtk == false) // 공격을 하지 않으면, 대기상태 시간측정 시작
        {
            /*
            if (_startStopAtkTime == 0) // 시간재기
                _startStopAtkTime = Time.time;*/
            StopAllCoroutines();
            _isAttack = false;
            _weaponColl.enabled = false;
            _ischeckAttack = false;
            _isFirstAttack = _isSecondAtk = _isThirdAtk = false;
            /*
            if (Time.time - _startStopAtkTime > _atkAgreeTime) // 좌클릭을 0.8초 동안 하지 않으면
            {
                //_isAttack = false;  // 변수들 초기화
                

                //_stopAtkTime = Time.time;
                //_isStopAtk = true;

                
                _startStopAtkTime = 0f; // 시간도 초기화
            }*/
        }
    }
    
    IEnumerator StartAttackDelayTime() // 공격 딜레이
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
    public void ChangeWeapon(WeaponType weapon) // 무기 Collider를 갱신;
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
