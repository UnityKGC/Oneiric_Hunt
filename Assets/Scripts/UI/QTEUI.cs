using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class QTEUI : MonoBehaviour
{
    public RectTransform _qteUI;
    public RectTransform _qteFront;
    public TextMeshProUGUI _qteText;
    void Awake()
    {
        UIManager._instacne._qtePosEvt -= SetUIPos;
        UIManager._instacne._qtePosEvt += SetUIPos;
    }
    private void OnEnable()
    {
        _qteFront.DOScale(0, 3f).SetUpdate(true);
    }
    /*
    private void OnEnable() // Ȱ��ȭ �� ��, QTEUI�� 1. ������ ��ġ�� ������Ų��. 2. ������ ��ġ�� ������Ų��.
    {
        
    }
    */
    void SetUIPos(Vector2 vec) // ��ġ�� ���� ��, _qteUI�� �ش� ��ġ�� ������.
    {
        _qteUI.anchoredPosition = vec;
    }
    private void OnDisable()
    {
        _qteFront.DOScale(1, 0f).SetUpdate(true);
    }
    private void OnDestroy()
    {
        _qteFront.DOScale(1, 0f).SetUpdate(true);
        UIManager._instacne._qtePosEvt -= SetUIPos;
    }
}
