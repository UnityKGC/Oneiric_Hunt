using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_2019_4_OR_NEWER && ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using DualShockGamepadPS4 = UnityEngine.InputSystem.DualShock.DualShock4GamepadHID;
#endif
#if UNITY_2018 && ENABLE_INPUT_SYSTEM
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Plugins.PS4;
#endif

public class QTEManagersss : MonoBehaviour
{
    [Header("Configuration")]
    public float slowMotionTimeScale = 0.1f;

    [HideInInspector]
    private bool isEventStarted;
    private QTEEvents eventData;
    private bool isAllButtonsPressed;
    private bool isFail;
    private bool isEnded;
    private bool isPaused;
    private bool wrongKeyPressed;
    private float currentTime;
    [SerializeField]
    private float smoothTimeUpdate;
    private float rememberTimeScale;
    private List<QTEKey> keys = new List<QTEKey>();

    protected void Update()
    {
        if (!isEventStarted || eventData == null || isPaused) return; // 이벤트가 시작하지 않았거나, eventData가 null이거나, 게임 정지상태라면 리턴

        updateTimer(); // 타이머 갱신
        if (keys.Count == 0 || isFail) // 만약, 눌러야 할 키를 다 눌렀거나, 실패했다면,
        {
            doFinally(); // 이벤트 종료
        }
        else // 아니라면
        {
            for (int i = 0; i < eventData.keys.Count; i++) // 해당 이벤트에서 눌러야 하는 키를 하나씩 실행
            {
                if(isGamePadConnected()) // 게임패드가 연결되어 있다면,
                {
                    checkGamepadInput(eventData.keys[i]);
                }
                else // 게임패드가 없다면,
                {
                    checkKeyboardInput(eventData.keys[i]);
                }
            }
        }
    }

    public void startEvent(QTEEvents eventScriptable) // 이벤트 시작 => 여기서는 ui버튼을 눌러 시작함
    {
        eventData = eventScriptable; // 이벤트 데이터를 받아서, 매니저의 이벤트데이터에 등록
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current == null) // 현재 누른 키가 없다면,
        {
            Debug.Log("No keyboard connected. Gamepad input in QTE events is not supported now"); // 에러 호출
            return;
        }
        keys = new List<QTEKey>(eventData.keys); // 이벤트 데이터에 눌러야 하는 키 리스트 등록
#else
        keys = new List<QTEKey>(eventData.keys);
#endif
        if (eventData.onStart != null) // onStart에 무엇인가 등록되어 있으면,
        {
            eventData.onStart.Invoke(); // 콜백 => 무엇인가(함수)를 실행
        }

        isAllButtonsPressed = false; // 전부 다 눌러야 하므로 false로 초기화
        isEnded = false; // 초기화
        isFail = false;
        isPaused = false;

        rememberTimeScale = Time.timeScale; // 현재 timeScale을 저장
        switch (eventScriptable.timeType) // QTE시간 타입에 따라 다르게 행동
        {
            case QTETimeType.Slow: // 슬로우 라면,
                Time.timeScale = slowMotionTimeScale; // 느리게
                break;
            case QTETimeType.Paused: // 멈춤이라면
                Time.timeScale = 0; // 멈춤
                break;
        }
        currentTime = eventData.time; // 이벤트에 눌러야 할 시간을 currentTime에 저장
        smoothTimeUpdate = currentTime; // smoothTimeUpdate에도 눌러야 할 시간 저장
        setupGUI(); // ui
        StartCoroutine(countDown()); // 카운트 다운 코루틴 시작
    }

    private IEnumerator countDown() // 카운트 다운 코루틴
    {
        isEventStarted = true; // 이벤트 시작
        while(currentTime > 0 && isEventStarted && !isEnded) // currentTime 즉, 눌러야 할 시간 내고, 이벤트가 시작했으며, 끝나지 않았으면, 반복
        {
            if(eventData.keyboardUI.eventTimerText != null) // ui가 존재하면
            {
                eventData.keyboardUI.eventTimerText.text = currentTime.ToString(); // ui시간 갱신 시키기
            }
            currentTime--; // 시간 감소
            yield return new WaitWhile(() => isPaused); // isPaused가 true면 리턴
            yield return new WaitForSecondsRealtime(1f); // 실제 시간 1초마다 리턴
        }
        if(!isAllButtonsPressed && !isEnded) // currentTime이 0이 되면,
        {
            isFail = true; // 실패가 true
            doFinally(); // 
        }
    }

    protected void doFinally()
    {
        if (keys.Count == 0) // 눌러야 할 버튼이 더 이상 없으면,
        {
            isAllButtonsPressed = true; // 버튼 전부 눌렀음을 true로
        }
        isEnded = true; // 끝났으니, isEnded true
        isEventStarted = false; // 끝났으니, 이벤트 시작을 false로 초기화
        Time.timeScale = rememberTimeScale; // TimeScale 초기화
        var ui = getUI();

        if (ui.eventUI != null) // 성공 유무 판별
        {
            ui.eventUI.SetActive(false);
        }
        if (eventData.onEnd != null) // 끝나면 등록된 함수 호출
        {
            eventData.onEnd.Invoke();
        }
        if(eventData.onFail != null && isFail) // 실패했으면
        {
            eventData.onFail.Invoke();
        }
        if(eventData.onSuccess != null && isAllButtonsPressed) // 성공했으면
        {
            eventData.onSuccess.Invoke();
        }
        eventData = null; // 끝났으니, 이벤트 데이터 초기화
    }

    protected void OnGUI()
    {
        if (eventData == null || isEnded) return;
        if (Event.current.isKey && Event.current.type == EventType.KeyDown && eventData.failOnWrongKey && !Event.current.keyCode.ToString().Equals("None"))
        {
            wrongKeyPressed = true;
            if (isGamePadConnected())
            {
                eventData.keys.ForEach(key => wrongKeyPressed = wrongKeyPressed && (!key.gamepadDualShockKey.ToString().Equals(Event.current.keyCode.ToString()) || !key.gamepadDualShockKey.ToString().Equals(Event.current.keyCode.ToString())));
            }
            else
            {
                eventData.keys.ForEach(key => wrongKeyPressed = wrongKeyPressed && !key.keyboardKey.ToString().Equals(Event.current.keyCode.ToString()));
            }            
            
            isFail = wrongKeyPressed;
        }
    }

    protected void updateTimer() // eventData마다 눌러야 할 시간을 ui에 적용시켜주고, 시간을 감소시킨다.
    {
        smoothTimeUpdate -= Time.unscaledDeltaTime;
        var ui = getUI();
        if (ui.eventTimerImage != null)
        {
            ui.eventTimerImage.fillAmount = smoothTimeUpdate / eventData.time;
        }
    }

    public void pause()
    {
        isPaused = true;
    }

    public void play()
    {
        isPaused = false;
    }
