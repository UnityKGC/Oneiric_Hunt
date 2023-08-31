using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWarning : MonoBehaviour
{
    public void ClickAgreeBtn() // �� ��ư
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            PluginManager._instance.GetExitBox();
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            PluginManager._instance.GetExitWinMessageBox();
        }
        else
            Application.Quit();
    }
    public void ClickDisAgreeBtn() // �ƴϿ� ��ư
    {
        UIManager._instacne.ClosePopupUI(); // UI�Ŵ����� �ڽ��� �ݾ��� => �ֳ�? �ֱٿ� ���� PopupUI�� �׻� �ڱ��ڽ��̹Ƿ�
    }
}
