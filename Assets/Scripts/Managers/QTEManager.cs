using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public static QTEManager _instance; // 외부에서도 사용할 수 있게 싱글턴화
    public float _slowTime = 0.2f; // TimeScale에 적용될 값
    public bool CheckQTEStart { get { return _isStart; } }
    public bool CheckQTESuccess { get { return _isSuccess; } } // 외부에서 QTE이벤트가 성공했는지 실패했는지 알려주는데 필요한 변수
    public bool CheckQTEEnd { get { return _isEnd; } } // 외부에서 QTE이벤트가 끝났는지 알려주는데 필요한 변수

    private QTEEvent _eventData; // 이벤트 데이터
    private List<QTEKeys> _keys; // 눌러야 한는 키 리스트

    private float _evtTime; // 눌러야 할 제한 시간

    private bool _isSuccess; // 성공 확인 변수.
    private bool _isStart; // 이벤트 시작 확인 변수
    private bool _isFail; // 실패 확인 변수
    private bool _isEnd; // 끝 확인 변수
    
    private void Awake()
    {
        _instance = this; // 싱글턴화 => 자기자신을 등록
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!_isStart) return; // 이벤트가 시작하지 않았으면 바로 리턴
        if(_keys.Count == 0 || _isFail) // 눌러야 할 key가 더이상 존재하지 않거나, 실패하였으면, QTE이벤트 종료함수 호출
        {
            QTEEnd();
        }
        else // 눌러야 할 Key가 아직 존재한다면
        {
            for(int i = 0; i < _eventData._keys.Count; i++) // for문을 돌아, 플레이어가 해당 key를 눌렀는지 판단하는 CheckKey함수 호출
            {
                CheckKey(_eventData._keys[i]);
            }
        }
    }
    public void StartEvent(QTEEvent evt) // 이벤트 시작
    {
        _eventData = evt; // 매니저에 인자로 전달받은 이벤트 등록

        _isSuccess = false; // 변수들 초기화
        _isFail = false; // 변수들 초기화
        _isEnd = false; // 변수들 초기화

        _keys = new List<QTEKeys>(_eventData._keys); // 눌러야 할 키 리스트 복사

        Time.timeScale = _slowTime; // 시간을 느리게
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // 좀 더 부드럽게 => 좀 더 공부 필요

        _evtTime = evt._time; // 전달받은 이벤트의 제한시간을 저장

        UIManager._instacne.SetQTEPosEvt(_eventData._pos); // UI에게 QTE가 배치되야 할 위치를 알려줌

        CheckKeyUI(); // 무슨 키를 눌러야 하는지 알려주는 함수 => 현재는 Debug로 알려줌 => 후에 UI로 수정 예정
        StartCoroutine(StartCountDown()); // 카운트 다운 코루틴 시작
    }
    IEnumerator StartCountDown() // 카운트 다운
    {
        _isStart = true; // 시작했으니, 변수 갱신

        while(_isStart && _evtTime > 0) // 시작했고, 눌러야 할 시간이 0초보다 크면 반복
        {
            Debug.Log(_evtTime);
            _evtTime--; // 제한 시간 줄어듬
            yield return new WaitForSecondsRealtime(1f); // 1초마다 줄어듦
        }

        if(_isEnd == false) // 끝났을 때, _isEnd가 false라면 실패!
        {
            _isFail = true; // 실패 확인 변수를 true로
            QTEEnd(); // 이벤트 종료 함수 호출
        }
    }
    void QTEEnd() // 이벤트 종료 시
    {
        if(_keys.Count == 0) // 모두 눌렀다면
        {
            _isSuccess = true; // 성공 확인 변수를 true로
        }

        _isEnd = true; // 변수 갱신
        _isStart = false; // 변수 갱신

        Time.timeScale = 1f; // 시간 초기화
        Time.fixedDeltaTime = 0.02f; // 시간 초기화

        if (_isFail) // 실패가 true라면
        {
            Debug.Log("실패");
        }
        else if(_isSuccess) // 성공이 true라면
        {
            Debug.Log("성공");
        }
        _eventData = null; // 이벤트 해제
    }
    void CheckKey(QTEKeys key) // 인자로 전달받은 key를 눌렀는지 확인
    {
        #region InputSystem을 사용할 시
        /*
        Keyboard inputKey = Keyboard.current; // 키보드의 상태를 가져온다
        if (inputKey != null) // 키보드가 연결되어 있으면,
        {
            if (inputKey[key._key].wasPressedThisFrame) // 그리고 그 누른 키가, 이벤트에서 눌러야 하는 키와 동일하다면,
            {
                _keys.Remove(key);
            }
        }*/
        #endregion
        if (Input.GetKeyDown(key._key)) // 해당 Key를 눌렀다면,
        {
            _keys.Remove(key); // 그 key 리스트에서 해당 key를 제거한다.
        }
    }
    void CheckKeyUI() // 눌러야 하는 키 확인
    {
        string text = "";
        _keys.ForEach(key => text += key._key.ToString()); // 람다식, key 리스트의 key를 가져와, 설정되어 있는 key를 string으로 만들어 준다. => ToString을 하지 않으면, Keycode의 Enum의 정수값이 나옴
        Debug.Log("눌러야 할 버튼 : " + text);
    }

}
