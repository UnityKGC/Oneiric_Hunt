using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Attack : MonoBehaviour
{
    private PlayerStat _stat;

    private float _stopAtkTime;
    private float _startAtkDelay;
    private float _atkDelay = 0.7f;
    private float _idleTime = 2f;

    private bool _isStopAtk = true;
    private bool _isAttack = false;
    private bool _isFirstAttack = false;
    private bool _isSecondAtk = false;
    private bool _isThirdAtk = false;

    void Awake()
    {
        _stat = GetComponent<PlayerStat>();
    }
    void Start()
    {

    }
    void Update()
    {
        if (GameManager._instance.PlayerDie || GameManager._instance.Playstate != GameManager.PlayState.Dream_Battle) return;

        if (_isStopAtk)
        {
            // 2�� ���� ��� ������ �־�����
            if (Time.time - _stopAtkTime >= _idleTime)
            {
                Debug.Log("���� �ʱ�ȭ");
                _isFirstAttack = _isSecondAtk = _isThirdAtk = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_isAttack)
                return;

            //_hand.enabled = true;

            if (Input.GetMouseButtonDown(0) && _isFirstAttack == false)
            {
                Debug.Log("ù��° ���� ����");
                _isStopAtk = false;
                _isFirstAttack = true;
            }
            else if (Input.GetMouseButtonDown(0) && _isSecondAtk == false)
            {
                Debug.Log("�ι�° ���� ����");
                _isSecondAtk = true;
            }
            else if (Input.GetMouseButtonDown(0) && _isThirdAtk == false)
            {
                Debug.Log("����° ���� ����");
                _isThirdAtk = true;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("�׹�° ���� ����");
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
