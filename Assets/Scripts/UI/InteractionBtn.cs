using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionBtn : MonoBehaviour
{
    [SerializeField] Sprite[] _sprites;

    private Sprite _nowSprite;
    private Image _image;

    void Start()
    {
        // �̺�Ʈ ���
        UIManager._instacne._interactBtnEvt -= InteractionAble;
        UIManager._instacne._interactBtnEvt += InteractionAble;

        _image = GetComponent<Image>();
    }

    void InteractionAble(ObjectType type) // ��ȣ�ۿ� ������ ��ü ���η� ���Դٸ�,
    {
        _nowSprite = _sprites[(int)type];
        _image.sprite = _nowSprite;
    }
    private void OnDestroy()
    {
        UIManager._instacne._interactBtnEvt -= InteractionAble;
    }
}
