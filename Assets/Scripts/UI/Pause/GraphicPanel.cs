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

    [SerializeField] int _resolutionNum = 0; // �ػ� ���� ������ ���� ����ϴ� ���� => ���� index
    [SerializeField] int _screenNum = 0; // ȭ�� ���� ������ ���� ����ϴ� ���� => ���� index

    [SerializeField] int _prevNum; // ������ ���� Num
    public void ClickResolutionBtn(int idx) // �ػ� ����
    {
        SoundManager._instance.PlayUISound();
        _graphicState = ChangeState.Resolution;
        _prevNum = _resolutionNum;
        ChangeResolution(idx);
        CheckResoultion(idx);
    }

    public void ClickScreenBtn(int idx) // ȭ�� ����
    {
        SoundManager._instance.PlayUISound();
        _graphicState = ChangeState.Screen;
        ChangeScreen(idx); // ȭ�� ���� ����
        CheckScreen(idx); // ������ ���� �ٸ��� Ȯ��
    }

    void CheckResoultion(int idx) // ��ư�� ���� ���� idx�� ������ Num�� �ٸ���, Num���� ����Ѵ�.
    {
        if(_resolutionNum != idx)
        {
            _resolutionNum = idx;

            _checkUI.SetActive(true);
            _checkUI.transform.localScale = Vector3.one; // Prefabȭ �� ��, ȣ���ߴٰ� �ݱ⸦ ������ Scale�� 0�� �Ǿ����... �׷��� 1�� ������Ŵ
            UIManager._instacne.SetPopupUI(_checkUI); // ��Ȯ�� Popup ȣ��
        }
    }
    void CheckScreen(int idx) // ������ ���� �ٸ��� Ȯ�� => �ٸ��� ��Ȯ�� ui ȣ��
    {
        if(_screenNum != idx)
        {
            _screenNum = idx;
            _checkUI.SetActive(true);

            _checkUI.transform.localScale = Vector3.one;
            UIManager._instacne.SetPopupUI(_checkUI); // ��Ȯ�� Popup ȣ��
        }
    }
    public void ClickAgreeBtn() // _checkUI => ���� ������ �״�� UI�� �ݾ�, ������ ������ �����Ѵ�
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.ClosePopupUI();
    }
    public void ClickDisagreeBtn() // _checkUI => �ƴϿ��� ������, ������ ����Num�� �̿��Ͽ� ���� ���·� �ǵ��� ��, UI�� �ݴ´�.
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
    void ChangeResolution(int idx) // Resolution ����
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
    void ChangeScreen(int idx) // Screen ����
    {
        switch (idx)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                _isFullScreen = false;
                _nowScreen.text = "âȭ��";
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                _isFullScreen = true;
                _nowScreen.text = "��üȭ�� ";
                break;
        }
    }
    public void ClickGraphicBtn(int idx) // �̰� ��Ȯ�� UI ���� ����.
    {
        SoundManager._instance.PlayUISound();
        QualitySettings.SetQualityLevel(idx);
        switch(idx)
        {
            case 0:
                _nowGraphic.text = "��ȭ��";
                break;
            case 1:
                _nowGraphic.text = "��ȭ��";
                break;
            case 2:
                _nowGraphic.text = "��ȭ��";
                break;
        }
    }
    public void ClickExitButton()
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.AllClosePopupUI(); // ��� PopupUI �ݱ�
    }
}
