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


    public Action<List<string>> _questDataEvt = null; // ����Ʈ UI�� ��Ͻ�Ű�� ���� ����ϴ� �ݹ�
    public Action<List<string>> _questContentEvt = null; // ����Ʈ ������ ���Ž�Ű�� ���� ����ϴ� �ݹ�

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
    public void SetQuestUI(int id) // �÷��̾ ����Ʈ�� ������, �ش� ����Ʈ�� id�� �����ͼ�, ��Ͻ�Ų��?
    {
        // id�� ���� �����;� �ϴ°� => ����Ʈ ����, ����Ʈ ����
        // ��� �����;� �ұ�?
        // 1. ����� ������ ����Ʈ�� ���� �� ����Ʈ�� ���Ϲ޴´�.
        // 2. �� �Լ��� �ϳ� �� ���� �� strng���� �ϳ��ϳ� �޴´�.
        // PlayUI���� �����ؾ� �Ѵ� => �ݹ��� �̿��ؼ� �ֵ��� ����. => ������ ��� �ұ�? => ����Ʈ �Ŵ����� ���� ���� �� �����͸� �״�� ����Ѵ�. => �׷� ����Ʈ�� ���� �����ؼ� �ֵ��� ����.

        List<string> temp;
        temp = QuestManager._instance.GetQuestData(id); // ������ ���� �޴´�.

        _questDataEvt.Invoke(temp);
    }


    public void GetBringQuestContent(BringQuestData data) // ����Ʈ �Ŵ����� �� �Լ��� ȣ���Ѵ� => ����? Content ��, Count�� ���ŵ� ��, 
    {
        List<string> tempLst = new List<string>();
        for(int i = 0; i < data._objLst.Count; i++) // ������Ʈ ����Ʈ�� �����ͼ� �ϳ����� �����ش�.
        {
            int now = data._objLst[i]._nowCount;
            int total = data._objLst[i]._totalCount;
            tempLst.Add(now + " / " + total);
        }

        _questContentEvt.Invoke(tempLst);
    }

    public void GetKillMonsterQuestContent(KilledMonsterQuestData data) // ����Ʈ �Ŵ����� �� �Լ��� ȣ���Ѵ� => ����? Content ��, Count�� ���ŵ� ��, 
    {
        /*
        List<string> tempLst = new List<string>();
        for (int i = 0; i < data._monsterLst.Count; i++) // ������Ʈ ����Ʈ�� �����ͼ� �ϳ����� �����ش�.
        {
            int now = data._monsterLst[i]._nowCount;
            int total = data._monsterLst[i]._totalCount;
            tempLst.Add(now + " / " + total);
        }

        _questContentEvt.Invoke(tempLst);*/
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
