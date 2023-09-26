using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePanel : MonoBehaviour
{
    public void ClickCloseBtn()
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.ClosePopupUI();
    }
}
