using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_DB_Attack : MonoBehaviour
{
    [SerializeField] Collider[] _weaponColls;
    [SerializeField] GameObject[] _weaponTrails; // 무기 잔상효과
    private PlayerStat _stat;
    private Player_DB_Anim _anim;
    //private Player_DB_Move _move;
    private Coroutine _atkCo;
    private Collider _weaponColl;
    private GameObject _weaponTrail;

    public bool _isAttack = false; // 외부에서 플레이어가 공격을 그만 뒀는지 확인하는 변수
    private bool _ischeckAttack = false; // 내부에서 플레이어의 공격을 했는지 확인하는 변수

    //private float _stopAtkTime;
    private float _startStopAtkTime; // 공격을 그만 둔 시간
    private float _atkAgreeTime = 1f; // 공격 인정 시간
    private float _atkDelay = 0.8f;
    //private float _idleTime = 2f;

    //private bool _isStopAtk = true;
    private bool _isFirstAttack = false;
    private bool _isSecondAtk = false;
    private bool _isThirdAtk = false;

    private WeaponType _nowType = WeaponType.Sword; // 현재 무기타입

    void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _anim = GetComponent<Player_DB_Anim>();
        //_move = GetComponent<Player_DB_Move>();
    }
    void Start()
    {
        _weaponColl = GameObject.FindWithTag("PlayerAtk").GetComponent<Collider>();

        _weaponColl.enabled = false; // 무기 collider의 기본은 false

        _weaponTrail = _weaponTrails[0];
        _weaponTrail.SetActive(false);
    }
    void Update()
    {
        if (GameManager._instance.PlayerDie || GameManager._instance.Playstate != GameManager.PlayState.Dream_Battle || PlayerManager._instance.IsMove || SkillManager._instance._isSkilling) return;

        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
            MobileCtrl();
        else
            PCCtrl();
        
    }
    
    
    void FixedUpdate()
    {

    }
    void PCCtrl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isAttack = true;

            if (_ischeckAttack) return; // 공격 딜레이 코루틴에서 _ischeckAttack을 조절하여, 계속 공격하지 못하게 막음.

            //_isStopAtk = false;

            _startStopAtkTime = 0f;

            if (Input.GetMouseButtonDown(0) && _isFirstAttack == false)
            {
                //Debug.Log("첫번째 공격 시작");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_1);
                _isFirstAttack = true;
            }
            else if (Input.GetMouseButtonDown(0) && _isSecondAtk == false)
            {
                //Debug.Log("두번째 공격 시작");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_2);
                _isSecondAtk = true;

            }
            else if (Input.GetMouseButtonDown(0) && _isThirdAtk == false)
            {
                //Debug.Log("세번째 공격 시작");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_3);
                _isThirdAtk = true;

            }
            else if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("네번째 공격 시작");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_4);
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false; // 다시 첫번째 공격으로

            }
            
            if (_atkCo != null) // 사실 할 필요 없는 듯 한데 혹시나 모를 변수를 막기위해??
                StopCoroutine(StartAttackDelayTime());

            StartCoroutine(StartAttackDelayTime());
        }
        else // 공격을 하지 않으면, 대기상태 시간측정 시작
        {
            if (_startStopAtkTime == 0) // 시간재기
                _startStopAtkTime = Time.time;
            
            if (Time.time - _startStopAtkTime > _atkAgreeTime) // 좌클릭을 1.2초 동안 하지 않으면
            {
                //StopAllCoroutines();
                _isAttack = false; // 공격중지
                _weaponColl.enabled = false;
                _ischeckAttack = false;
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false;

                _startStopAtkTime = 0f; // 시간도 초기화
            }
        }
    }
    void MobileCtrl()
    {
        if (SimpleInput.GetButtonDown("Attack"))
        {
            _isAttack = true;

            if (_ischeckAttack) return; // 공격 딜레이 코루틴에서 _ischeckAttack을 조절하여, 계속 공격하지 못하게 막음.

            //_isStopAtk = false;

            _startStopAtkTime = 0f;

            if (SimpleInput.GetButtonDown("Attack") && _isFirstAttack == false)
            {
                //Debug.Log("첫번째 공격 시작");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_1);
                _isFirstAttack = true;
            }
            else if (SimpleInput.GetButtonDown("Attack") && _isSecondAtk == false)
            {
                //Debug.Log("두번째 공격 시작");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_2);
                _isSecondAtk = true;
            }
            else if (SimpleInput.GetButtonDown("Attack") && _isThirdAtk == false)
            {
                //Debug.Log("세번째 공격 시작");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_3);
                _isThirdAtk = true;
            }
            else if (SimpleInput.GetButtonDown("Attack"))
            {
                //Debug.Log("네번째 공격 시작");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_4);
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false; // 다시 첫번째 공격으로
            }

            if (_atkCo != null) // 사실 할 필요 없는 듯 한데 혹시나 모를 변수를 막기위해??
                StopCoroutine(StartAttackDelayTime());

            StartCoroutine(StartAttackDelayTime());
        }
        else // 공격을 하지 않으면, 대기상태 시간측정 시작
        {
            if (_startStopAtkTime == 0) // 시간재기
                _startStopAtkTime = Time.time;

            if (Time.time - _startStopAtkTime > _atkAgreeTime) // 좌클릭을 1.2초 동안 하지 않으면
            {
                //StopAllCoroutines();
                _isAttack = false; // 공격중지
                _weaponColl.enabled = false;
                _ischeckAttack = false;
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false;

                _startStopAtkTime = 0f; // 시간도 초기화
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.CompareTag("MonsterAtk"))
        {
            float dmg = other.GetComponentInParent<MonsterStat>().GetDamage();
            _stat.SetDamage(dmg);
        }
        else if (other.CompareTag("BossAtk"))
        {
            float dmg = other.GetComponentInParent<BossStat>().GetDamage();
            _stat.SetDamage(dmg);
        }*/

        if(other.CompareTag("MonsterAtk") || other.CompareTag("BossAtk"))
        {
            float dmg = other.GetComponentInParent<Stat>().GetDamage();
            _stat.SetDamage(dmg);
        }
    }
    public void ChangeWeapon(WeaponType weapon) // 무기 Collider를 갱신;
    {
        for (int i = 0; i < (int)WeaponType.Max; i++)
        {
            if (i == (int)weapon)
            {
                _nowType = weapon;
                _weaponColl = _weaponColls[i];
                _weaponTrail = _weaponTrails[i];
            }
            _weaponTrails[i].SetActive(false);
        }
    }

    IEnumerator StartAttackDelayTime() // 공격 딜레이
    {
        SoundManager._instance.PlayAttackSound((WeaponSound)((int)_nowType)); // 현재 착용중인 무기 정보(Enum)과 EffectSound의 Enum이 동일

        _ischeckAttack = true;
        //AttackStart();

        yield return new WaitForSeconds(_atkDelay);

        //AttackEnd();
        _ischeckAttack = false;
    }
    void AttackSound()
    {
        
    }

    public void AttackStart()
    {
        _weaponColl.enabled = true;
        _weaponTrail.SetActive(true);
    }
    public void AttackEnd()
    {
        _weaponColl.enabled = false;
        _weaponTrail.SetActive(false);
    }
    private void OnDisable()
    {
        if(_weaponTrail != null)
            _weaponTrail.SetActive(false);
    }

}
