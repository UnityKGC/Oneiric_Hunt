using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anger : MonoBehaviour
{
    [SerializeField]
    SphereCollider _firstColl; // 처음 점프하고, 착지할 때 주변에게 주는 데미지를 담당하는 coll

    [SerializeField]
    SphereCollider _secondColl; // 난타할 때 주변에게 주는 데미지를 담당하는 coll

    [SerializeField]
    GameObject _parabola;

    private Parabola parabola;
    private Animator _bossAnim;
    Boss _boss;
    Vector3 _playerPos; // 스킬 사용 시, 플레이어가 있는 위치
    //Vector3 _startBossPos; // 스킬 시작 시, 보스의 위치

    //float _height;

    float _dmgStart; // 데미지 시간측정 start
    float _dmgDelay = 0.5f; // 데미지 주는 딜레이 시간

    float _startTime; // 시작시간
    [SerializeField]
    float _remainingTime; // 남은시간

    float _duringTime = 5f; // 총 지속시간
    float _firstTime = 1.5f;
    //float _waitTime = 0.5f;
    //float _secondTime = 3f; // 난타 지속시간

    float _bossDmg; // 스킬 사용 시, 보스 공격력

    float _firstDmg = 1.5f; // 착지 시 주는 데미지 배율
    float _secondDmg = 0.3f; // 주변을 난타 시 주는 데미지 배율

    float _atk; // 최종 스킬 공격력

    float _timer; // 이동하는데 측정하는 시간

    bool _isDmg; // 데미지를 입혔는가
    bool _isJumping; // 점프했는가
    bool _isArrive;
    public void SetBossDmg(float dmg) // 외부에서 호출해 줘야 함
    {
        _bossDmg = dmg;
    }
    public void FirstSetDmg() // 첫번째 공격 비율
    {
        _atk = _bossDmg * _firstDmg; 
    }
    public void SecondSetDmg() // 두번째 공격 비율
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

        _firstColl.enabled = true; // 도착했으니, 첫번재 공격력 감지 coll 키기 => 먼저 키고 있으면, 스킬 사용할 때 이미 플레이어가 데미지를 입거나, 점프중에 Enter에 들어와서 데미지를 입지 않음

        CameraManager._instance.StartEffectCam(CameraType.PlayerCam, 5, 1f);

        //_boss.transform.position = new Vector3(_boss.transform.position.x, 1f, _boss.transform.position.z);

        yield return new WaitForSeconds(0.5f); // 도착 후 0.5초 대기

        SecondSetDmg();

        _bossAnim.CrossFade("Anger_2_L", 0.1f);

        _firstColl.enabled = false;
        _secondColl.enabled = true;
    }

    private void OnTriggerEnter(Collider other) // 첫번째 데미지
    {
        if (other.CompareTag("Player")) // 착지했는데, 플레이어가 존재한다면,
        {
            if (_firstColl.enabled && !_secondColl.enabled) // 또, 첫번째 coll이 켜져있고, 두번째 coll이 꺼져있고, 도착했다면(_isArrive을 추가하지 않으면, 처음 스킬 시전 시, 플레이어가 있다면 데미지 입음)
            {
                //Debug.Log("첫번째 데미지 입힘 : " + _atk);
                other.GetComponent<PlayerStat>().SetDamage(_atk); // 데미지 입힘
            }
        }
    }
    private void OnTriggerStay(Collider other) // 두번째 데미지
    {
        if (!_isDmg) // _isDmg변수 => _dmgDelay마다 false로 변경 => _isDmg가 false가 되면, 다시 시간 측정
            _dmgStart = Time.time;

        if (other.CompareTag("Player") && !_isDmg) // 플레이어가 범위 내에 있고, 데미지를 입히지 않았으면,
        {
            if (!_firstColl.enabled && _secondColl.enabled) // 첫번째는 꺼져있고, 두번째는 켜져있다면
            {
                //Debug.Log("두번째 데미지 입힘 : " + _atk);
                _isDmg = true; // 데미지 줬으므로, 변수 true로 전환
                other.GetComponent<PlayerStat>().SetDamage(_atk); // 데미지 입힘
            }
        }
        if (Time.time - _dmgStart >= _dmgDelay)
            _isDmg = false;
    }
}
