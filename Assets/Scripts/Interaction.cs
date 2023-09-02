using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public ObjectInteractUI _interactionUI; // ��ȣ�ۿ� UI
    public ObjectType ObjType { get { return _objType; } set { _objType = value; } }

    [SerializeField] private ObjectType _objType = ObjectType.None;

    [SerializeField] private Vector3 _playerPos;
    [SerializeField] private Vector3 _Pos;

    [SerializeField] private RectTransform _interactUIRect; // UI�� Rect
    void Start()
    {
        _interactionUI.Init(InteractionManager._instance.SetUIText(ObjType));
        _interactUIRect = _interactionUI.GetComponent<RectTransform>();
        _interactionUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) // 1. ó�� ������ �Ǵ� => ���õ� �������� ������ ����, �̹� �����ص� ������ ���� ������ ���� �� �̹Ƿ� �ѱ�
    {
        if (other.gameObject.tag == "Player")
        {
            _playerPos = other.transform.position; // �÷��̾��� ��ġ
            _Pos = transform.position;//  ��ü�� ��ġ

            float dis = Vector3.Distance(_playerPos,_Pos);
            InteractionManager._instance.UpdateClosestInteractObj(this, dis);

            Vector3 temp = new Vector3((_playerPos.x + _Pos.x) / 2, 2.5f, (_playerPos.z + _Pos.z) / 2); // y�� ������Ű��, �÷��̾�� ��ü ������ �߾Ӱ��� �����´�.
            _interactUIRect.position = temp;

            _interactionUI.gameObject.SetActive(true);
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
