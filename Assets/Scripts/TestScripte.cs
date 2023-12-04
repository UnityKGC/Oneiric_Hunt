using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripte : MonoBehaviour
{
    private Transform _trans;
    public float _moveSpeed;
    public Transform _targetPos;
    public Transform _targetPos_2;
    private Vector3 _dir;
    private Vector3 _dir_2;

    private bool _start;
    void Start()
    {
        _trans = GetComponent<Transform>();
        _dir = _targetPos.position - _trans.position;
        _dir_2 = _targetPos_2.position - _trans.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _start = true;

        if(_start)
            _trans.rotation = Quaternion.Slerp(_trans.rotation, Quaternion.LookRotation(_dir, Vector3.up), 0.05f);

        if(Input.GetKeyDown(KeyCode.Z))
            _trans.rotation = Quaternion.Slerp(_trans.rotation, Quaternion.LookRotation(_dir_2, Vector3.up), 0.8f);
    }
}
