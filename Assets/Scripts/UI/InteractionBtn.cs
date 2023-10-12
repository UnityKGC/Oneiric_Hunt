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
        // 이벤트 등록
        UIManager._instacne._interactBtnEvt -= InteractionAble;
        UIManager._instacne._interactBtnEvt += InteractionAble;

        _image = GetComponent<Image>();
    }

    void InteractionAble(ObjectType type) // 상호작용 가능한 객체 내부로 들어왔다면,
    {
        _nowSprite = _sprites[(int)type];
        _image.sprite = _nowSprite;
    }
    private void OnDestroy()
    {
        UIManager._instacne._interactBtnEvt -= InteractionAble;
    }
}
