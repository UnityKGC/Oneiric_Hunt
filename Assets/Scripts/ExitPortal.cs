using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(SceneManagerEX._instance.NowScene)
        {
            case SceneManagerEX.SceneType.FirstHouseScene:
                SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.FirstDreamScene);
                break;
            case SceneManagerEX.SceneType.FirstDreamScene:
                SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.Chase);
                break;
            case SceneManagerEX.SceneType.Chase:
                SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.MiniGame);
                break;
            case SceneManagerEX.SceneType.MiniGame:
                SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.FirstHouseScene);
                break;
        }
    }
}
