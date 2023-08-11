using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScene : MonoBehaviour
{
    // 플레이어가 죽었는지 판단
    // 스테이지가 끝났는지 판단

    void Start()
    {
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.Chase;
        GameManager._instance.Playstate = GameManager.PlayState.Real_Normal;
        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Tutorial);
    }

    void Update()
    {
           
    }
}
