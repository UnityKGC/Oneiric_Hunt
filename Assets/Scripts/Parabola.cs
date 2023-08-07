using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    private Vector3 _targetPos; // 목표 위치

    private GameObject _obj;

    public bool _isFinish;

    private float _time;
    public void SetValue(GameObject obj, Vector3 targetPos, float time)
    {
        _obj = obj;
        _targetPos = targetPos;
        _time = time;
        StartCoroutine(StartParabola());
    }
    IEnumerator StartParabola()
    {
        Vector3 distance = _targetPos - _obj.transform.position;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz / _time;
        float Vy = (sY / _time) + (0.5f * Mathf.Abs(Physics.gravity.y) * _time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        _obj.GetComponent<Rigidbody>().velocity = result;

        yield return new WaitForSeconds(_time);

        _obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
