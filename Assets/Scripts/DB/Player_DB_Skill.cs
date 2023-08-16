using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Skill : MonoBehaviour
{
    // WeaponSwap스킬에는 ChagneWeapon이라는 함수를 호출해야한다
    // 이 함수는 플레이어가 지니고 있는 무기를 확인하고, 교체되는 무기로 변경해야 하는 것이다.
    // 그리고 애니메이션도 CrossFade의 인자로 무기를 넣어주고, 애니메이터도 변경해 줘야 한다.

    private Player_DB_Anim _anim;
    void Awake()
    {
        _anim = GetComponent<Player_DB_Anim>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkillManager._instance.StartSkill(SkillManager.Skills.Dodge, 0f, transform.position, transform.rotation, transform);
            _anim.CrossFade(Player_DB_State.DB_State.Skill, SkillManager.Skills.Dodge);
        }
    }
}
