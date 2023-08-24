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

    public static Interaction _closeObj; // ���� ������ �ִ� ������Ʈ
    public static float _closeDist = float.MaxValue; // ���� ����� ������Ʈ�� �Ÿ�

    public string SetUIText(ObjectType type)
    {
        string text = "";
        switch (type)
        {
            case ObjectType.NPC:
                text = "��ȭ�ϱ�";
                break;
            case ObjectType.Clue:
                text = "Ȯ���ϱ�";
                break;
            case ObjectType.Object:
                text = "��ȣ�ۿ�";
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
