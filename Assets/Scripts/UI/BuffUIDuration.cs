using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffUIDuration : MonoBehaviour
{
    [SerializeField] private Image _img;
    private float _durationTime; // ���� �� ���� �ð�
    private float _remainTime; // ���� ���� �ð�
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
        _img.fillAmount = 0f; // ��Ÿ�� ������ 0���� �ʱ�ȭ = Ȥ�ó� �𸣴ϱ�
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
