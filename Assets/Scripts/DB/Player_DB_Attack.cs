using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Attack : MonoBehaviour
{
    private PlayerStat _stat;
    private Player_DB_Anim _anim;
    private Player_DB_Move _move;

    public bool _isAttack = false;

    private float _stopAtkTime;
    private float _startAtkDelay;
    private float _atkDelay = 1f;
    private float _idleTime = 2f;

    private bool _isStopAtk = true;
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

    }
    void Update()
    {
        if (GameManager._instance.PlayerDie || GameManager._instance.Playstate != GameManager.PlayState.Dream_Battle || _move._isMove || SkillManager._instance._isSkilling) return;

        if (_isStopAtk)
        {
            // 2�� ���� ��� ������ �־�����
            if (Time.time - _stopAtkTime >= _idleTime)
            {
                Debug.Log("���� �ʱ�ȭ");
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (_isAttack)
                return;

            //_hand.enabled = true;

            if (Input.GetMouseButton(0) && _isFirstAttack == false)
            {
                Debug.Log("ù��° ���� ����");

                _anim.CrossFade(Player_DB_State.DB_State.Attack_1);
                _isStopAtk = false;
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
            StartCoroutine(StartAttackDelayTime());
        }
        else if (_isStopAtk == false) // ������ ���� ������, ������ �ð����� ����
        {
            //_hand.enabled = false;
            _stopAtkTime = Time.time;
            _isStopAtk = true;
        }
    }
    IEnumerator StartAttackDelayTime() // ���� ������
    {
        _isAttack = true;
        //_hand.enabled = true;

        yield return new WaitForSeconds(_atkDelay);

        //_hand.enabled = false;
        _isAttack = false;
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
}
