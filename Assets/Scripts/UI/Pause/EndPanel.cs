using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    public void ClickLobbyBtn()
    {
        SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.Title); // 로비(타이틀)로 이동
    }
    public void ClickEndBtn()
    {
        Application.Quit();
    }
}
