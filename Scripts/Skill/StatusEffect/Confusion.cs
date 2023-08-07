using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Confusion : MonoBehaviour
{
    PlayerMovement _player;
    float _startTime; // 시작시간
    public float _remainingTime; // 남은시간
    float _duration; // 지속시간

    public void SetValues(float time)
    {
        Confusion confusion = transform.parent.GetComponentInChildren<Confusion>(); // 부모에게서 상태이상을 가져온다.
        if (confusion != this && confusion != null) // 만약 본인이 아닌 Confusion 상태이상이 이미 존재한다면
            Destroy(confusion.gameObject); // 이미 존재하고 있는 Confusion은 파괴시킨다. => 상태이상 갱신

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
