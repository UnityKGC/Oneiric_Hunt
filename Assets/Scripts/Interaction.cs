using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public ObjectInteractUI _interactionUI; // 상호작용 UI
    public ObjectType ObjType { get { return _objType; } set { _objType = value; } }

    [SerializeField] private ObjectType _objType = ObjectType.None;
    void Start()
    {
        _interactionUI.Init(InteractionManager._instance.SetUIText(ObjType));
        _interactionUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) // 1. 처음 들어오면 판단 => 선택된 아이템이 없으면 선택, 이미 존재해도 본인이 가장 가깝지 않을 것 이므로 넘김
    {
        if (other.gameObject.tag == "Player")
        {
            _interactionUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other) // 2. 들어오고 있을 때
    {
        if (other.gameObject.tag == "Player")
        {
            float distance = Vector3.Distance(transform.position, other.transform.position); // 거리를 계산

            if(InteractionManager._instance.UpdateClosestInteractObj(this, distance))
            {
                _interactionUI.gameObject.SetActive(true);
            }
            else
            {
                _interactionUI.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _interactionUI.gameObject.SetActive(false);
            InteractionManager._instance.ExitClosestInteractObj(this);
        }
    }
}
