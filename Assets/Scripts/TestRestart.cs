using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRestart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManagerEX._instance.LoadScene(SceneManagerEX._instance.NowScene);
        }
    }
}
