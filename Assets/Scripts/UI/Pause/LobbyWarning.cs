using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyWarning : MonoBehaviour
{
    public void ClickAgreeBtn() // �� ��ư
    {
        SoundManager._instance.PlayUISound();
        SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.Title); // �κ�(Ÿ��Ʋ)�� �̵�
    }
    public void ClickDisAgreeBtn() // �ƴϿ� ��ư
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.ClosePopupUI(); // UI�Ŵ����� �ڽ��� �ݾ��� => �ֳ�? �ֱٿ� ���� PopupUI�� �׻� �ڱ��ڽ��̹Ƿ�
    }
}
