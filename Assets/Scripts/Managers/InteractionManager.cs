using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static Interaction _closeObj; // 가장 가까이 있는 오브젝트
    public static float _closeDist = float.MaxValue; // 가장 가까운 오브젝트의 거리

    public string SetUIText(ObjectType type)
    {
        string text = "";
        switch (type)
        {
            case ObjectType.NPC:
                text = "대화";
                break;
            case ObjectType.Door:
                text = "열기";
                break;
            case ObjectType.Object:
                text = "줍기";
                break;
        }
        return text;
    }
    
    public bool UpdateClosestInteractObj(Interaction obj, float distance)
    {
        if (_closeObj == null || distance < _closeDist)
        {
            _closeObj = obj;
            _closeDist = distance;
            return true;
        }

        if (_closeObj == obj)
        {
            _closeDist = distance;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ExitClosestInteractObj(Interaction obj)
    {
        if(_closeObj == obj)
        {
            _closeObj = null;
            _closeDist = float.MaxValue;
        }
    }
}
