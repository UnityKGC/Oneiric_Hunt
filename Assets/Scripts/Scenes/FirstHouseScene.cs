using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FirstHouseScene : MonoBehaviour
{
    public Material _skybox;
    void Start()
    {
        RenderSettings.skybox = _skybox;
        RenderSettings.customReflection = null; // Reset any custom reflection probes
        DynamicGI.UpdateEnvironment();

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
