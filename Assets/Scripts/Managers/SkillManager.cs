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
    List<SkillScriptable> _skillScriptableLst = new List<SkillScriptable>(); // ��ų ��ũ���ͺ�

    [SerializeField]
    List<GameObject> _skillPrefabLst = new List<GameObject>(); // ��ų ������

    [SerializeField]
    List<GameObject> _useSkillLst = new List<GameObject>(); // ������� ��ų

    [SerializeField] private BasePlayerState _playerState; // �÷��̾� ����

    public bool _isSkilling = false; // ��ų�� ��� �� �ΰ� �Ǵ�

    private WaitForEndOfFrame _frameTime = new WaitForEndOfFrame(); // ����ȭ�� ���� �̸� �Ҵ�
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

    public void StartSkill(Skills skill, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null) // ��ų ���
    {
        if (!CheckCoolTime(skill)) return; // ��Ÿ���� üũ�Ͽ�, ��Ÿ���̶�� ����

        _playerState.PlayerState = BasePlayerState.EPlayerState.Skill; // �÷��̾� ���¸� ��ų ������� ��ȯ

        switch (skill) // ��ų Ȯ��
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
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime); // ĳ���� �ð� ��ŭ ����Ѵ�.

        SoundManager._instance.PlaySkillSound(Skills.Slash, scriptable._durationTime + 0.5f, 1f, 0f, false); // ��ų ���ӽð� + 0.5�ʸ� �ؼ�, �Ҹ��� �� �� ���ӵǰ� ����

        Vector3 forward = playerRot * Vector3.forward;

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Slash], playerPos + forward, playerRot, parent);

        obj.GetComponent<Slash>().Init(scriptable, playerAtk); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkillLst.Add(obj);
    }

    IEnumerator StartSwordForce(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime); // ĳ���� �ð� ��ŭ ����Ѵ�.

        SoundManager._instance.PlaySkillSound(Skills.SwordForce, scriptable._durationTime, 1.25f, 0f, false);

        EndSkill();

        Vector3 forward = playerRot * Vector3.forward * 2;

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.SwordForce], playerPos + forward, playerRot, parent);

        obj.GetComponent<SwordForce>().Init(scriptable, playerAtk); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkillLst.Add(obj);
    }

    IEnumerator StartSpaceCut(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime); // ĳ���� �ð� ��ŭ ����Ѵ�.

        SoundManager._instance.PlaySkillSound(Skills.SpaceCut, scriptable._durationTime, 1f, 0f, false);

        EndSkill();

        Vector3 forward = playerRot * Vector3.forward * 2; // �÷��̾��� Rot�� ���� �̿��Ͽ� �÷��̾��� ���鰪�� ��´�.
        // �ǹ�1 : �� Quaternion�� �տ� ���;� �ұ�? �׳� ��ȸ������ ��ӵǾ� �־?

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.SpaceCut], playerPos + forward, playerRot, parent);

        obj.GetComponent<SpaceCut>().Init(scriptable, playerAtk); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkillLst.Add(obj);
    }

    IEnumerator StartStabing(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;

        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime); // ĳ���� �ð� ��ŭ ����Ѵ�. => �ִϸ��̼��� �����ϰ� ���� ������ ��

        SoundManager._instance.PlaySkillSound(Skills.Stabing, scriptable._durationTime, 1.2f, 0.5f, false);

        //EndSkill();

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Stabing], playerPos, playerRot, parent);

        obj.GetComponent<Stabing>().Init(scriptable, playerAtk); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkillLst.Add(obj);
    }

    IEnumerator StartSweep(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime);

        SoundManager._instance.PlaySkillSound(Skills.Sweep, scriptable._durationTime, 1f, 0f, false);

        //EndSkill();

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Sweep], playerPos + Vector3.up * 1.5f, playerRot, parent);

        obj.GetComponent<Sweep>().Init(scriptable, playerAtk); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkillLst.Add(obj);
    }
    IEnumerator StartChallenge(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // ��ų ��� ��

        SoundManager._instance.PlaySkillSound(Skills.Challenge, scriptable._durationTime, 1f, 0f, false); // ���� ���۰� ��ũ�� ���߱� ���� �̸� ����.

        yield return new WaitForSeconds(scriptable._castTime);

        EndSkill();

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Challenge], playerPos, Quaternion.identity, parent);

        obj.GetComponent<Challenge>().Init(scriptable, playerAtk); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkillLst.Add(obj);
    }

    IEnumerator StartTakedown(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime); // ĳ���� �ð� ��ŭ ����Ѵ�.

        SoundManager._instance.PlaySkillSound(Skills.Takedown, scriptable._durationTime, 1f, 0.2f, false);

        //EndSkill();

        Vector3 forward = playerRot * Vector3.forward;

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Takedown], playerPos + forward, playerRot, parent);

        obj.GetComponent<Takedown>().Init(scriptable, playerAtk); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkillLst.Add(obj);
    }
    IEnumerator StartWindMill(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime);

        SoundManager._instance.PlaySkillSound(Skills.WindMill, scriptable._durationTime, 1f, 0f, true);

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.WindMill], playerPos, Quaternion.identity, parent);

        obj.GetComponent<WindMill>().Init(scriptable, playerAtk, parent); // ���ڿ� �÷��̾� ���ݷ��� �ִ´�.

        _useSkillLst.Add(obj);
    }
    IEnumerator StartBerserk(SkillScriptable scriptable, float playerAtk, Vector3 playerPos, Quaternion playerRot, Transform parent = null)
    {
        PlayerManager._instance.IsSkill = true;
        _isSkilling = true; // ��ų ��� ��

        yield return new WaitForSeconds(scriptable._castTime); // ĳ���� �ð� ��ŭ ����Ѵ�.

        SoundManager._instance.PlaySkillSound(Skills.Berserk, scriptable._durationTime, 1f, 0f, false);

        Vector3 down = Vector3.down * 1.5f;

        GameObject obj = Instantiate(_skillPrefabLst[(int)Skills.Berserk], playerPos + down, Quaternion.identity, parent);

        obj.GetComponent<Berserk>().Init(scriptable);

        _useSkillLst.Add(obj);
    }
    public bool CheckCoolTime(Skills skill) // �÷��̾ ����Ϸ��� ��ų�� ���ڷ� �޾�, �ش� ��ų�� ��ũ���ͺ��� ������ isAble�� �Ǵ��Ͽ� ��ų���¸� ������
    {
        bool state = false;

        state = _skillScriptableLst[(int)skill]._isAble; // �ش� ��ų�� ��ũ���ͺ��� ��ų���¸� �����´�.

        
        //if(!state)
           //Debug.Log(_skillScriptableLst[(int)skill].name + "��(��) �� Ÿ�� ���� �Դϴ�. ���� �ð� : " + _skillScriptableLst[(int)skill]._remainTime);

        return state;
    }
    void StartSkillCoolTime(Skills type) // � ��ų�̵� ���ڷ� �ش� ��ų�� �ް�, �� ��ų�� ��Ÿ���� �ڷ�ƾ���� ��� �����ϸ�, �ڷ�ƾ�� ������ �� ��ų�� ��ũ���ͺ�.isAble�� true�� ��ȯ
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

        float coolTime = scriptable._skillCoolTime; // ��Ÿ���� ���� �ð� ������ �����´�.

        scriptable._remainTime = coolTime - (Time.time - _startCoolTime); // ���� �ð� = ��Ÿ�� - (���� �ð� - ��ų ���ð�)

        while (scriptable._remainTime > 0)
        {
            scriptable._remainTime = coolTime - (Time.time - _startCoolTime);
            yield return _frameTime;
        }

        scriptable._remainTime = scriptable._skillCoolTime; // ���� �ð� �ʱ�ȭ
        scriptable._isAble = true; // ��Ÿ�Ӹ�ŭ �ð��� ������ ��ų ��� �������� ����
    }

    public void EndSkill()
    {
        _isSkilling = false;

        if (_isSkilling || !_playerState.gameObject.activeSelf) return;

        _playerState.PlayerState = BasePlayerState.EPlayerState.Idle;
        // PlayerManager._instance.ChangeState() ��ȯ��Ű��.
    }

    public void EndBattle()
    {
        foreach(GameObject obj in _useSkillLst)
        {
            if(obj != null)
                Destroy(obj);
        }
        StopAllCoroutines(); // ���⿡�� ����ǰ� �ִ� ��� �ڷ�ƾ(��ų ��Ÿ�� Co)�� �����Ų��.
        _useSkillLst.Clear();
        EndSkill();
        ResetSkillValue(); // ��ų �ʱ�ȭ

    }
    void ResetSkillValue() // ��ų ���� �ʱ�ȭ
    {
        foreach (SkillScriptable skill in _skillScriptableLst)
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
