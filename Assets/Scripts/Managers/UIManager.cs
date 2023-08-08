using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager _instacne;

    public enum SceneUIState
    {
        None = -1,
        Pause,
        Play,
    }


    public Action<QuestData> _questDataEvt = null; // ����Ʈ UI�� ��Ͻ�Ű�� ���� ����ϴ� �ݹ�
    public Action<QuestData> _questContentEvt = null; // ����Ʈ ������ ���Ž�Ű�� ���� ����ϴ� �ݹ�
    public Action<QuestData> _questFinishEvt = null; // ����Ʈ ������ �Ϸ��Ű�� ���� ����ϴ� �ݹ�

    public SceneUIState SceneUI { get { return _sceneUIState; } set { _sceneUIState = value; } }

    private SceneUIState _sceneUIState;
    
    public List<GameObject> _sceneUILst;

    public Stack<GameObject> _popupUIStack = new Stack<GameObject>();
    private void Awake()
    {
        _instacne = this;
    }
    void Start()
    {
        SceneUI = SceneUIState.Play;
        SetCursor(SceneUI);
    }

    
    void Update()
    {
        Debug.Log("Stack ũ�� : "+_popupUIStack.Count);
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // Popup�޴��� ����������, ���� �ֱٿ� ���� PopupUI ������� ���ش�. STack���� Popup����
            // Ȥ�ó� �ٷ� �������� �����ϸ�, Popup�� ���ִ� ��� ������ �ʱ�ȭ
            // �����ִ� Popup�޴��� ������ PauseUI�� Ű�ų� ���ش�.
            if(_popupUIStack.Count > 0)
            {
                ClosePopupUI();
            }
            else
                SetSceneUI(SceneUIState.Pause);
        }
    }

    #region Scene ���÷���
    public void SetSceneUI(SceneUIState state) // Ű�� ���� SceneUI�� State�� �̿��ؼ� ȣ��
    {
        AllClosePopupUI(); // Ȥ�ó� �����ִ� ��� PopupUI�� ���ش�. => �����ʿ� �ִ��� �ٽ� �Ǵ��ϱ� �ٶ� KGC

        if (SceneUI == state) // �̹� �����ִ� UI���, sceneUI�� Play�� ��ȯ��Ų��.
            state = SceneUIState.Play;

        switch(state)
        {
            case SceneUIState.Pause: // Pause�޴� ȣ��
                Time.timeScale = 0f; // �ð� ����!
                break;
            case SceneUIState.Play: // �̴ϸ�, ��� ȣ��
                Time.timeScale = 1f;
                break;
        }

        ActiveSceneUI(state);
        SetCursor(state);
        SceneUI = state;
    }

    void ActiveSceneUI(SceneUIState state) // SceneUIȰ��ȭ
    {
        for (int i = 0; i < _sceneUILst.Count; i++)
        {
            if (state == (SceneUIState)i && !_sceneUILst[i].activeSelf) // state��° SceneUI�� �����ִٸ� Ų��.
            {
                _sceneUILst[i].SetActive(true);
            }
            else
                _sceneUILst[i].SetActive(false);
        }
    }

    public void StartQuest(QuestData data) // ����Ʈ �Ŵ����� ���� ������ ����Ʈ�� ������ �޴´�.
    {
        if(_questDataEvt != null)
            _questDataEvt.Invoke(data);
    }
    public void UpdateQuestContent(QuestData data) // ����Ʈ �Ŵ����� ���� ����Ʈ ������ ������ �޴´�.
    {
        if (_questContentEvt != null)
            _questContentEvt.Invoke(data);
    }
    public void FinishQuest(QuestData data) // ����Ʈ �Ŵ����� ���� ����Ʈ ������ ������ �޴´�.
    {
        if (_questFinishEvt != null)
            _questFinishEvt.Invoke(data);
    }
    #endregion

    #region Popup ���÷���
    public void SetPopupUI(GameObject popupUI) // PopupUI�� ������ ��, UIManager�� �ش� PopupUI�� �����ϴ� popupUIStack�� �������
    {
        _popupUIStack.Push(popupUI);
    }
    public void ClosePopupUI() // ���� �ֱٿ� ���� PopupUI�� �ݴ´�.
    {
        if (_popupUIStack.Count <= 0) // popup stack�� �����Ͱ� ������ ����
            return;

        _popupUIStack.Pop().SetActive(false); // �ش� popupUI�� ���� ��, ��Ȱ��ȭ ��Ų��.
                                              // => �Ŀ� Popup UI�� �θ� ����, ClosePopupUI�� ȣ���ϰ� ������ �ڴ�?
                                              // => ��? �ɼǰ��� ����, ��ġ�� �ٲٸ�, ����� ��ġ�� �ֽ��ϴ�. �����Ű�ڽ��ϱ�? ��� ��� UI�� ȣ����Ѿ� �Ѵ�. => ��! �� popupUI���� Close�� �� ȣ���ϴ°� �ٸ���, �������� ���� �Լ��� ����� �ְ�, ���⿡�� ȣ���Ű�� �ؾ� �Ѵ�.
    }
    public void AllClosePopupUI() // ���� ���� ��� PopupUI�� �ݾ��ش�.
    {
        foreach(GameObject obj in _popupUIStack) // ��ϵǾ� �ִ� ��� popupUI�� ���� ��Ȱ��ȭ �� �ش�.
        {
            obj.SetActive(false);
        }
        _popupUIStack.Clear(); // popupStack �ʱ�ȭ
    }
    #endregion

    void SetCursor(SceneUIState state) // UIScene ���¿� ���� Ŀ�� ����
    {
        switch (state)
        {
            case SceneUIState.Pause:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case SceneUIState.Play:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }

    }
    private void OnDestroy() // Scene��ȯ�� ��, 
    {
        _sceneUILst.Clear(); // ������ ������Ʈ�� ���� �ı��Ǿ����Ƿ�, �����͸� ����ִ� �ڷᱸ���� Clear�����ش�.
        _popupUIStack.Clear();
        SetSceneUI(SceneUIState.Play); // UI���� ä�� ���� �� �����ϱ�, PlaySceneUI�� ���ش�
    }
}
