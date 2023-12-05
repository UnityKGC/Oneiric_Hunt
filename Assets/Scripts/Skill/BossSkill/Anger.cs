using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anger : MonoBehaviour
{
    [SerializeField]
    SphereCollider _firstColl; // ó�� �����ϰ�, ������ �� �ֺ����� �ִ� �������� ����ϴ� coll

    [SerializeField]
    SphereCollider _secondColl; // ��Ÿ�� �� �ֺ����� �ִ� �������� ����ϴ� coll

    [SerializeField]
    GameObject _parabola;

    private Parabola parabola;
    private Animator _bossAnim;
    Boss _boss;
    Vector3 _playerPos; // ��ų ��� ��, �÷��̾ �ִ� ��ġ
    //Vector3 _startBossPos; // ��ų ���� ��, ������ ��ġ

    //float _height;

    float _dmgStart; // ������ �ð����� start
    float _dmgDelay = 0.5f; // ������ �ִ� ������ �ð�

    float _startTime; // ���۽ð�
    [SerializeField]
    float _remainingTime; // �����ð�

    float _duringTime = 5f; // �� ���ӽð�
    float _firstTime = 1.5f;
    //float _waitTime = 0.5f;
    //float _secondTime = 3f; // ��Ÿ ���ӽð�

    float _bossDmg; // ��ų ��� ��, ���� ���ݷ�

    float _firstDmg = 1.5f; // ���� �� �ִ� ������ ����
    float _secondDmg = 0.3f; // �ֺ��� ��Ÿ �� �ִ� ������ ����

    float _atk; // ���� ��ų ���ݷ�

    float _timer; // �̵��ϴµ� �����ϴ� �ð�

    bool _isDmg; // �������� �����°�
    bool _isJumping; // �����ߴ°�
    bool _isArrive;
    public void SetBossDmg(float dmg) // �ܺο��� ȣ���� ��� ��
    {
        _bossDmg = dmg;
    }
    public void FirstSetDmg() // ù��° ���� ����
    {
        _atk = _bossDmg * _firstDmg; 
    }
    public void SecondSetDmg() // �ι�° ���� ����
    {
        _atk = _bossDmg * _secondDmg;
    }
    private void Awake()
    {
        
    }
    void Start()
    {
        _playerPos = GameManager._instance.Player.transform.position;
        //_startBossPos = transform.parent.position;

        _boss = transform.parent.GetComponent<Boss>();
        _bossAnim = _boss.GetComponent<Animator>();

        FirstSetDmg();
        StartCoroutine(StartFirstAtk());

        _startTime = Time.time;

    }
    private void Update()
    {
        _remainingTime = _duringTime - (Time.time - _startTime);
        if (_remainingTime >= 0f)
        {
            
        }
        else
        {
            BossSkillManager._instance._isSkilling = false;
            BossSkillManager._instance.EndSkill();
            Destroy(parabola.gameObject);
            Destroy(gameObject);
        }
    }
    
    IEnumerator StartFirstAtk()
    {
        GameObject obj = Instantiate(_parabola, transform.parent);
        parabola = obj.GetComponent<Parabola>();

        parabola.SetValue(transform.parent.gameObject, _playerPos, _firstTime);

        yield return new WaitForSeconds(_firstTime);

        _firstColl.enabled = true; // ����������, ù���� ���ݷ� ���� coll Ű�� => ���� Ű�� ������, ��ų ����� �� �̹� �÷��̾ �������� �԰ų�, �����߿� Enter�� ���ͼ� �������� ���� ����

        CameraManager._instance.StartEffectCam(CameraType.PlayerCam, 5, 1f);

        //_boss.transform.position = new Vector3(_boss.transform.position.x, 1f, _boss.transform.position.z);

        yield return new WaitForSeconds(0.5f); // ���� �� 0.5�� ���

        SecondSetDmg();

        _bossAnim.CrossFade("Anger_2_L", 0.1f);

        _firstColl.enabled = false;
        _secondColl.enabled = true;
    }

    private void OnTriggerEnter(Collider other) // ù��° ������
    {
        if (other.CompareTag("Player")) // �����ߴµ�, �÷��̾ �����Ѵٸ�,
        {
            if (_firstColl.enabled && !_secondColl.enabled) // ��, ù��° coll�� �����ְ�, �ι�° coll�� �����ְ�, �����ߴٸ�(_isArrive�� �߰����� ������, ó�� ��ų ���� ��, �÷��̾ �ִٸ� ������ ����)
            {
                //Debug.Log("ù��° ������ ���� : " + _atk);
                other.GetComponent<PlayerStat>().SetDamage(_atk); // ������ ����
            }
        }
    }
    private void OnTriggerStay(Collider other) // �ι�° ������
    {
        if (!_isDmg) // _isDmg���� => _dmgDelay���� false�� ���� => _isDmg�� false�� �Ǹ�, �ٽ� �ð� ����
            _dmgStart = Time.time;

        if (other.CompareTag("Player") && !_isDmg) // �÷��̾ ���� ���� �ְ�, �������� ������ �ʾ�����,
        {
            if (!_firstColl.enabled && _secondColl.enabled) // ù��°�� �����ְ�, �ι�°�� �����ִٸ�
            {
                //Debug.Log("�ι�° ������ ���� : " + _atk);
                _isDmg = true; // ������ �����Ƿ�, ���� true�� ��ȯ
                other.GetComponent<PlayerStat>().SetDamage(_atk); // ������ ����
            }
        }
        if (Time.time - _dmgStart >= _dmgDelay)
            _isDmg = false;
    }
}
