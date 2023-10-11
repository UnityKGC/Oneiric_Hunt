using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchManager : MonoBehaviour
{
    public static CatchManager _instance;

    public bool CheckCatchSuccess { get { return _isSuccess; } }

    private CatchEvent _eventData;
    private CatchPolice _police;
    
    private bool _clickQ = false;

    private float _maxGauge = 1f;
    private float _nowGauge; // 현재 게이지
    private float _clickGainGauge = 0.1f; // 버튼 누를 때 획득하는 게이지 량 

    private bool _isSuccess;
    private bool _isStart;
    //private bool _isFail; // 실패 확인 변수
    private bool _isEnd; // 끝 확인 변수


    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!_isStart) return;

        if(_maxGauge <= _nowGauge || _isSuccess)
        {
            CatchEventEnd();
        }
        else
        {
            CheckKey();
        }
        
    }
    public void StartCatchEvt(CatchEvent evt)
    {
        ChaseManager._instance.Chasestate = ChaseManager.ChaseState.Catch; // 상태를 변화시킨다.

        _eventData = evt;

        _isStart = true;
        _isEnd = false;
        
        _police = _eventData._police;
    }
    void CheckKey()
    {
        if (_police.State == CatchPolice.CatchPoliceState.Fight)
        {
            if (_clickQ)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _nowGauge += _clickGainGauge;
                    _clickQ = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    _nowGauge += _clickGainGauge;
                    _clickQ = true;
                }
            }

            UIManager._instacne.UpdateCatchUI(_nowGauge);

            //Debug.Log("현재 게이지 량 : " + _nowGauge);

            if (_maxGauge <= _nowGauge)
            {
                _isSuccess = true;
            }
        }
    }
    void CatchEventEnd()
    {
        if(_isSuccess)
        {
            ChaseManager._instance.Chasestate = ChaseManager.ChaseState.Normal; // 게임상태를 평상시로,
            _isStart = false;
            _isEnd = true;
            _eventData = null;
            //Debug.Log("빠져나옴");
        }
    }
}
