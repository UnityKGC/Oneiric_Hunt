using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private GameObject _player;
    private CharacterController _ctrl;
    private MonsterStat _stat;
    private Vector3 _dir;
    private void Awake()
    {
        _ctrl = GetComponent<CharacterController>();
        _stat = GetComponent<MonsterStat>();
    }
    void Start()
    {
        _player = GameManager._instance.Player;
    }

    void FixedUpdate()
    {
        _dir = (_player.transform.position - transform.position).normalized;
        _ctrl.SimpleMove(_dir * _stat.MoveSpd * Time.deltaTime);

        Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 720f * Time.deltaTime);
    }
}
