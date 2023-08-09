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

    public bool _isSkilling = false; // 스킬을 사용 중 인가

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
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime); // 캐스팅 시간 만큼 대기한다.

        _isSkilling = false; // 스킬 사용 끝

        Vector3 forward = playerRot * Vector3.forward * 2;

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.SwordForce], playerPos + forward, playerRot, parent);

        obj.GetComponent<SwordForce>().Init(scriptable, playerAtk); // 인자에 플레이어 공격력을 넣는다.

        _useSkill.Add(obj);
    }

    IEnumerator StartSpaceCut(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime); // 캐스팅 시간 만큼 대기한다.

        _isSkilling = false; // 스킬 사용 끝

        Vector3 forward = playerRot * Vector3.forward * 2; // 플레이어의 Rot의 값을 이용하여 플레이어의 정면값을 얻는다.
        // 의문1 : 왜 Quaternion이 앞에 나와야 할까? 그냥 사회적으로 약속되어 있어서?

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.SpaceCut], playerPos + forward, playerRot, parent);

        obj.GetComponent<SpaceCut>().Init(scriptable, playerAtk); // 인자에 플레이어 공격력을 넣는다.

        _useSkill.Add(obj);
    }

    IEnumerator StartSweep(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime);

        _isSkilling = false; // 스킬 사용 끝

        Vector3 forward = playerRot * Vector3.forward * 2;

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.Sweep], playerPos + forward, playerRot, parent);

        obj.GetComponent<Sweep>().Init(scriptable, playerAtk); // 인자에 플레이어 공격력을 넣는다.

        _useSkill.Add(obj);
    }
    IEnumerator StartChallenge(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime);

        _isSkilling = false; // 스킬 사용 끝

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.Challenge], playerPos, Quaternion.identity, parent);

        obj.GetComponent<Challenge>().Init(scriptable, playerAtk); // 인자에 플레이어 공격력을 넣는다.

        _useSkill.Add(obj);
    }

    IEnumerator StartWindMill(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime);

        _isSkilling = false; // 스킬 사용 끝

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.WindMill], playerPos, Quaternion.identity, parent);

        obj.GetComponent<WindMill>().Init(scriptable, playerAtk, parent); // 인자에 플레이어 공격력을 넣는다.

        _useSkill.Add(obj);
    }
    IEnumerator StartBerserk(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime); // 캐스팅 시간 만큼 대기한다.

        _isSkilling = false; // 스킬 사용 끝

        Vector3 down = Vector3.down * 1.5f;

        GameObject obj = Instantiate(_skillPrefabs[(int)Skills.Berserk], playerPos + down, Quaternion.identity, parent);

        obj.GetComponent<Berserk>().Init(scriptable);

        _useSkill.Add(obj);
    }
    public bool CheckCoolTime(Skills skill) // 플레이어가 사용하려는 스킬을 인자로 받아, 해당 스킬의 스크립터블을 가져와 isAble를 판단하여 스킬상태를 리턴함
    {
        bool state = false;

        state = _skills[(int)skill]._isAble; // 해달 스킬의 스크립터블의 스킬상태를 가져온다.

        if(!state)
            Debug.Log(_skills[(int)skill].name + "은(는) 쿨 타임 상태 입니다. 남은 시간 : " + _skills[(int)skill]._remainTime);

        return state;
    }
    void StartSkillCoolTime(Skills type) // 어떤 스킬이든 인자로 해당 스킬을 받고, 그 스킬의 쿨타임을 코루틴으로 재기 시작하며, 코루틴이 끝나면 그 스킬의 스크립터블.isAble을 true로 전환
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

        float coolTime = scriptable._skillCoolTime; // 쿨타임을 남은 시간 변수로 가져온다.

        scriptable._remainTime = coolTime - (Time.time - _startCoolTime); // 남은 시간 = 쿨타임 - (현재 시간 - 스킬 사용시간)

        while (scriptable._remainTime > 0)
        {
            scriptable._remainTime = coolTime - (Time.time - _startCoolTime);
            yield return new WaitForEndOfFrame();
        }

        scriptable._remainTime = scriptable._skillCoolTime; // 남은 시간 초기화
        scriptable._isAble = true; // 쿨타임만큼 시간이 지나면 스킬 사용 가능으로 변경
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
    void ResetSkillValue() // 스킬 변수 초기화
    {
        foreach (SkillScriptable skill in _skills)
        {
            skill._remainTime = skill._skillCoolTime; // 남은 시간을 쿨타임으로 초기화
            skill._isAble = true;
        }
    }
    private void OnDestroy()
    {
        ResetSkillValue();
    }
}
