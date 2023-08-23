using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RN_Anim : BasePlayerAnim
{
    void Start()
    {
        _anim = GetComponent<Animator>();
    }
}
