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

    void StartBuffUI(float time) // ���� ���� �ð��� ���ڷ� �޾�, UI ����.
    {

    }
    void Update()
    {
        
    }
}
