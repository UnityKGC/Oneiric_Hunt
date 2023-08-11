using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using KeyCode = UnityEngine.InputSystem.Key; // InputSystem의 Key

[System.Serializable] // 직렬화
public class QTEKeys
{
    public KeyCode _key; // 이벤트 시 눌러야 할 Key.
}
public class QTEEvent : MonoBehaviour
{
    public List<QTEKeys> _keys = new List<QTEKeys>(); // 해당 이벤트에서 눌러야 할 키 리스트

    public Vector2 _pos; // QTEUI가 배치되야 할 위치

    public float _time; // 눌러야 할 시간
}
