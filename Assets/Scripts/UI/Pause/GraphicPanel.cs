using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicPanel : MonoBehaviour
{
    enum ChangeState
    {
        None = 1,
        Resolution,
        Screen,
    }

    ChangeState _graphicState = ChangeState.None;

    public GameObject _checkUI;

    bool _isFullScreen = true;

    int _resolutionNum = 0; // 해상도 변경 감지를 위해 사용하는 변수 => 현재 index
    int _screenNum = 0; // 화면 변경 감지를 위해 사용하는 변수 => 현재 index

    int _prevNum; // 기존의 설정 Num
    public void ClickResolutionBtn(int idx) // 해상도 변경
    {
        _graphicState = ChangeState.Resolution;
        _prevNum = _resolutionNum;
        ChangeResolution(idx);
        CheckResoultion(idx);
    }

    public void ClickScreenBtn(int idx) // 화면 변경
    {
        _graphicState = ChangeState.Screen;
        ChangeScreen(idx);
        CheckScreen(idx);
    }

    void CheckResoultion(int idx) // 버튼을 눌러 받은 idx가 기존의 Num와 다르면, Num으로 등록한다.
    {
        if(_resolutionNum != idx)
        {
            _resolutionNum = idx;
            _checkUI.SetActive(true);
            UIManager._instacne.SetPopupUI(_checkUI);
        }
    }
    void CheckScreen(int idx)
    {
        if(_screenNum != idx)
        {
            _screenNum = idx;
            _checkUI.SetActive(true);
            UIManager._instacne.SetPopupUI(_checkUI);
        }
    }
    public void ClickAgreeBtn() // 예를 누르면 그대로 UI만 닫아, 변경한 설정을 유지한다
    {
        UIManager._instacne.ClosePopupUI();
    }
    public void ClickDisagreeBtn() // 아니오를 누르면, 기존에 설정Num을 이용하여 이전 상태로 되돌린 후, UI를 닫는다.
    {
        switch (_graphicState)
        {
            case ChangeState.Resolution:
                _resolutionNum = _prevNum;
                ChangeResolution(_resolutionNum);
                break;
            case ChangeState.Screen:
                _screenNum = _prevNum;
                ChangeScreen(_screenNum);
                break;
        }

        UIManager._instacne.ClosePopupUI();
    }
    void ChangeResolution(int idx) // Resolution 변경
    {
        switch (idx)
        {
            case 0:
                Screen.SetResolution(800, 600, _isFullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 1024, _isFullScreen);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, _isFullScreen);
                break;
            case 3:
                Screen.SetResolution(2560, 1440, _isFullScreen);
                break;
        }
    }
    void ChangeScreen(int idx) // Screen 변경
    {
        switch (idx)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                _isFullScreen = false;
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                _isFullScreen = true;
                break;
        }
    }
    public void ClickGraphicBtn(int idx) // 이건 재확인 UI 하지 말자.
    {
        QualitySettings.SetQualityLevel(idx);
    }
}
