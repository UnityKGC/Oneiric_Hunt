using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOfficeScene : MonoBehaviour
{
    void Start()
    {
        GameManager._instance.Playstate = GameManager.PlayState.Real_Normal;
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.PlayerOfficeScene;

        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Play);
    }
    void Update()
    {
        
    }
}
