using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvas : MonoBehaviour
{
    public Image _frontImg; // HP front�κ�

    private Transform _camTrans;
    void Start()
    {
        _camTrans = Camera.main.transform;
    }
    
    void Update()
    {
        transform.LookAt(_camTrans);
    }
    public void SetHPAmount(float value) // ���� HP ����
    {
        _frontImg.fillAmount = value;
    }
}
