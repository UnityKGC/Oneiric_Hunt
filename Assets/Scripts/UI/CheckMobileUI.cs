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
    [SerializeField] private bool _isMobile = false;
    void Start()
    {
        //_isMobile = GameManager._instance.IsMobile;
        foreach(GameObject obj in _objs)
        {
            obj.SetActive(false);
        }

        if (_isMobile)
            _objs[1].SetActive(true);
        else
            _objs[0].SetActive(true);
    }
}
