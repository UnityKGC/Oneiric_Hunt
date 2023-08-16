using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    SkillScriptable _scriptable;

    //PlayerSkill _playerSkill;
    Player_DB_Skill _playerSkill;
    Player_DB_Anim _anim;
    public void Init(SkillScriptable scriptable)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        //_playerSkill = GetComponentInParent<PlayerSkill>();
        _playerSkill = GetComponentInParent<Player_DB_Skill>();
        _anim = GetComponentInParent<Player_DB_Anim>();
        if (_playerSkill != null)
        {
            switch (_playerSkill.Weapon)
            {
                case WeaponType.Sword:
                    _playerSkill.Weapon = WeaponType.Spear;
                    break;
                case WeaponType.Spear:
                    _playerSkill.Weapon = WeaponType.Axe;
                    break;
                case WeaponType.Axe:
                    _playerSkill.Weapon = WeaponType.Sword;
                    break;
            }
            UIManager._instacne.SetWeapon(_playerSkill.Weapon);
            _anim.ChangeWeapon(_playerSkill.Weapon);
        }
        Destroy(gameObject);
    }
}
