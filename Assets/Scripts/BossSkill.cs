using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Boss;

public class BossSkill : MonoBehaviour
{
    public enum SkillType
    {
        All,
        Near,
        Far
    }
    
    public enum AllSkill
    {
        Despair,
        Guardian,
        Anger,
        Overdose,
        Rush,
        Delirium,
        Stench,
    }

    public enum NearSkill
    {
        Despair,
        Guardian,
        Overdose,
        Delirium,
        Stench,
    }
    public enum FarSkill
    {
        Anger,
        Overdose,
        Rush,
        Stench,
    }


    private Animator _anim;
    private BossStat _bossStat;


    //private bool _isSkill = false; // 스킬 사용 중인가 확인
    void Start()
    {
        _bossStat = GetComponent<BossStat>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void Despair() // 절망, 플레이어의 공격력과 방어력을 감소
    {
        if (BossSkillManager._instance._isSkilling) return; // 스킬 사용중 이라면 리턴

        BossSkillManager._instance._isSkilling  = true; // 스킬사용 시작
        _anim.CrossFade("Despair",0.1f);
        StartCoroutine(DespairCo());
    }
    public void Guardian() // 수호, 주변 몬스터의 방어력과 체력을 올려준다.
    {
        if (BossSkillManager._instance._isSkilling) return; // 스킬 사용중 이라면 리턴

        BossSkillManager._instance._isSkilling  = true; // 스킬사용 시작
        _anim.CrossFade("Guardian",0.1f);
        StartCoroutine(GuardianCo());
    }
    public void Anger() // 분노, 플레이어에게 점프한 후, 그 주변을 내려치면서 난타
    {
        if (BossSkillManager._instance._isSkilling) return; // 스킬 사용중 이라면 리턴

        BossSkillManager._instance._isSkilling  = true; // 스킬사용 시작
        _anim.CrossFade("Anger_1", 0.1f);
        StartCoroutine(AngerCo());
    }

    public void Overdose() // 과다복용, 약을 투여해, 일시적으로 강력해지지만, 체력이 계속 감소된다.
    {
        if (BossSkillManager._instance._isSkilling) return; // 스킬 사용중 이라면 리턴

        BossSkillManager._instance._isSkilling  = true; // 스킬사용 시작
        _anim.CrossFade("Overdose", 0.1f);
        StartCoroutine(OverdoseCo());
    }
    public void Rush() // 돌진, 플레이어의 방향으로 미친듯이 돌진한다.
    {
        if (BossSkillManager._instance._isSkilling) return; // 스킬 사용중 이라면 리턴

        BossSkillManager._instance._isSkilling  = true; // 스킬사용 시작
        _anim.CrossFade("Rush", 0.1f);
        StartCoroutine(RushCo());
    }
    public void Delirium() // 정신착란, 플레이어의 이동이 반전된다.
    {
        if (BossSkillManager._instance._isSkilling) return; // 스킬 사용중 이라면 리턴

        BossSkillManager._instance._isSkilling  = true; // 스킬사용 시작
        _anim.CrossFade("Delirium", 0.1f);
        StartCoroutine(DeliriumCo());
    }
    public void Stench() // 악취, 지독한 냄새를 뿜는 버섯을 소환한 후, 주변의 플레이어에게 여러 디버프를 적용시킨다 (독,속도 감소)
    {
        if (BossSkillManager._instance._isSkilling) return; // 스킬 사용중 이라면 리턴

        BossSkillManager._instance._isSkilling  = true; // 스킬사용 시작
        _anim.CrossFade("Stench", 0.1f);
        StartCoroutine(StenchCo());
    }
    IEnumerator DespairCo()
    {
        BossSkillManager._instance.StartDespair(gameObject);

        yield return null;
    }
    IEnumerator GuardianCo()
    {
        BossSkillManager._instance.StartGuardian(gameObject);

        yield return null;
    }
    IEnumerator AngerCo()
    {
        // 자기자신을 플레이어 위치로 점프한 후(포물선) 난타 사용
        float dmg = Random.Range(_bossStat.MinAtk, _bossStat.MaxAtk);
        BossSkillManager._instance.StartAnger(dmg, gameObject);

        //yield return new WaitForSeconds(5f); // 1.5 + 3.0 (점프후 착지 + 난타시간)

        //BossSkillManager._instance._isSkilling = _isSkill = false; // 수호 스킬 사용 끝이므로 초기화
        yield return null;
    }
    IEnumerator OverdoseCo()
    {
        yield return new WaitForSeconds(4f); // 캐스팅 시간

        BossSkillManager._instance.StartOverdose(gameObject);

        yield return null;
    }
    IEnumerator RushCo()
    {
        float dmg = Random.Range(_bossStat.MinAtk, _bossStat.MaxAtk);
        BossSkillManager._instance.StartRush(dmg, gameObject);
        yield return null;
    }
    IEnumerator DeliriumCo()
    {
        BossSkillManager._instance.StartDelirium(gameObject);

        yield return null;
    }
    IEnumerator StenchCo()
    {
        // 캐스팅 시간 대기 후 스킬 사용하게 수정하기

        yield return new WaitForSeconds(3f);

        BossSkillManager._instance.StartStench(gameObject);

    }
}
