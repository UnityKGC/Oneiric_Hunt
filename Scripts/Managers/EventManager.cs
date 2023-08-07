using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Transform _player;
    public static EventManager _instance { get; private set; }

    void Start()
    {
        _instance = this;
    }

    void Update()
    {
        
    }
    public void CatchEvt(CatchPolice police)
    {

    }
}
