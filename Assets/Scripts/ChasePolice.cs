using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePolice : MonoBehaviour
{
    public enum ChasePoliceState
    {
        None,
        Run,
        Catch,
    }
    public ChasePoliceState State { get { return _state; } set { _state = value; } }
    private ChasePoliceState _state = ChasePoliceState.Run;

    public GameObject _player;

    private Vector3 _dir; // 방향벡터
    private float _dist; // 플레이어와 경찰 사이의 거리
    private CharacterController _ctrl;

    private float _moveSpd = 100f;
    private void Awake()
    {
        _ctrl = GetComponent<CharacterController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ChaseManager._instance.ChasePlayerDie) return; // Update에서 GameManager를 계속 불러오는 게 맘에 안듬

        switch(State)
        {
            case ChasePoliceState.Run:
                UpdateRun();
                break;
            case ChasePoliceState.Catch:
                UpdateCatch();
                break;
        }
    }
    void UpdateRun()
    {
        _dir = (_player.transform.position - transform.position).normalized;
        _ctrl.SimpleMove(_dir * _moveSpd * Time.deltaTime);

        Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 0.1f);

        _dist = Vector3.Distance(_player.transform.position, transform.position);
        if (_dist <= 1.2f)
        {
            State = ChasePoliceState.Catch;
        }
    }
    void UpdateCatch()
    {
        ChaseManager._instance.ChasePlayerDie = true;
        //Debug.Log("끗");
    }
}
