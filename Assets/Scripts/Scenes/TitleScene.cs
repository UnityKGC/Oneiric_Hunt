using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    void Start()
    {
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.Title;
        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.None); // SceneUI�� �����

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // ������ ����
    }

    void Update()
    {
        
    }
}
