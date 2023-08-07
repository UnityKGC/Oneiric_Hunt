using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Confusion : MonoBehaviour
{
    PlayerMovement _player;
    float _startTime; // ���۽ð�
    public float _remainingTime; // �����ð�
    float _duration; // ���ӽð�

    public void SetValues(float time)
    {
        Confusion confusion = transform.parent.GetComponentInChildren<Confusion>(); // �θ𿡰Լ� �����̻��� �����´�.
        if (confusion != this && confusion != null) // ���� ������ �ƴ� Confusion �����̻��� �̹� �����Ѵٸ�
            Destroy(confusion.gameObject); // �̹� �����ϰ� �ִ� Confusion�� �ı���Ų��. => �����̻� ����

        _player = GetComponentInParent<PlayerMovement>();

        _duration = time;
        _startTime = Time.time;
        _player._isConfus = true;
    }
    void Start()
    {
        
    }

    void Update()
    {
        _remainingTime = _duration - (Time.time - _startTime);
        if (_remainingTime >= 0f)
        {
            
        }
        else
        {
            _player._isConfus = false;
            Destroy(gameObject);
        }
    }

}
