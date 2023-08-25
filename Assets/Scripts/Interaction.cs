using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public ObjectInteractUI _interactionUI; // ��ȣ�ۿ� UI
    public ObjectType ObjType { get { return _objType; } set { _objType = value; } }

    [SerializeField] private ObjectType _objType = ObjectType.None;
    void Start()
    {
        _interactionUI.Init(InteractionManager._instance.SetUIText(ObjType));
        _interactionUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) // 1. ó�� ������ �Ǵ� => ���õ� �������� ������ ����, �̹� �����ص� ������ ���� ������ ���� �� �̹Ƿ� �ѱ�
    {
        if (other.gameObject.tag == "Player")
        {
            _interactionUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other) // 2. ������ ���� ��
    {
        if (other.gameObject.tag == "Player")
        {
            float distance = Vector3.Distance(transform.position, other.transform.position); // �Ÿ��� ���

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
