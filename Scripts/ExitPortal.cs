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
        if(other.CompareTag("Player"))
        {
            switch(SceneManagerEX._instance.NowScene)
            {
                case SceneManagerEX.SceneType.FirstDreamScene:
                    SceneManager.LoadScene("Chase");
                    break;
                case SceneManagerEX.SceneType.Chase:
                    SceneManager.LoadScene("FirstHouseScene");
                    break;
                case SceneManagerEX.SceneType.FirstHouseScene:
                    SceneManager.LoadScene("MiniGame");
                    break;
                case SceneManagerEX.SceneType.MiniGame:
                    SceneManager.LoadScene("Dream");
                    break;
            }
        }
    }
}
