using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaseMovement : MonoBehaviour
{
    //public Vector3 Dir { get { return _dir; } set { _dir = value; } }

    public float _rotSpd;
    public float _h, _v;

    private PlayerStat _stat;
    public Vector3 _dir;

    [SerializeField]
    private Transform _cameTrans;

    //CharacterController _contrl;
    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (GameManager._instance.ChasePlayerDie) return;

        Move();
        Rotate();
    }
    void Move()
    {
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        _dir = new Vector3(_h, 0, _v);
        _dir = Quaternion.AngleAxis(_cameTrans.rotation.eulerAngles.y, Vector3.up) * _dir; // ���� _dir * y�� �������� ī�޶��� rotation.y����ŭ Quaternion�� �����Ѵ�.

        _dir = _dir.normalized;

        //transform.Translate(_dir * _moveSpd * Time.deltaTime);
        float magnitude = Mathf.Clamp01(_dir.magnitude) * _stat.MoveSpd; // �������߸� �ӵ��� �ش�.

        transform.position += _dir * magnitude * Time.deltaTime;
        //_contrl.SimpleMove(_dir * magnitude * Time.deltaTime);
    }
    void Rotate()
    {
        if (_dir != Vector3.zero) // _dir�� 0�� �ƴ϶��, ��! �����̰� �ִٸ�,
        {
            Quaternion quat = Quaternion.LookRotation(_dir, Vector3.up); // ù��° ���ڴ� �ٶ󺸴� �����̸�, �ι�° ���ڴ� ���̴�. => ù��° ���ڴ� �ٶ󺸰��� �ϴ� ���⺤�Ͱ� �����Ѵ�.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, _rotSpd * Time.deltaTime); // (ù��°) ���� (�ι�°)���� (����°)�� �ӵ��� ȸ���� ����� �����Ѵ�.
        }

        /*
        float angle = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, angle, 0f);*/
    }
    private void OnApplicationFocus(bool focus) // ���� ��, ������ ��Ŀ�� ������, ����
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
