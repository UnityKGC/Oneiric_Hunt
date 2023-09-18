using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Anim : BasePlayerAnim
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

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public override void CrossFade(BasePlayerState.EPlayerState state, Skills type = Skills.None) // ���� ������ ����?
    {
        base.CrossFade(state, type); // �⺻���� Anim���� Ȯ��

        if (state == BasePlayerState.EPlayerState.Skill)
        {
            StartSkillAnim(type);
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

    void StartSkillAnim(Skills type) // ����, ��ųŸ���� ���ڷ� �޴´�.
    {
        if (!SkillManager._instance.CheckCoolTime(type)) return; // ����Ϸ��� ��ų�� ���� ������� ���ϸ� ����
        
        switch (type)
        {
            case Skills.Dodge: // Dodge��� Run���� �����Ų��.
                _anim.CrossFade("Dodge", 0.1f);
                break;

            case Skills.WeaponSwap:
                _anim.CrossFade("WeaponSwap", 0.1f);
                break;

            case Skills.Slash:
                _anim.CrossFade("Slash", 0.1f);
                break;

            case Skills.SwordForce:
                _anim.CrossFade("SwordForce", 0.1f);
                break;

            case Skills.SpaceCut:
                _anim.CrossFade("SpaceCut", 0.1f);
                break;

            case Skills.Stabing:
                _anim.CrossFade("Stabing", 0.1f);
                break;

            case Skills.Sweep:
                _anim.CrossFade("Sweep", 0.1f);
                break;

            case Skills.Challenge:
                _anim.CrossFade("Challenge", 0.1f);
                break;

            case Skills.Takedown:
                _anim.CrossFade("Takedown", 0.1f);
                break;

            case Skills.WindMill:
                _anim.CrossFade("WindMill", 0.1f);
                break;

            case Skills.Berserk:
                _anim.CrossFade("Berserk", 0.1f);
                break;
        }
    }
    // ��ų�� ����Ǹ� �ٽ� Idle�̳� Move�� ��ȯ���Ѿ� �ϴµ�, �װ� ���� ���� �Ǻ�����?
}
