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


    //private bool _isSkill = false; // ��ų ��� ���ΰ� Ȯ��
    void Start()
    {
        _bossStat = GetComponent<BossStat>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void Despair() // ����, �÷��̾��� ���ݷ°� ������ ����
    {
        if (BossSkillManager._instance._isSkilling) return; // ��ų ����� �̶�� ����

        BossSkillManager._instance._isSkilling  = true; // ��ų��� ����
        _anim.CrossFade("Despair",0.1f);
        StartCoroutine(DespairCo());
    }
    public void Guardian() // ��ȣ, �ֺ� ������ ���°� ü���� �÷��ش�.
    {
        if (BossSkillManager._instance._isSkilling) return; // ��ų ����� �̶�� ����

        BossSkillManager._instance._isSkilling  = true; // ��ų��� ����
        _anim.CrossFade("Guardian",0.1f);
        StartCoroutine(GuardianCo());
    }
    public void Anger() // �г�, �÷��̾�� ������ ��, �� �ֺ��� ����ġ�鼭 ��Ÿ
    {
        if (BossSkillManager._instance._isSkilling) return; // ��ų ����� �̶�� ����

        BossSkillManager._instance._isSkilling  = true; // ��ų��� ����
        _anim.CrossFade("Anger_1", 0.1f);
        StartCoroutine(AngerCo());
    }

    public void Overdose() // ���ٺ���, ���� ������, �Ͻ������� ������������, ü���� ��� ���ҵȴ�.
    {
        if (BossSkillManager._instance._isSkilling) return; // ��ų ����� �̶�� ����

        BossSkillManager._instance._isSkilling  = true; // ��ų��� ����
        _anim.CrossFade("Overdose", 0.1f);
        StartCoroutine(OverdoseCo());
    }
    public void Rush() // ����, �÷��̾��� �������� ��ģ���� �����Ѵ�.
    {
        if (BossSkillManager._instance._isSkilling) return; // ��ų ����� �̶�� ����

        BossSkillManager._instance._isSkilling  = true; // ��ų��� ����
        _anim.CrossFade("Rush", 0.1f);
        StartCoroutine(RushCo());
    }
    public void Delirium() // ��������, �÷��̾��� �̵��� �����ȴ�.
    {
        if (BossSkillManager._instance._isSkilling) return; // ��ų ����� �̶�� ����

        BossSkillManager._instance._isSkilling  = true; // ��ų��� ����
        _anim.CrossFade("Delirium", 0.1f);
        StartCoroutine(DeliriumCo());
    }
    public void Stench() // ����, ������ ������ �մ� ������ ��ȯ�� ��, �ֺ��� �÷��̾�� ���� ������� �����Ų�� (��,�ӵ� ����)
    {
        if (BossSkillManager._instance._isSkilling) return; // ��ų ����� �̶�� ����

        BossSkillManager._instance._isSkilling  = true; // ��ų��� ����
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
        // �ڱ��ڽ��� �÷��̾� ��ġ�� ������ ��(������) ��Ÿ ���
        float dmg = Random.Range(_bossStat.MinAtk, _bossStat.MaxAtk);
        BossSkillManager._instance.StartAnger(dmg, gameObject);

        //yield return new WaitForSeconds(5f); // 1.5 + 3.0 (������ ���� + ��Ÿ�ð�)

        //BossSkillManager._instance._isSkilling = _isSkill = false; // ��ȣ ��ų ��� ���̹Ƿ� �ʱ�ȭ
        yield return null;
    }
    IEnumerator OverdoseCo()
    {
        yield return new WaitForSeconds(4f); // ĳ���� �ð�

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
        // ĳ���� �ð� ��� �� ��ų ����ϰ� �����ϱ�

        yield return new WaitForSeconds(3f);

        BossSkillManager._instance.StartStench(gameObject);

    }
}
