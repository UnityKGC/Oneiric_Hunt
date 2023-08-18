using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour
{

    void Start()
    {
        UIManager._instacne._buffEvt -= StartBuffUI;
        UIManager._instacne._buffEvt += StartBuffUI;
    }

    void StartBuffUI(float time) // 버프 지속 시간을 인자로 받아, UI 구현.
    {

    }
    void Update()
    {
        
    }
}
