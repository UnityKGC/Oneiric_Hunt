using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skills
{
    None = -1,

    WeaponSwap,
    Dodge,

    Slash,
    SwordForce,
    SpaceCut,

    Stabing,
    Sweep,
    Challenge,

    Takedown,
    WindMill,
    Berserk,
}

public class SkillManager : MonoBehaviour
{
    public static SkillManager _instance;

    [SerializeField]
    List<SkillScriptable> _skillScriptableLst = new List<SkillScriptable>(); // 스킬 스크립터블

    [SerializeField]
    List<GameObject> _skillPrefabLst = new List<GameObject>(); // 스킬 프리펩

    [SerializeField]
    List<GameObject> _useSkillLst = new List<GameObject>(); // 사용중인 스킬

    [SerializeField] private BasePlayerState _playerState; // 플레이어 상태

    public bool _isSkilling = false; // 스킬을 사용 중 인가 판단

    private WaitForEndOfFrame _frameTime = new WaitForEndOfFrame(); // 최적화를 위해 미리 할당
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

    public void StartSkill(Skills skill, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null) // 스킬 사용
    {
        if (!CheckCoolTime(skill)) return; // 쿨타임을 체크하여, 쿨타임이라면 리턴

        _playerState.PlayerState = BasePlayerState.EPlayerState.Skill; // 플레이어 상태를 스킬 사용으로 변환

        switch (skill) // 스킬 확인
        {
            case Skills.WeaponSwap:
                StartCoroutine(StartWeaponSwap(_skillScriptableLst[(int)skill], parent));
                break;

            case Skills.Dodge:
                StartCoroutine(StartDodge(_skillScriptableLst[(int)skill], playerPos, playerRot, parent));
                break;

            case Skills.Slash:
                StartCoroutine(StartSlash(_skillScriptableLst[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.SwordForce:
                StartCoroutine(StartSwordForce(_skillScriptableLst[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.SpaceCut:
                StartCoroutine(StartSpaceCut(_skillScriptableLst[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.Stabing:
                StartCoroutine(StartStabing(_skillScriptableLst[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.Sweep:
                StartCoroutine(StartSweep(_skillScriptableLst[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.Challenge:
                StartCoroutine(StartChallenge(_skillScriptableLst[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.Takedown:
                StartCoroutine(StartTakedown(_skillScriptableLst[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.WindMill:
                StartCoroutine(StartWindMill(_skillScriptableLst[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;

            case Skills.Berserk:
                StartCoroutine(StartBerserk(_skillScriptableLst[(int)skill], playerAtk, playerPos, playerRot, parent));
                break;
        }
        StartSkillCoolTime(skill);
    }
    
    IEnumerator StartWeaponSwap(SkillScriptable scriptable, Transform parent)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true;

        yield return new WaitForSeconds(scriptable._castTime);

        SoundManager._instance.PlaySkillSound(Skills.WeaponSwap, scriptable._durationTime, 1f, 0f, false);

        EndSkill();

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.WeaponSwap], parent);

        obj.GetComponent<WeaponSwap>().Init(scriptable);

        _useSkillLst.Add(obj);

        yield return null;
    }
    IEnumerator StartDodge(SkillScriptable scriptable, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true;

        yield return new WaitForSeconds(scriptable._castTime);

        SoundManager._instance.PlaySkillSound(Skills.Dodge, scriptable._durationTime, 1f, 0f, false);

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Dodge], parent);

        obj.GetComponent<Dodge>().Init(scriptable, playerPos, playerRot);

        _useSkillLst.Add(obj);

        yield return null;
    }
    IEnumerator StartSlash(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime); // 캐스팅 시간 만큼 대기한다.

        SoundManager._instance.PlaySkillSound(Skills.Slash, scriptable._durationTime + 0.5f, 1f, 0f, false); // 스킬 지속시간 + 0.5초를 해서, 소리를 좀 더 지속되게 만듦

        Vector3 forward = playerRot * Vector3.forward;

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Slash], playerPos + forward, playerRot, parent);

        obj.GetComponent<Slash>().Init(scriptable, playerAtk); // 인자에 플레이어 공격력을 넣는다.

        _useSkillLst.Add(obj);
    }

    IEnumerator StartSwordForce(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime); // 캐스팅 시간 만큼 대기한다.

        SoundManager._instance.PlaySkillSound(Skills.SwordForce, scriptable._durationTime, 1.25f, 0f, false);

        EndSkill();

        Vector3 forward = playerRot * Vector3.forward * 2;

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.SwordForce], playerPos + forward, playerRot, parent);

        obj.GetComponent<SwordForce>().Init(scriptable, playerAtk); // 인자에 플레이어 공격력을 넣는다.

        _useSkillLst.Add(obj);
    }

    IEnumerator StartSpaceCut(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime); // 캐스팅 시간 만큼 대기한다.

        SoundManager._instance.PlaySkillSound(Skills.SpaceCut, scriptable._durationTime, 1f, 0f, false);

        EndSkill();

        Vector3 forward = playerRot * Vector3.forward * 2; // 플레이어의 Rot의 값을 이용하여 플레이어의 정면값을 얻는다.
        // 의문1 : 왜 Quaternion이 앞에 나와야 할까? 그냥 사회적으로 약속되어 있어서?

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.SpaceCut], playerPos + forward, playerRot, parent);

        obj.GetComponent<SpaceCut>().Init(scriptable, playerAtk); // 인자에 플레이어 공격력을 넣는다.

        _useSkillLst.Add(obj);
    }

    IEnumerator StartStabing(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;

        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime); // 캐스팅 시간 만큼 대기한다. => 애니메이션이 적절하게 적을 공격할 때

        SoundManager._instance.PlaySkillSound(Skills.Stabing, scriptable._durationTime, 1.2f, 0.5f, false);

        //EndSkill();

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Stabing], playerPos, playerRot, parent);

        obj.GetComponent<Stabing>().Init(scriptable, playerAtk); // 인자에 플레이어 공격력을 넣는다.

        _useSkillLst.Add(obj);
    }

    IEnumerator StartSweep(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime);

        SoundManager._instance.PlaySkillSound(Skills.Sweep, scriptable._durationTime, 1f, 0f, false);

        //EndSkill();

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Sweep], playerPos + Vector3.up * 1.5f, playerRot, parent);

        obj.GetComponent<Sweep>().Init(scriptable, playerAtk); // 인자에 플레이어 공격력을 넣는다.

        _useSkillLst.Add(obj);
    }
    IEnumerator StartChallenge(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // 스킬 사용 중

        SoundManager._instance.PlaySkillSound(Skills.Challenge, scriptable._durationTime, 1f, 0f, false); // 선행 동작과 싱크를 맞추기 위해 미리 실행.

        yield return new WaitForSeconds(scriptable._castTime);

        EndSkill();

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Challenge], playerPos, Quaternion.identity, parent);

        obj.GetComponent<Challenge>().Init(scriptable, playerAtk); // 인자에 플레이어 공격력을 넣는다.

        _useSkillLst.Add(obj);
    }

    IEnumerator StartTakedown(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime); // 캐스팅 시간 만큼 대기한다.

        SoundManager._instance.PlaySkillSound(Skills.Takedown, scriptable._durationTime, 1f, 0.2f, false);

        //EndSkill();

        Vector3 forward = playerRot * Vector3.forward;

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Takedown], playerPos + forward, playerRot, parent);

