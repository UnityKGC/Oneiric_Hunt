using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    public void ClickLobbyBtn()
    {
        gameObject.SetActive(false);
        SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.Title); // �κ�(Ÿ��Ʋ)�� �̵�
    }
    public void ClickEndBtn()
    {
        Application.Quit();
    }
}
