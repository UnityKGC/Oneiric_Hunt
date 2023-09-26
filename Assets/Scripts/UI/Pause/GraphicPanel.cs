using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] TextMeshProUGUI _nowResolution;
    [SerializeField] TextMeshProUGUI _nowScreen;
    [SerializeField] TextMeshProUGUI _nowGraphic;

    [SerializeField] int _resolutionNum = 0; // 해상도 변경 감지를 위해 사용하는 변수 => 현재 index
    [SerializeField] int _screenNum = 0; // 화면 변경 감지를 위해 사용하는 변수 => 현재 index

    [SerializeField] int _prevNum; // 기존의 설정 Num
    public void ClickResolutionBtn(int idx) // 해상도 변경
    {
        SoundManager._instance.PlayUISound();
        _graphicState = ChangeState.Resolution;
        _prevNum = _resolutionNum;
        ChangeResolution(idx);
        CheckResoultion(idx);
    }

    public void ClickScreenBtn(int idx) // 화면 변경
    {
        SoundManager._instance.PlayUISound();
        _graphicState = ChangeState.Screen;
        ChangeScreen(idx); // 화면 변경 적용
        CheckScreen(idx); // 이전의 값과 다른지 확인
    }

    void CheckResoultion(int idx) // 버튼을 눌러 받은 idx가 기존의 Num와 다르면, Num으로 등록한다.
    {
        if(_resolutionNum != idx)
        {
            _resolutionNum = idx;

            _checkUI.SetActive(true);
            _checkUI.transform.localScale = Vector3.one; // Prefab화 한 후, 호출했다가 닫기를 누르면 Scale이 0이 되어버림... 그래서 1로 고정시킴
            UIManager._instacne.SetPopupUI(_checkUI); // 재확인 Popup 호출
        }
    }
    void CheckScreen(int idx) // 이전의 값과 다른지 확인 => 다르면 재확인 ui 호출
    {
        if(_screenNum != idx)
        {
            _screenNum = idx;
            _checkUI.SetActive(true);

            _checkUI.transform.localScale = Vector3.one;
            UIManager._instacne.SetPopupUI(_checkUI); // 재확인 Popup 호출
        }
    }
    public void ClickAgreeBtn() // _checkUI => 예를 누르면 그대로 UI만 닫아, 변경한 설정을 유지한다
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.ClosePopupUI();
    }
    public void ClickDisagreeBtn() // _checkUI => 아니오를 누르면, 기존에 설정Num을 이용하여 이전 상태로 되돌린 후, UI를 닫는다.
    {
        SoundManager._instance.PlayUISound();
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
                _nowResolution.text = "800x600";
                break;
            case 1:
                Screen.SetResolution(1280, 1024, _isFullScreen);
                _nowResolution.text = "1280x1024";
                break;
            case 2:
                Screen.SetResolution(1920, 1080, _isFullScreen);
                _nowResolution.text = "1920x1080";
                break;
            case 3:
                Screen.SetResolution(2560, 1440, _isFullScreen);
                _nowResolution.text = "2560x1440";
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
                _nowScreen.text = "창화면";
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                _isFullScreen = true;
                _nowScreen.text = "전체화면 ";
                break;
        }
    }
    public void ClickGraphicBtn(int idx) // 이건 재확인 UI 하지 말자.
    {
        SoundManager._instance.PlayUISound();
        QualitySettings.SetQualityLevel(idx);
        switch(idx)
        {
            case 0:
                _nowGraphic.text = "저화질";
                break;
            case 1:
                _nowGraphic.text = "중화질";
                break;
            case 2:
                _nowGraphic.text = "고화질";
                break;
        }
    }
    public void ClickExitButton()
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.AllClosePopupUI(); // 모든 PopupUI 닫기
    }
}
