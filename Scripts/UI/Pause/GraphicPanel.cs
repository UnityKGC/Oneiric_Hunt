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

    int _resolutionNum = 0; // �ػ� ���� ������ ���� ����ϴ� ���� => ���� index
    int _screenNum = 0; // ȭ�� ���� ������ ���� ����ϴ� ���� => ���� index

    int _prevNum; // ������ ���� Num
    public void ClickResolutionBtn(int idx) // �ػ� ����
    {
        _graphicState = ChangeState.Resolution;
        _prevNum = _resolutionNum;
        ChangeResolution(idx);
        CheckResoultion(idx);
    }

    public void ClickScreenBtn(int idx) // ȭ�� ����
    {
        _graphicState = ChangeState.Screen;
        ChangeScreen(idx);
        CheckScreen(idx);
    }

    void CheckResoultion(int idx) // ��ư�� ���� ���� idx�� ������ Num�� �ٸ���, Num���� ����Ѵ�.
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
    public void ClickAgreeBtn() // ���� ������ �״�� UI�� �ݾ�, ������ ������ �����Ѵ�
    {
        UIManager._instacne.ClosePopupUI();
    }
    public void ClickDisagreeBtn() // �ƴϿ��� ������, ������ ����Num�� �̿��Ͽ� ���� ���·� �ǵ��� ��, UI�� �ݴ´�.
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
    void ChangeResolution(int idx) // Resolution ����
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
    void ChangeScreen(int idx) // Screen ����
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
    public void ClickGraphicBtn(int idx) // �̰� ��Ȯ�� UI ���� ����.
    {
        QualitySettings.SetQualityLevel(idx);
    }
}
