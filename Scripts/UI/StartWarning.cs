using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWarning : MonoBehaviour
{
    public void ClickAgreeBtn() // �� ��ư
    {
        SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.FirstHouseScene); // �Ͻ������� HouseScene���� �̵�
    }
    public void ClickDisAgreeBtn() // �ƴϿ� ��ư
    {
        UIManager._instacne.ClosePopupUI(); // UI�Ŵ����� �ڽ��� �ݾ��� => �ֳ�? �ֱٿ� ���� PopupUI�� �׻� �ڱ��ڽ��̹Ƿ�
    }
}
