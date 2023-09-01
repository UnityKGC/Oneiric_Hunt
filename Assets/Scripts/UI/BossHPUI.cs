using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    [SerializeField] Image _bossHP;
    void Start()
    {
        // 이벤트 등록
        UIManager._instacne._setBossHPUI -= SetBossHP;
        UIManager._instacne._setBossHPUI += SetBossHP;

        UIManager._instacne._bossHPEvt -= SetHPAmount;
        UIManager._instacne._bossHPEvt += SetHPAmount;

        gameObject.SetActive(false); // 처음 켜지면 이벤트 등록 후, 꺼진다. 
    }
    void SetBossHP(bool value)
    {
        gameObject.SetActive(value);
    }
    void SetHPAmount(float value) // 남은 HP 비율
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
