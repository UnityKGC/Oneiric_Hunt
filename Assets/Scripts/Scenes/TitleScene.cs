using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public Material _skybox;
    void Start()
    {
        RenderSettings.skybox = _skybox;
        RenderSettings.customReflection = null; // Reset any custom reflection probes
        DynamicGI.UpdateEnvironment();

        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.Title;
        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.None); // SceneUI가 없어용

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // 강제로 변경

        SoundManager._instance.PlayBGM(BGM.Title);

    }

    void Update()
    {
        
    }
}
