using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Car"))
        {
            switch (SceneManagerEX._instance.NowScene)
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
}
