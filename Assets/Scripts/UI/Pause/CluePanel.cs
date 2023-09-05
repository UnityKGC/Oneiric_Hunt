using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePanel : MonoBehaviour
{
    public void ClickCloseBtn()
    {
        Debug.Log(transform.root.name);
        UIManager._instacne.ClosePopupUI();
    }
}
