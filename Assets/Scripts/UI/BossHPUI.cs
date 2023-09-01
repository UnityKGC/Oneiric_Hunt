using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    [SerializeField] Image _bossHP;
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
    }
    
    private void OnDestroy()
    {
        UIManager._instacne._bossHPEvt -= SetHPAmount;
    }
}
