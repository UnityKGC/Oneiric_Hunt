using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager _instance;

    public enum Skills
    {
        WeaponSwap,
        Dodge,

        SwordForce,
        SpaceCut,

        Sweep,
        Challenge,

        WindMill,
        Berserk,
    }

    [SerializeField]
    List<SkillScriptable> _skills = new List<SkillScriptable>();

    [SerializeField]
    List<GameObject> _skillPrefabs = new List<GameObject>();

    [SerializeField]
    List<GameObject> _useSkill = new List<GameObject>();

    public bool _isSkilling = false; // ��ų�� ��� �� �ΰ�

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

    public void StartSkill(Skills skill, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        if (!CheckCoolTime(skill)) return;
        switch(skill)
        {
            case Skills.WeaponSwap:
                StartCoroutine(StartWeaponSwap(_skills[(int)skill], parent));
                break;

            case Skills.Dodge:
                StartCoroutine(StartDodge(_skills[(int)skill], parent));
                break;

            case Skills.SwordForce:
                StartCoroutine(StartSwordForce(_skills[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.SpaceCut:
                StartCoroutine(StartSpaceCut(_skills[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.Sweep:
                StartCoroutine(StartSweep(_skills[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.Challenge:
                StartCoroutine(StartChallenge(_skills[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.WindMill:
                StartCoroutine(StartWindMill(_skills[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.Berserk:
                StartCoroutine(StartBerserk(_skills[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;
        }
        StartSkillCoolTime(skill);
    }
    
    IEnumerator StartWeaponSwap(SkillScriptable scriptable, Transform parent)
    {
        _isSkilling = true;

        yield return new WaitForSeconds(scriptable._castTime);

        _isSkilling = false;

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.WeaponSwap], parent);

        obj.GetComponent<WeaponSwap>().Init(scriptable);

        _useSkill.Add(obj);

        yield return null;
    }
    IEnumerator StartDodge(SkillScriptable scriptable, Transform parent)
    {
        _isSkilling = true;

        yield return new WaitForSeconds(scriptable._castTime);

        _isSkilling = false;

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.Dodge], parent);

        obj.GetComponent<Dodge>().Init(scriptable);

        _useSkill.Add(obj);

        yield return null;
    }
    public void StartCut(float playerAtk, Vector3 playerPos, Quaternion playerRot)
    {

    }

    IEnumerator StartSwordForce(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime); // ĳ���� �ð� ��ŭ ����Ѵ�.

        _isSkilling = false; // ��ų ��� ��

        Vector3 forward = playerRot * Vector3.forward * 2;

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.SwordForce], playerPos + forward, playerRot, parent);

        obj.GetComponent<SwordForce>().Init(scriptable, playerAtk); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkill.Add(obj);
    }

    IEnumerator StartSpaceCut(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime); // ĳ���� �ð� ��ŭ ����Ѵ�.

        _isSkilling = false; // ��ų ��� ��

        Vector3 forward = playerRot * Vector3.forward * 2; // �÷��̾��� Rot�� ���� �̿��Ͽ� �÷��̾��� ���鰪�� ��´�.
        // �ǹ�1 : �� Quaternion�� �տ� ���;� �ұ�? �׳� ��ȸ������ ��ӵǾ� �־?

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.SpaceCut], playerPos + forward, playerRot, parent);

        obj.GetComponent<SpaceCut>().Init(scriptable, playerAtk); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkill.Add(obj);
    }

    IEnumerator StartSweep(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime);

        _isSkilling = false; // ��ų ��� ��

        Vector3 forward = playerRot * Vector3.forward * 2;

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.Sweep], playerPos + forward, playerRot, parent);

        obj.GetComponent<Sweep>().Init(scriptable, playerAtk); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkill.Add(obj);
    }
    IEnumerator StartChallenge(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime);

        _isSkilling = false; // ��ų ��� ��

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.Challenge], playerPos, Quaternion.identity, parent);

        obj.GetComponent<Challenge>().Init(scriptable, playerAtk); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkill.Add(obj);
    }

    IEnumerator StartWindMill(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime);

        _isSkilling = false; // ��ų ��� ��

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.WindMill], playerPos, Quaternion.identity, parent);

        obj.GetComponent<WindMill>().Init(scriptable, playerAtk, parent); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkill.Add(obj);
    }
    IEnumerator StartBerserk(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime); // ĳ���� �ð� ��ŭ ����Ѵ�.

        _isSkilling = false; // ��ų ��� ��

        Vector3 down = Vector3.down * 1.5f;

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.Berserk], playerPos + down, Quaternion.identity, parent);

        obj.GetComponent<Berserk>().Init(scriptable);

        _useSkill.Add(obj);
    }
    public bool CheckCoolTime(Skills skill) // �÷��̾ ����Ϸ��� ��ų�� ���ڷ� �޾�, �ش� ��ų�� ��ũ���ͺ��� ������ isAble�� �Ǵ��Ͽ� ��ų���¸� ������
    {
        bool state = false;

        state = _skills[(int)skill]._isAble; // �ش� ��ų�� ��ũ���ͺ��� ��ų���¸� �����´�.

        if(!state)
            Debug.Log(_skills[(int)skill].name + "��(��) �� Ÿ�� ���� �Դϴ�. ���� �ð� : " + _skills[(int)skill]._remainTime);

        return state;
    }
    void StartSkillCoolTime(Skills type) // � ��ų�̵� ���ڷ� �ش� ��ų�� �ް�, �� ��ų�� ��Ÿ���� �ڷ�ƾ���� ��� �����ϸ�, �ڷ�ƾ�� ������ �� ��ų�� ��ũ���ͺ�.isAble�� true�� ��ȯ
    {
        SkillScriptable scriptable = null;

        scriptable = _skills[(int)type];

        if(scriptable != null)
        {
            UIManager._instacne.SetSkillUI(scriptable, type);
            StartCoroutine(StartCoolTimeCo(scriptable));
        }
            
    }
    
    IEnumerator StartCoolTimeCo(SkillScriptable scriptable)
    {
        float _startCoolTime = Time.time;

        float coolTime = scriptable._skillCoolTime; // ��Ÿ���� ���� �ð� ������ �����´�.

        scriptable._remainTime = coolTime - (Time.time - _startCoolTime); // ���� �ð� = ��Ÿ�� - (���� �ð� - ��ų ���ð�)

        while (scriptable._remainTime > 0)
        {
            scriptable._remainTime = coolTime - (Time.time - _startCoolTime);
            yield return new WaitForEndOfFrame();
        }

        scriptable._remainTime = scriptable._skillCoolTime; // ���� �ð� �ʱ�ȭ
        scriptable._isAble = true; // ��Ÿ�Ӹ�ŭ �ð��� ������ ��ų ��� �������� ����
    }

    public void EndBattle()
    {
        foreach(GameObject obj in _useSkill)
        {
            if(obj != null)
                Destroy(obj);
        }
        _useSkill.Clear();
    }
    void ResetSkillValue() // ��ų ���� �ʱ�ȭ
    {
        foreach (SkillScriptable skill in _skills)
        {
            skill._remainTime = skill._skillCoolTime; // ���� �ð��� ��Ÿ������ �ʱ�ȭ
            skill._isAble = true;
        }
    }
    private void OnDestroy()
    {
        ResetSkillValue();
    }
}
