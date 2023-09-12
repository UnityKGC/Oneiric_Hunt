using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    private Vector3 _targetPos; // 목표 위치

    private GameObject _obj;

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
        Vector3 distance = _targetPos - _obj.transform.position; // 목표 위치 - 본인의 위치를 한 방향벡터를 구함
        Vector3 distanceXz = distance; // 그 방향벡터를 distanceXz라는 변수에 복사
        distanceXz.y = 0f; // 복사한 이유 => y를 0으로 하여, 높이가 없는 방향벡터를 구함 => y는 후에 따로 구하므로 0으로 초기화시킴.

        float sY = distance.y; // 기존 방향벡터의 y를 sY로 구함.
        float sXz = distanceXz.magnitude; // y값이 0인 방향벡터의 순수이동량을 sXz로 구함.

        float Vxz = sXz / _time; // xZ에 시간을 곱하여 해당 시간에 따른 순수 이동량을 구함.
        float Vy = (sY / _time) + (0.5f * Mathf.Abs(Physics.gravity.y) * _time); // 하나씩 설명하겠다.
        // sY / _time : 기존 방향벡터의 y값에 시간을 곱하여, 해당 시간에 따른 y값을 구한다.
            // 오브젝트의 현재 y값을 고려한 값.

        // 0.5f : 중력에 0.5를 곱하면 현재 중력의 절반만큼 효과가 적용되어, 목표위치에 알맞게 이동한다.
        // Mathf.Abs(Physics.gravity.y) : 유니티 기본제공 중력은 -9.81이며 이것을 Abs하여 9.81로 바꾸는 것이다.
        // _time : 그 중력에 지정한 time을 곱하여, 해당 시간에 따른 중력값을 구할 수 있다.

        // 시간에 따른 기존 Y값과, 중력적용한 y값을 더하여, 오브젝트의 현재 위치를 고려한 포물선 계산을 함.

        Vector3 result = distanceXz.normalized; // y를 제외한 방향벡터를 정규화시켜, 순수방향벡터를 result에 구한다.
        result *= Vxz; // result에 시간에 따른 순수 이동량을 곱한 후,
        result.y = Vy; // result의 시간에 따른 y값을 넣는다.

        _obj.GetComponent<Rigidbody>().velocity = result; // 오브젝트의 rigidbody의 이동량에 result를 줘, 이동량만큼 이동하게 만든 다음

        yield return new WaitForSeconds(_time); // 시간만큼 코루틴을 멈추게 하고

        _obj.GetComponent<Rigidbody>().velocity = Vector3.zero; // 다 움직였으면, 오브젝트의 이동량을 zero하여 초기화시킨다.
    }
}
