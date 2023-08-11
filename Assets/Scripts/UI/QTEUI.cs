using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QTEUI : MonoBehaviour
{
    public RectTransform _qteUI;
    public TextMeshProUGUI _qteText;
    void Start()
    {
        UIManager._instacne._qtePosEvt -= SetUIPos;
        UIManager._instacne._qtePosEvt += SetUIPos;
    }
    /*
    private void OnEnable() // Ȱ��ȭ �� ��, QTEUI�� 1. ������ ��ġ�� ������Ų��. 2. ������ ��ġ�� ������Ų��.
    {
        
    }
    */
    void SetUIPos(Vector2 vec)
    {
        // ��ġ�� ���� ��, _qteUI�� �ش� ��ġ�� ������.
        _qteUI.anchoredPosition = vec;
    }
}
