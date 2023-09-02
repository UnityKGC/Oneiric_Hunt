using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrowUI : MonoBehaviour
{
    public static TargetArrowUI _instance;

    public static GameObject _nextObj = null; // ���� ����� ������Ʈ.
    static GameObject _thisObj; // ���� ������Ʈ
    [SerializeField] RectTransform _trans; // ������ transform
    [SerializeField] Transform _playerTrans; // �÷��̾��� Transform;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        _thisObj = gameObject;
        _trans = GetComponent<RectTransform>();
        _playerTrans = transform.parent.GetComponent<Transform>();
        // �̺�Ʈ ���
        QuestManager._instance._targetArrowEvt -= SetTarget;
        QuestManager._instance._targetArrowEvt += SetTarget;

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (_nextObj.activeSelf)
        {
            Vector3 dir = _nextObj.transform.position - _playerTrans.position;
            Quaternion quat = Quaternion.LookRotation(dir, Vector3.up);
            _trans.rotation = quat;
            _trans.rotation = Quaternion.Euler(-_trans.eulerAngles.x, _trans.eulerAngles.y, _trans.eulerAngles.z);
        }
        else
            gameObject.SetActive(false);
    }

    void SetTarget(bool value)
    {
        gameObject.SetActive(value);
    }
    public static void UpdateClosestTarget(GameObject obj)
    {
        if (obj == null || obj.activeSelf == false) return; // ������Ʈ�� ���ų�, ��Ȱ��ȭ �Ǿ��ٸ�,

        _nextObj = obj;

        if (!_thisObj.activeSelf) // ������ �����ִٸ� ���ش�.
            _thisObj.SetActive(true);
    }
}
