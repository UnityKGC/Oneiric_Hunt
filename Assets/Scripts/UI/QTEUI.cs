using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QTEUI : MonoBehaviour
{
    public RectTransform _qteUI;
    public TextMeshProUGUI _qteText;
    void Awake()
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
    private void OnDestroy()
    {
        UIManager._instacne._qtePosEvt -= SetUIPos;
    }
}
