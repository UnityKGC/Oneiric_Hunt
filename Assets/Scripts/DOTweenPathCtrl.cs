using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTweenPathCtrl : MonoBehaviour
{
    private DOTweenPath _doTweenPath;
    [SerializeField] Vector3[] _wayPoints;

    [SerializeField] Vector3 _nextWayPoint;

    int _lookIndex = 1;
    void Start()
    {
        _doTweenPath = GetComponent<DOTweenPath>();
        
        _wayPoints = _doTweenPath.path.wps;
        
        //transform.DOPath(_wayPoints, 4f).Play();
    }
    void Update()
    {

        Vector3 dir = _wayPoints[_lookIndex] - transform.position;

        if (dir.magnitude <= 0.1)
        {
            _lookIndex++;
        }
            
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        //transform.LookAt(_wayPoints[_lookIndex]);
    }
}
