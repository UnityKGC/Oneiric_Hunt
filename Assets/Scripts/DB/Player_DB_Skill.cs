using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DB_Skill : MonoBehaviour
{
    // WeaponSwap��ų���� ChagneWeapon�̶�� �Լ��� ȣ���ؾ��Ѵ�
    // �� �Լ��� �÷��̾ ���ϰ� �ִ� ���⸦ Ȯ���ϰ�, ��ü�Ǵ� ����� �����ؾ� �ϴ� ���̴�.
    // �׸��� �ִϸ��̼ǵ� CrossFade�� ���ڷ� ���⸦ �־��ְ�, �ִϸ����͵� ������ ��� �Ѵ�.

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
