using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOfficeScene : MonoBehaviour
{
    [SerializeField] Material _skybox;

    void Start()
    {
        GameManager._instance.Playstate = GameManager.PlayState.Real_Normal;
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.PlayerOfficeScene;

        //UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Play);

        RenderSettings.skybox = _skybox;
        RenderSettings.customReflection = null; // Reset any custom reflection probes
        DynamicGI.UpdateEnvironment();

        CameraManager._instance.ChangeCam(CameraType.PlayerCam);

        SoundManager._instance.PlayBGM(BGM.PlayerOffice);
    }
    void Update()
    {
        
    }
}
