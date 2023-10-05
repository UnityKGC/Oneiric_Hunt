using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWarning : MonoBehaviour
{
    public void ClickAgreeBtn() // �� ��ư
    {
        SoundManager._instance.PlayUISound();
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            PluginManager._instance.GetExitBox();
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            PluginManager._instance.GetExitWinMessageBox();
        }
        else
            Application.Quit();
    }
    public void ClickDisAgreeBtn() // �ƴϿ� ��ư
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.ClosePopupUI(); // UI�Ŵ����� �ڽ��� �ݾ��� => �ֳ�? �ֱٿ� ���� PopupUI�� �׻� �ڱ��ڽ��̹Ƿ�
    }
}
