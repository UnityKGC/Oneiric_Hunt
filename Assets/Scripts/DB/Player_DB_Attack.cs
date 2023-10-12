using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_DB_Attack : MonoBehaviour
{
    [SerializeField] Collider[] _weaponColls;
    [SerializeField] GameObject[] _weaponTrails; // ���� �ܻ�ȿ��
    private PlayerStat _stat;
    private Player_DB_Anim _anim;
    //private Player_DB_Move _move;
    private Coroutine _atkCo;
    private Collider _weaponColl;
    private GameObject _weaponTrail;

    public bool _isAttack = false; // �ܺο��� �÷��̾ ������ �׸� �״��� Ȯ���ϴ� ����
    private bool _ischeckAttack = false; // ���ο��� �÷��̾��� ������ �ߴ��� Ȯ���ϴ� ����

    //private float _stopAtkTime;
    private float _startStopAtkTime; // ������ �׸� �� �ð�
    private float _atkAgreeTime = 1f; // ���� ���� �ð�
    private float _atkDelay = 0.8f;
    //private float _idleTime = 2f;

    //private bool _isStopAtk = true;
    private bool _isFirstAttack = false;
    private bool _isSecondAtk = false;
    private bool _isThirdAtk = false;

    private WeaponType _nowType = WeaponType.Sword; // ���� ����Ÿ��

    void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _anim = GetComponent<Player_DB_Anim>();
        //_move = GetComponent<Player_DB_Move>();
    }
    void Start()
    {
        _weaponColl = GameObject.FindWithTag("PlayerAtk").GetComponent<Collider>();

        _weaponColl.enabled = false; // ���� collider�� �⺻�� false

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

            if (_ischeckAttack) return; // ���� ������ �ڷ�ƾ���� _ischeckAttack�� �����Ͽ�, ��� �������� ���ϰ� ����.

            //_isStopAtk = false;

            _startStopAtkTime = 0f;

            if (Input.GetMouseButtonDown(0) && _isFirstAttack == false)
            {
                //Debug.Log("ù��° ���� ����");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_1);
                _isFirstAttack = true;
            }
            else if (Input.GetMouseButtonDown(0) && _isSecondAtk == false)
            {
                //Debug.Log("�ι�° ���� ����");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_2);
                _isSecondAtk = true;

            }
            else if (Input.GetMouseButtonDown(0) && _isThirdAtk == false)
            {
                //Debug.Log("����° ���� ����");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_3);
                _isThirdAtk = true;

            }
            else if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("�׹�° ���� ����");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_4);
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false; // �ٽ� ù��° ��������

            }
            
            if (_atkCo != null) // ��� �� �ʿ� ���� �� �ѵ� Ȥ�ó� �� ������ ��������??
                StopCoroutine(StartAttackDelayTime());

            StartCoroutine(StartAttackDelayTime());
        }
        else // ������ ���� ������, ������ �ð����� ����
        {
            if (_startStopAtkTime == 0) // �ð����
                _startStopAtkTime = Time.time;
            
            if (Time.time - _startStopAtkTime > _atkAgreeTime) // ��Ŭ���� 1.2�� ���� ���� ������
            {
                //StopAllCoroutines();
                _isAttack = false; // ��������
                _weaponColl.enabled = false;
                _ischeckAttack = false;
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false;

                _startStopAtkTime = 0f; // �ð��� �ʱ�ȭ
            }
        }
    }
    void MobileCtrl()
    {
        if (SimpleInput.GetButtonDown("Attack"))
        {
            _isAttack = true;

            if (_ischeckAttack) return; // ���� ������ �ڷ�ƾ���� _ischeckAttack�� �����Ͽ�, ��� �������� ���ϰ� ����.

            //_isStopAtk = false;

            _startStopAtkTime = 0f;

            if (SimpleInput.GetButtonDown("Attack") && _isFirstAttack == false)
            {
                //Debug.Log("ù��° ���� ����");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_1);
                _isFirstAttack = true;
            }
            else if (SimpleInput.GetButtonDown("Attack") && _isSecondAtk == false)
            {
                //Debug.Log("�ι�° ���� ����");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_2);
                _isSecondAtk = true;
            }
            else if (SimpleInput.GetButtonDown("Attack") && _isThirdAtk == false)
            {
                //Debug.Log("����° ���� ����");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_3);
                _isThirdAtk = true;
            }
            else if (SimpleInput.GetButtonDown("Attack"))
            {
                //Debug.Log("�׹�° ���� ����");

                _anim.CrossFade(BasePlayerState.EPlayerState.Attack_4);
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false; // �ٽ� ù��° ��������
            }

            if (_atkCo != null) // ��� �� �ʿ� ���� �� �ѵ� Ȥ�ó� �� ������ ��������??
                StopCoroutine(StartAttackDelayTime());

            StartCoroutine(StartAttackDelayTime());
        }
        else // ������ ���� ������, ������ �ð����� ����
        {
            if (_startStopAtkTime == 0) // �ð����
                _startStopAtkTime = Time.time;

            if (Time.time - _startStopAtkTime > _atkAgreeTime) // ��Ŭ���� 1.2�� ���� ���� ������
            {
                //StopAllCoroutines();
                _isAttack = false; // ��������
                _weaponColl.enabled = false;
                _ischeckAttack = false;
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false;

                _startStopAtkTime = 0f; // �ð��� �ʱ�ȭ
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
    public void ChangeWeapon(WeaponType weapon) // ���� Collider�� ����;
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

    IEnumerator StartAttackDelayTime() // ���� ������
    {
        SoundManager._instance.PlayAttackSound((WeaponSound)((int)_nowType)); // ���� �������� ���� ����(Enum)�� EffectSound�� Enum�� ����

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
