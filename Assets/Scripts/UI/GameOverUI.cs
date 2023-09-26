using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    enum ButtonType
    {
        None = -1,
        ReStart,
        Lobby,
        End,
    }
    ButtonType btnType = ButtonType.None;
    public GameObject _warning;
    public void ClickBtn(int idx)
    {
        SoundManager._instance.PlayUISound();
        btnType = (ButtonType)idx;
        switch (btnType)
        {
            case ButtonType.ReStart:
                SceneManagerEX._instance.LoadScene(SceneManagerEX._instance.NowScene); // 현재 Scene을 다시 시작
                break;
            case ButtonType.Lobby: // Lobby든, End든 결국 Warning실행
            case ButtonType.End:
                UIManager._instacne.SetPopupUI(_warning);
                _warning.SetActive(true);
                break;
        }
    }
    public void ClickWarningBtn(int idx)
    {
        SoundManager._instance.PlayUISound();

        if (idx == 0)
        {
            switch(btnType)
            {
                case ButtonType.Lobby:
                    SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.Title); // 타이틀로
                    break;
                case ButtonType.End:
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        PluginManager._instance.GetExitBox();
                    }
                    Application.Quit(); // 게임 종료
                    break;
            }
        }
        else
            UIManager._instacne.ClosePopupUI();
    }
}
