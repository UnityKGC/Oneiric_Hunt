using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    private Vector3 _targetPos; // ��ǥ ��ġ

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
        Vector3 distance = _targetPos - _obj.transform.position; // ��ǥ ��ġ - ������ ��ġ�� �� ���⺤�͸� ����
        Vector3 distanceXz = distance; // �� ���⺤�͸� distanceXz��� ������ ����
        distanceXz.y = 0f; // ������ ���� => y�� 0���� �Ͽ�, ���̰� ���� ���⺤�͸� ���� => y�� �Ŀ� ���� ���ϹǷ� 0���� �ʱ�ȭ��Ŵ.

        float sY = distance.y; // ���� ���⺤���� y�� sY�� ����.
        float sXz = distanceXz.magnitude; // y���� 0�� ���⺤���� �����̵����� sXz�� ����.

        float Vxz = sXz / _time; // xZ�� �ð��� ���Ͽ� �ش� �ð��� ���� ���� �̵����� ����.
        float Vy = (sY / _time) + (0.5f * Mathf.Abs(Physics.gravity.y) * _time); // �ϳ��� �����ϰڴ�.
        // sY / _time : ���� ���⺤���� y���� �ð��� ���Ͽ�, �ش� �ð��� ���� y���� ���Ѵ�.
            // ������Ʈ�� ���� y���� ����� ��.

        // 0.5f : �߷¿� 0.5�� ���ϸ� ���� �߷��� ���ݸ�ŭ ȿ���� ����Ǿ�, ��ǥ��ġ�� �˸°� �̵��Ѵ�.
        // Mathf.Abs(Physics.gravity.y) : ����Ƽ �⺻���� �߷��� -9.81�̸� �̰��� Abs�Ͽ� 9.81�� �ٲٴ� ���̴�.
        // _time : �� �߷¿� ������ time�� ���Ͽ�, �ش� �ð��� ���� �߷°��� ���� �� �ִ�.

        // �ð��� ���� ���� Y����, �߷������� y���� ���Ͽ�, ������Ʈ�� ���� ��ġ�� ����� ������ ����� ��.

        Vector3 result = distanceXz.normalized; // y�� ������ ���⺤�͸� ����ȭ����, �������⺤�͸� result�� ���Ѵ�.
        result *= Vxz; // result�� �ð��� ���� ���� �̵����� ���� ��,
        result.y = Vy; // result�� �ð��� ���� y���� �ִ´�.

        _obj.GetComponent<Rigidbody>().velocity = result; // ������Ʈ�� rigidbody�� �̵����� result�� ��, �̵�����ŭ �̵��ϰ� ���� ����

        yield return new WaitForSeconds(_time); // �ð���ŭ �ڷ�ƾ�� ���߰� �ϰ�

        _obj.GetComponent<Rigidbody>().velocity = Vector3.zero; // �� ����������, ������Ʈ�� �̵����� zero�Ͽ� �ʱ�ȭ��Ų��.
    }
}
