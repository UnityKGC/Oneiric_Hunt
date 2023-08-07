using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    SkillScriptable _scriptable;

    PlayerSkill _playerSkill;

    public void Init(SkillScriptable scriptable)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _playerSkill = GetComponentInParent<PlayerSkill>();
        if (_playerSkill != null)
        {
            switch (_playerSkill.Weapon)
            {
                case PlayerSkill.WeaponType.Sword:
                    _playerSkill.Weapon = PlayerSkill.WeaponType.Spear;
                    break;
                case PlayerSkill.WeaponType.Spear:
                    _playerSkill.Weapon = PlayerSkill.WeaponType.Axe;
                    break;
                case PlayerSkill.WeaponType.Axe:
                    _playerSkill.Weapon = PlayerSkill.WeaponType.Sword;
                    break;
            }
        }
        Destroy(gameObject);
    }
}
