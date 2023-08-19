using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffUIDuration : MonoBehaviour
{
    [SerializeField] private Image _img;
    private float _durationTime; // 버프 총 지속 시간
    private float _remainTime; // 버프 남은 시간
    public void Init(float time)
    {
        _remainTime = _durationTime = time;
        StartCoroutine(BuffUICo());
    }

    IEnumerator BuffUICo()
    {
        while (_remainTime >= 0)
        {
            _img.fillAmount = _remainTime / _durationTime;

            _remainTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _img.fillAmount = 0f; // 쿨타임 끝나면 0으로 초기화 = 혹시나 모르니까
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
