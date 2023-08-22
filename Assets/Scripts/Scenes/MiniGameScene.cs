using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameScene : MonoBehaviour
{
    
    void Start()
    {
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.MiniGame;
    }

    void Update()
    {
        
    }

}
