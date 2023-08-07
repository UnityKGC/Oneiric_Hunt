using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePlayer : MonoBehaviour
{
    private CharacterController _ctrl;

    public Quaternion _rightQuat;
    public Quaternion _leftQuat;

    public float _moveSpd = 500f;

    public float Hp { get { return _hp; } set { _hp = value; } }

    private float _hp = 100f;

    private bool _isRight = true;

    private bool _isFinish = false;
    private bool _isDead = false;

    private void Awake()
    {
        _ctrl = GetComponent<CharacterController>();
    }
    void Start()
    {
        Hp = MiniGameManager._instance._nowHp;
    }

    private void Update()
    {
        if (_isFinish || _isDead) return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(_isRight) // 우측을 바라보고 있으면,
            {
                transform.rotation = _leftQuat;// 좌측으로 시점 변경
                _isRight = false;
            }
            else // 좌측을 바라보고 있으면,
            {
                transform.rotation = _rightQuat;// 우측으로 시점 변경
                _isRight = true;
            }
        }
    }
    void FixedUpdate()
    {
        if (_isFinish) return;

        _ctrl.SimpleMove(transform.forward * _moveSpd * Time.deltaTime);
    }
    public void SetDamage(float dmg)
    {
        MiniGameManager._instance._isDamaged = true;
        Hp -= dmg;

        if(Hp <= 0)
        {
            Debug.Log("파키------------인");
            MiniGameManager._instance._isDead = true;
            Destroy(gameObject);
            return;
        }

        MiniGameManager._instance._nowHp = Hp;
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MiniGameObstacle")) // 장애물에 부딪혔으면,
        {
            SetDamage(20); // 20데미지
        }
        if(other.CompareTag("MiniGameCheckPoint")) // 체크포인트라면
        {
            MiniGameManager._instance.SetCheckPoint(); // 현재 플레이어의 위치와 방향을 저장한다.
            Destroy(other.gameObject);
        }
    }
    private void OnDestroy()
    {
        
    }
}
