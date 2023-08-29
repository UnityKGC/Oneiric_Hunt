using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public List<Image> _skillFrontImgLst; // Amount�� 1 => 0���� �������� ��Ÿ��ó�� �����.

    public List<GameObject> _weaponSkillLst; // ���� ����� �ش� ���� ��ųUI���� ���̵��� Ȱ��,��Ȱ��ȭ ��
    void Start()
    {
        UIManager._instacne._skillEvt -= StartCoolTime;
        UIManager._instacne._skillEvt += StartCoolTime;

        UIManager._instacne._weaponEvt -= ChangeWeapon;
        UIManager._instacne._weaponEvt += ChangeWeapon;

        UIManager._instacne._endBattleEvt -= ResetSkillUI;
        UIManager._instacne._endBattleEvt += ResetSkillUI;
    }

    void Update()
    {
        
    }
    void StartCoolTime(SkillScriptable scriptable, SkillManager.Skills skill) // ��ũ���ͺ� ��ü��, Skill Enum�� �̿��Ͽ� ��ųUI ��Ÿ�� ����
    {
        if (scriptable == null || !gameObject.activeSelf) return; // ���ӿ�����Ʈ�� �����ִ��� �Ǵ��Ͽ� PC����UI�� �����UI�� ���ÿ� �������� �ʰ� ����.

        SkillScriptable temp = null;

        temp = scriptable;

        Image img = _skillFrontImgLst[(int)skill];

        img.fillAmount = 1f; // 1�� �ؼ�, ��ų�� ��Ÿ�� �� ó�� ����

        if (temp != null)
            StartCoroutine(UpdateCoolUI(temp, img));

    }
    IEnumerator UpdateCoolUI(SkillScriptable scriptable, Image img)
    {
        while (scriptable._remainTime > 0)
        {
            img.fillAmount = scriptable._remainTime / scriptable._skillCoolTime;
            yield return new WaitForEndOfFrame();
        }
        img.fillAmount = 0f; // ��Ÿ�� ������ 0���� �ʱ�ȭ = Ȥ�ó� �𸣴ϱ�
    }

    void ChangeWeapon(WeaponType weapon)
    {
        if (!gameObject.activeSelf) return;

        for(int i = 0; i < (int)WeaponType.Max; i++)
        {
            if(i == (int)weapon)
                _weaponSkillLst[i].SetActive(true);
            else
                _weaponSkillLst[i].SetActive(false);
        }
    }

    void ResetSkillUI() // ��ų UI �ʱ�ȭ
    {
        if (!gameObject.activeSelf) return;

        foreach (Image img in _skillFrontImgLst)
        {
            img.fillAmount = 0f;
        }
    }
    private void OnDestroy()
    {
        UIManager._instacne._skillEvt -= StartCoolTime;
        UIManager._instacne._weaponEvt -= ChangeWeapon;
        UIManager._instacne._endBattleEvt -= ResetSkillUI;
    }
}
