using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWarning : MonoBehaviour
{
    public void ClickAgreeBtn() // �� ��ư
    {
        SoundManager._instance.PlayUISound();
        SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.FirstHouseScene); // �Ͻ������� HouseScene���� �̵�
    }
    public void ClickDisAgreeBtn() // �ƴϿ� ��ư
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.ClosePopupUI(); // UI�Ŵ����� �ڽ��� �ݾ��� => �ֳ�? �ֱٿ� ���� PopupUI�� �׻� �ڱ��ڽ��̹Ƿ�
    }
}
