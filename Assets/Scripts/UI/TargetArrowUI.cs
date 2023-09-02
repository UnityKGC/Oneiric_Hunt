using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrowUI : MonoBehaviour
{
    public static TargetArrowUI _instance;

    public static GameObject _closestObj = null; // 가장 가까운 오브젝트.
    public static float _distance = float.MaxValue; // 플레이어와 가장 가까운 오브젝트의 사이의 거리

    [SerializeField] RectTransform _trans; // 본인의 transform
    [SerializeField] Transform _playerTrans; // 플레이어의 Transform;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        _trans = GetComponent<RectTransform>();
        _playerTrans = transform.parent.GetComponent<Transform>();
        // 이벤트 등록
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
        if (obj == null || obj.activeSelf == false) return; // 오브젝트가 없거나, 비활성화 되었다면,
        if(_distance > distance)
        {
            _distance = distance;
            _closestObj = obj;
        }
    }
}
