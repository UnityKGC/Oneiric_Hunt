using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWarning : MonoBehaviour
{
    public void ClickAgreeBtn() // �� ��ư
    {
        Application.Quit();
    }
    public void ClickDisAgreeBtn() // �ƴϿ� ��ư
    {
        UIManager._instacne.ClosePopupUI(); // UI�Ŵ����� �ڽ��� �ݾ��� => �ֳ�? �ֱٿ� ���� PopupUI�� �׻� �ڱ��ڽ��̹Ƿ�
    }
}