#if !ENABLE_INPUT_SYSTEM
    private bool isGamePadConnected()
    {
        string[] temp = Input.GetJoystickNames();
        bool result = false;
        if (temp.Length > 0)
        {
            for (int i = 0; i < temp.Length; ++i)
            {
                result = result || !string.IsNullOrEmpty(temp[i]);
            }
        }
        return result;
    }

    public void checkKeyboardInput(QTEKey key)
    {
        if (Input.GetKeyDown(key.keyboardKey))
        {
            keys.Remove(key);
        }
        if (Input.GetKeyUp(key.keyboardKey) && eventData.pressType == QTEPressType.Simultaneously)
        {
            keys.Add(key);
        }
    }

    public void checkGamepadInput(QTEKey key)
    {
        if (Input.GetKeyDown(key.gamepadXBOXKey) || Input.GetKeyDown(key.gamepadDualShockKey))
        {
            keys.Remove(key);
        }
        if ((Input.GetKeyUp(key.gamepadXBOXKey) || Input.GetKeyUp(key.gamepadDualShockKey)) && eventData.pressType == QTEPressType.Simultaneously)
        {
            keys.Add(key);
        }
    }
#else
    public bool isGamePadConnected()
    {
        return Gamepad.current != null;
    }

    public bool isXBOXGamePad()
    {
        return DualShockGamepadPS4.current == null;
    }

    public void checkKeyboardInput(QTEKey key) // 이번에 눌러야 할 Key를 가져온다.
    {
        var inputType = Keyboard.current; // 현재 누른 Key를 가져온다
        if (inputType != null) // 누른 키가 존재한다면,
        {
            if (inputType[key.keyboardKey].wasPressedThisFrame) // 눌러야 되는 key가 이번 프레임에 눌러졌으면,
            {
                keys.Remove(key); // 눌러야 할 키 리스트에서 해당 키를 제거
            }
            if (inputType[key.keyboardKey].wasReleasedThisFrame && eventData.pressType == QTEPressType.Simultaneously) // 동시에 눌러야 한다면,
            {
                keys.Add(key); // 해당 키를 리스트에 추가
            }
        }
    }

    public void checkGamepadInput(QTEKey key)
    {
        var inputType = Gamepad.current;
        if (inputType != null)
        {
            if (inputType[key.gamepadXBOXKey].wasPressedThisFrame || inputType[key.gamepadDualShockKey].wasPressedThisFrame)
            {
                keys.Remove(key);
            }
            if ((inputType[key.gamepadXBOXKey].wasReleasedThisFrame || inputType[key.gamepadDualShockKey].wasReleasedThisFrame) && eventData.pressType == QTEPressType.Simultaneously)
            {
                keys.Add(key);
            }
        }
    }
#endif

    protected void setupGUI()
    {
        var ui = getUI();
        
        if (ui.eventTimerImage != null)
        {
            ui.eventTimerImage.fillAmount = 1;
        }
        if (ui.eventText != null)
        {
            ui.eventText.text = "";
            eventData.keys.ForEach(key => ui.eventText.text += key.keyboardKey + "+");
            eventData.keyboardUI.eventText.text = ui.eventText.text.Remove(ui.eventText.text.Length - 1);
        }
        if (ui.eventUI != null)
        {
            ui.eventUI.SetActive(true);
        }
    }

    protected QTEUI getUI()
    {
        var ui = eventData.keyboardUI;
        if (isGamePadConnected())
        {
#if ENABLE_INPUT_SYSTEM
            if (isXBOXGamePad())
            {
                ui = eventData.gamepadXBOXUI;
            }
            else
            {
                ui = eventData.gamepadDualShockUI;
            }
#else
            ui = eventData.gamepadUI;
#endif
        }
        return ui;
    }
}
