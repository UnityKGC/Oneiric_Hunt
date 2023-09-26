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
                SceneManagerEX._instance.LoadScene(SceneManagerEX._instance.NowScene); // ���� Scene�� �ٽ� ����
                break;
            case ButtonType.Lobby: // Lobby��, End�� �ᱹ Warning����
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
                    SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.Title); // Ÿ��Ʋ��
                    break;
                case ButtonType.End:
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        PluginManager._instance.GetExitBox();
                    }
                    Application.Quit(); // ���� ����
                    break;
            }
        }
        else
            UIManager._instacne.ClosePopupUI();
    }
}
