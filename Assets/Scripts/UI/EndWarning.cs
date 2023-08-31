using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWarning : MonoBehaviour
{
    public void ClickAgreeBtn() // 예 버튼
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
    public void ClickDisAgreeBtn() // 아니오 버튼
    {
        UIManager._instacne.ClosePopupUI(); // UI매니저가 자신을 닫아줌 => 왜냐? 최근에 열린 PopupUI는 항상 자기자신이므로
    }
}
