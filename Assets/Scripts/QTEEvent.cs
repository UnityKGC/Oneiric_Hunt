using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using KeyCode = UnityEngine.InputSystem.Key; // InputSystem�� Key

[System.Serializable] // ����ȭ
public class QTEKeys
{
    public KeyCode _key; // �̺�Ʈ �� ������ �� Key.
}
public class QTEEvent : MonoBehaviour
{
    public List<QTEKeys> _keys = new List<QTEKeys>(); // �ش� �̺�Ʈ���� ������ �� Ű ����Ʈ

    public Vector2 _pos; // QTEUI�� ��ġ�Ǿ� �� ��ġ

    public float _time; // ������ �� �ð�
}
