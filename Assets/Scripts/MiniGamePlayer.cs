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
            if(_isRight) // ������ �ٶ󺸰� ������,
            {
                transform.rotation = _leftQuat;// �������� ���� ����
                _isRight = false;
            }
            else // ������ �ٶ󺸰� ������,
            {
                transform.rotation = _rightQuat;// �������� ���� ����
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
            Debug.Log("��Ű------------��");
            MiniGameManager._instance._isDead = true;
            Destroy(gameObject);
            return;
        }

        MiniGameManager._instance._nowHp = Hp;
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MiniGameObstacle")) // ��ֹ��� �ε�������,
        {
            SetDamage(20); // 20������
        }
        if(other.CompareTag("MiniGameCheckPoint")) // üũ����Ʈ���
        {
            MiniGameManager._instance.SetCheckPoint(); // ���� �÷��̾��� ��ġ�� ������ �����Ѵ�.
            Destroy(other.gameObject);
        }
    }
    private void OnDestroy()
    {
        
    }
}
