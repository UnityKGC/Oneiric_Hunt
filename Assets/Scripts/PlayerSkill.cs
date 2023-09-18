using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerSkill : MonoBehaviour
{

    public WeaponType Weapon { get { return _weapon; } set { _weapon = value; } }

    public WeaponType _weapon = WeaponType.Sword;

    private PlayerStat _stat;
    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
    }
    void Start()
    {
        UIManager._instacne.SetWeapon(Weapon); // 현재 무기 정보를 UI매니저에게 전달
    }

    void Update()
    {
        if (GameManager._instance.PlayerDie || SkillManager._instance._isSkilling || GameManager._instance.Playstate != GameManager.PlayState.Dream_Battle) return; // 플레이어가 죽었거나, 스킬 사용 중 이라면 리턴
        // 우선 이것부터 별로다... 차라리 변수를 새로 만들어, 이벤트가 콜백될 때 마다 변수를 갱신키기게 하는게 좋으려나?

        Debug.Log("현재 무기 : " + Weapon.ToString());

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SkillManager._instance.StartSkill(Skills.WeaponSwap, 0f, transform.position, transform.rotation, transform);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 회피
            // 0.5초? 간 캐릭터의 Collider를 끄기 => 무적상태
            // 이동속도를 잠시동안 2배로 늘린다.
            SkillManager._instance.StartSkill(Skills.Dodge, 0f, transform.position, transform.rotation, transform);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (Weapon)
            {
                case WeaponType.Sword:

                    break;
                case WeaponType.Spear:

                    break;
                case WeaponType.Axe:

                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            float dmg;
            switch (Weapon)
            {
                case WeaponType.Sword:
                    dmg = Random.Range(_stat.SwordMinAtk, _stat.SwordMaxAtk);
                    SkillManager._instance.StartSkill(Skills.SwordForce, dmg, transform.position, transform.rotation);
                    break;

                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(Skills.Sweep, dmg, transform.position, transform.rotation);
                    break;

                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(Skills.WindMill, dmg, transform.position, transform.rotation, transform);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            float dmg;
            switch (Weapon)
            {
                case WeaponType.Sword:
                    dmg = Random.Range(_stat.SwordMinAtk, _stat.SwordMaxAtk);
                    SkillManager._instance.StartSkill(Skills.SpaceCut, dmg, transform.position, transform.rotation);
                    break;

                case WeaponType.Spear:
                    dmg = Random.Range(_stat.SpearMinAtk, _stat.SpearMaxAtk);
                    SkillManager._instance.StartSkill(Skills.Challenge, dmg, transform.position, transform.rotation);
                    break;

                case WeaponType.Axe:
                    dmg = Random.Range(_stat.AxeMinAtk, _stat.AxeMaxAtk);
                    SkillManager._instance.StartSkill(Skills.Berserk, dmg, transform.position, transform.rotation);
                    break;
            }
        }
    }
}
