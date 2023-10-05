using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMobileUI : MonoBehaviour
{
    enum CheckEnum
    {
        None = -1,
        PC,
        Mobile,
    }
    [SerializeField] GameObject[] _objs;
    
    void Start()
    {
        foreach(GameObject obj in _objs)
        {
            obj.SetActive(false);
        }

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
            _objs[1].SetActive(true);
        else
            _objs[0].SetActive(true);
    }
}
