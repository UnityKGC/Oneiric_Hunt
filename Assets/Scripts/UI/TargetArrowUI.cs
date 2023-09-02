using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrowUI : MonoBehaviour
{
    public static TargetArrowUI _instance;

    public static GameObject _closestObj = null; // ���� ����� ������Ʈ.
    public static float _distance = float.MaxValue; // �÷��̾�� ���� ����� ������Ʈ�� ������ �Ÿ�

    [SerializeField] RectTransform _trans; // ������ transform
    [SerializeField] Transform _playerTrans; // �÷��̾��� Transform;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        _trans = GetComponent<RectTransform>();
        _playerTrans = transform.parent.GetComponent<Transform>();
        // �̺�Ʈ ���
        QuestManager._instance._targetArrowEvt -= SetTarget;
        QuestManager._instance._targetArrowEvt += SetTarget;

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (_closestObj.activeSelf)
        {
            Vector3 dir = _closestObj.transform.position - _playerTrans.position;
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
    public static void UpdateClosestTarget(GameObject obj, float distance)
    {
        if (obj == null || obj.activeSelf == false) return; // ������Ʈ�� ���ų�, ��Ȱ��ȭ �Ǿ��ٸ�,
        if(_distance > distance)
        {
            _distance = distance;
            _closestObj = obj;
        }
    }
}
