using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyWarning : MonoBehaviour
{
    public void ClickAgreeBtn() // �� ��ư
    {
        SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.Title); // �κ�(Ÿ��Ʋ)�� �̵�
    }
    public void ClickDisAgreeBtn() // �ƴϿ� ��ư
    {
        UIManager._instacne.ClosePopupUI(); // UI�Ŵ����� �ڽ��� �ݾ��� => �ֳ�? �ֱٿ� ���� PopupUI�� �׻� �ڱ��ڽ��̹Ƿ�
    }
}
