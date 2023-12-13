using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    [SerializeField] Image _bossHP;
    [SerializeField] Image _midImg; // HP ������ ���� ��, ���̴� ����κ�

    [SerializeField] private float _remainDmg; // ���� ������ �����ϴ� ����
    [SerializeField] private float _nowHP = 1f; // ���� ���� ü�� => �ִ�ü�� ����;
    [SerializeField] private float _damage; // ���� ������

    private WaitForEndOfFrame _waitFrame = new WaitForEndOfFrame();

    private float _damageSpd = 0.02f;
    

    void Start()
    {
        // �̺�Ʈ ���
        UIManager._instacne._setBossHPUI -= SetBossHP;
        UIManager._instacne._setBossHPUI += SetBossHP;

        UIManager._instacne._bossHPEvt -= SetHPAmount;
        UIManager._instacne._bossHPEvt += SetHPAmount;

        gameObject.SetActive(false); // ó�� ������ �̺�Ʈ ��� ��, ������. 
    }
    void SetBossHP(bool value)
    {
        gameObject.SetActive(value);
    }
    void SetHPAmount(float value) // ���� HP ����
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);

        _bossHP.fillAmount = value;

        StopAllCoroutines();

        _damage = _nowHP - value;

        _remainDmg += _damage;

        _nowHP -= _damage;

        StartCoroutine(DamageUI());

    }
    IEnumerator DamageUI() // ������ ���� �κ�(���)�� ���ҽ�Ű�� �ڷ�ƾ
    {
        yield return new WaitForSeconds(0.1f);
        while (_remainDmg > 0) // ���� ������ŭ UI�� �����Ѵ�. value�� damagedSpd��ŭ ���ҽ�Ű�� �����, 0���� �۰ų� �������� �����.
        {
            _midImg.fillAmount -= _damageSpd;

            _remainDmg -= _damageSpd;

            yield return _waitFrame;
        }

    }

    private void OnDestroy()
    {
        UIManager._instacne._bossHPEvt -= SetHPAmount;
    }
}