        obj.GetComponent<Takedown>().Init(scriptable, playerAtk); // 인자에 플레이어 공격력을 넣는다.

        _useSkillLst.Add(obj);
    }
    IEnumerator StartWindMill(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime);

        SoundManager._instance.PlaySkillSound(Skills.WindMill, scriptable._durationTime, 1f, 0f, true);

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.WindMill], playerPos, Quaternion.identity, parent);

        obj.GetComponent<WindMill>().Init(scriptable, playerAtk, parent); // 인자에 플레이어 공격력을 넣는다.

        _useSkillLst.Add(obj);
    }
    IEnumerator StartBerserk(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // 스킬 사용 중

        yield return new WaitForSeconds(scriptable._castTime); // 캐스팅 시간 만큼 대기한다.

        SoundManager._instance.PlaySkillSound(Skills.Berserk, scriptable._durationTime, 1f, 0f, false);

        Vector3 down = Vector3.down * 1.5f;

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Berserk], playerPos + down, Quaternion.identity, parent);

        obj.GetComponent<Berserk>().Init(scriptable);

        _useSkillLst.Add(obj);
    }
    public bool CheckCoolTime(Skills skill) // 플레이어가 사용하려는 스킬을 인자로 받아, 해당 스킬의 스크립터블을 가져와 isAble를 판단하여 스킬상태를 리턴함
    {
        bool state = false;

        state = _skillScriptableLst[(int)skill]._isAble; // 해달 스킬의 스크립터블의 스킬상태를 가져온다.

        
        //if(!state)
           //Debug.Log(_skillScriptableLst[(int)skill].name + "은(는) 쿨 타임 상태 입니다. 남은 시간 : " + _skillScriptableLst[(int)skill]._remainTime);

        return state;
    }
    void StartSkillCoolTime(Skills type) // 어떤 스킬이든 인자로 해당 스킬을 받고, 그 스킬의 쿨타임을 코루틴으로 재기 시작하며, 코루틴이 끝나면 그 스킬의 스크립터블.isAble을 true로 전환
    {
        SkillScriptable scriptable = null;

        scriptable = _skillScriptableLst[(int)type];

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
            yield return _frameTime;
        }

        scriptable._remainTime = scriptable._skillCoolTime; // 남은 시간 초기화
        scriptable._isAble = true; // 쿨타임만큼 시간이 지나면 스킬 사용 가능으로 변경
    }

    public void EndSkill()
    {
        _isSkilling = false;

        if (_isSkilling || !_playerState.gameObject.activeSelf) return;

        _playerState.PlayerState = BasePlayerState.EPlayerState.Idle;
        // PlayerManager._instance.ChangeState() 변환시키기.
    }

    public void EndBattle()
    {
        foreach(GameObject obj in _useSkillLst)
        {
            if(obj != null)
                Destroy(obj);
        }
        StopAllCoroutines(); // 여기에서 실행되고 있는 모든 코루틴(스킬 쿨타임 Co)를 종료시킨다.
        _useSkillLst.Clear();
        EndSkill();
        ResetSkillValue(); // 스킬 초기화

    }
    void ResetSkillValue() // 스킬 변수 초기화
    {
        foreach (SkillScriptable skill in _skillScriptableLst)
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
