using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Anim : MonoBehaviour
{
    enum WeaponAnim
    {
        None = -1,
        Sword,
        Spear,
        Axe,
    }
    
    [SerializeField] List<RuntimeAnimatorController> _animLst;
    [SerializeField] List<GameObject> _weaponLst;
    private Animator _anim;

    Player_DB_State.DB_State _animState;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void CrossFade(Player_DB_State.DB_State state, SkillManager.Skills type = SkillManager.Skills.None) // ���� ������ ����?
    {
        if (_animState == state)
            return;

        _animState = state;

        switch (_animState)
        {
            case Player_DB_State.DB_State.Idle:
                _anim.CrossFade("Idle", 0.1f);
                break;
            case Player_DB_State.DB_State.Run:
                _anim.CrossFade("Run", 0.1f);
                break;
            case Player_DB_State.DB_State.Attack_1:
                _anim.CrossFade("Attack_1", 0.1f);
                break;
            case Player_DB_State.DB_State.Attack_2:
                _anim.CrossFade("Attack_2", 0.1f);
                break;
            case Player_DB_State.DB_State.Attack_3:
                _anim.CrossFade("Attack_3", 0.1f);
                break;
            case Player_DB_State.DB_State.Attack_4:
                _anim.CrossFade("Attack_4", 0.1f);
                break;
            case Player_DB_State.DB_State.Skill:
                StartSkillAnim(type);
                break;
            case Player_DB_State.DB_State.Die:

                break;
        }
    }
    public void ChangeWeapon(WeaponType weapon)
    {
        for(int i = 0; i < (int)WeaponType.Max; i++)
        {
            if(i == (int)weapon)
            {
                _weaponLst[i].SetActive(true);
                _anim.runtimeAnimatorController = _animLst[i];
            }
            else
            {
                _weaponLst[i].SetActive(false);
            } 
        }
    }
    void StartSkillAnim(SkillManager.Skills type) // ����, ��ųŸ���� ���ڷ� �޴´�.
    {
        if (!SkillManager._instance.CheckCoolTime(type)) return; // ����Ϸ��� ��ų�� ���� ������� ���ϸ� ����
        
        switch (type)
        {
            case SkillManager.Skills.Dodge: // Dodge��� Run���� �����Ų��.
                _anim.CrossFade("Dodge", 0.1f);
                break;

            case SkillManager.Skills.WeaponSwap:
                _anim.CrossFade("WeaponSwap", 0.1f);
                break;

            case SkillManager.Skills.Slash:
                _anim.CrossFade("Slash", 0.1f);
                break;

            case SkillManager.Skills.SwordForce:
                _anim.CrossFade("SwordForce", 0.1f);
                break;

            case SkillManager.Skills.SpaceCut:
                _anim.CrossFade("SpaceCut", 0.1f);
                break;

            case SkillManager.Skills.Stabing:
                _anim.CrossFade("Stabing", 0.1f);
                break;

            case SkillManager.Skills.Sweep:
                _anim.CrossFade("Sweep", 0.1f);
                break;

            case SkillManager.Skills.Challenge:
                _anim.CrossFade("Challenge", 0.1f);
                break;

            case SkillManager.Skills.Takedown:
                _anim.CrossFade("Takedown", 0.1f);
                break;

            case SkillManager.Skills.WindMill:
                _anim.CrossFade("WindMill", 0.1f);
                break;

            case SkillManager.Skills.Berserk:
                _anim.CrossFade("Berserk", 0.1f);
                break;
        }
    }
    // ��ų�� ����Ǹ� �ٽ� Idle�̳� Move�� ��ȯ���Ѿ� �ϴµ�, �װ� ���� ���� �Ǻ�����?
}
