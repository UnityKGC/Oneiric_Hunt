using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillManager : MonoBehaviour
{
    public static BossSkillManager _instance;

    public enum BossSkills
    {
        Despair,
        Guardian,
        Anger,
        OverDose,
        Rush,
        Delirium,
        Scent,
        Stench,
    }

    [SerializeField]
    List<SkillScriptable> _bossSkills = new List<SkillScriptable>();

    [SerializeField] GameObject _despair;
    [SerializeField] GameObject _guardian;
    [SerializeField] GameObject _anger;
    [SerializeField] GameObject _overdose;
    [SerializeField] GameObject _rush;
    [SerializeField] GameObject _delirium;
    [SerializeField] GameObject _scent;
    [SerializeField] GameObject _stench;

    public bool _isSkilling = false;

    private GameObject _boss;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void StartDespair(GameObject boss) // 보스의 위치, 보스의 스킬 범위
    {
        _boss = boss;
        GameObject obj = Instantiate(_despair, boss.transform.transform.position, Quaternion.identity);
        obj.GetComponent<Despair>();
    }
    public void StartGuardian(GameObject boss) 
    {
        _boss = boss;
        GameObject obj = Instantiate(_guardian, boss.transform.transform.position, Quaternion.identity);
        obj.GetComponent<Guardian>();
    }
    public void StartAnger(float dmg, GameObject boss)
    {
        _boss = boss;
        GameObject obj = Instantiate(_anger, boss.transform); // 보스에게 붙여준다.
        obj.GetComponent<Anger>().SetBossDmg(dmg); // 보스의 공격력을 준다.
    }
    public void StartOverdose(GameObject boss) 
    {
        _boss = boss;
        GameObject obj = Instantiate(_overdose, boss.transform); // 보스에게 붙여준다.
        obj.GetComponent<Overdose>();
    }
    public void StartRush(float dmg, GameObject boss)
    {
        _boss = boss;
        GameObject obj = Instantiate(_rush, boss.transform); // 보스에게 붙여준다.
        obj.GetComponent<Rush>().SetBossDmg(dmg);
    }
    public void StartDelirium(GameObject boss)
    {
        _boss = boss;
        GameObject obj = Instantiate(_delirium, boss.transform); // 보스에게 붙여준다.
        obj.GetComponent<Delirium>();
    }
    public void StartStench(GameObject boss)
    {
        _boss = boss;
        Vector3 forward = boss.transform.rotation * Vector3.forward * 2;

        GameObject obj = Instantiate(_stench, boss.transform.position + forward, boss.transform.rotation); // 보스의 정면에 소환
        obj.GetComponent<Stench>();
    }
    public void EndSkill()
    {
        if (_boss == null)
            return;
        _boss.GetComponent<Boss>().State = Boss.BossState.Run;
    }
    private void OnDestroy()
    {
        _isSkilling = false;
    }
}
