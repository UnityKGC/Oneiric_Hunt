using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstHouseScene : MonoBehaviour
{
    void Start()
    {
        GameManager._instance.Playstate = GameManager.PlayState.Real_Normal;
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.FirstHouseScene;
        CameraManager._instance.SetFreeLookCam();

        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Tutorial);
    }


    void Update()
    {

        // �ش� Scene�� ���� ����Ʈ�� ������, portal�� Ȱ��ȭ ��Ű�� �ϱ�.
    }
}
