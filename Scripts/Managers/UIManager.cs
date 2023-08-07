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


    public Action<List<string>> _questDataEvt = null; // 퀘스트 UI를 등록시키기 위해 사용하는 콜백
    public Action<List<string>> _questContentEvt = null; // 퀘스트 내용을 갱신시키기 위해 사용하는 콜백

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
        Debug.Log("Stack 크기 : "+_popupUIStack.Count);
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // Popup메뉴가 열려있으면, 가장 최근에 열린 PopupUI 순서대로 꺼준다. STack으로 Popup관리
            // 혹시나 바로 게임으로 진행하면, Popup에 들어가있는 모든 데이터 초기화
            // 열려있는 Popup메뉴가 없으면 PauseUI를 키거나 껴준다.
            if(_popupUIStack.Count > 0)
            {
                ClosePopupUI();
            }
            else
                SetSceneUI(SceneUIState.Pause);
        }
    }

    #region Scene 관련로직
    public void SetSceneUI(SceneUIState state) // 키고 싶은 SceneUI를 State을 이용해서 호출
    {
        AllClosePopupUI(); // 혹시나 열려있는 모든 PopupUI를 꺼준다. => 해줄필요 있는지 다시 판단하기 바람 KGC

        if (SceneUI == state) // 이미 켜져있는 UI라면, sceneUI를 Play로 전환시킨다.
            state = SceneUIState.Play;

        switch(state)
        {
            case SceneUIState.Pause: // Pause메뉴 호출
                Time.timeScale = 0f; // 시간 멈춰!
                break;
            case SceneUIState.Play: // 미니맵, 등등 호출
                Time.timeScale = 1f;
                break;
        }

        ActiveSceneUI(state);
        SetCursor(state);
        SceneUI = state;
    }

    void ActiveSceneUI(SceneUIState state) // SceneUI활성화
    {
        for (int i = 0; i < _sceneUILst.Count; i++)
        {
            if (state == (SceneUIState)i && !_sceneUILst[i].activeSelf) // state번째 SceneUI가 꺼져있다면 킨다.
            {
                _sceneUILst[i].SetActive(true);
            }
            else
                _sceneUILst[i].SetActive(false);
        }
    }
    public void SetQuestUI(int id) // 플레이어가 퀘스트를 받으면, 해당 퀘스트의 id를 가져와서, 등록시킨다?
    {
        // id를 통해 가져와야 하는것 => 퀘스트 제목, 퀘스트 내용
        // 어떻게 가져와야 할까?
        // 1. 제목과 내용을 리스트로 담은 후 리스트를 리턴받는다.
        // 2. 각 함수를 하나 씩 만든 후 strng값을 하나하나 받는다.
        // PlayUI에게 전달해야 한다 => 콜백을 이용해서 주도록 하자. => 무엇을 줘야 할까? => 퀘스트 매니저로 부터 받은 그 데이터를 그대로 줘야한다. => 그럼 리스트의 값만 복제해서 주도록 하자.

        List<string> temp;
        temp = QuestManager._instance.GetQuestData(id); // 복제된 값만 받는다.

        _questDataEvt.Invoke(temp);
    }


    public void GetBringQuestContent(BringQuestData data) // 퀘스트 매니저가 이 함수를 호출한다 => 언제? Content 즉, Count가 갱신될 때, 
    {
        List<string> tempLst = new List<string>();
        for(int i = 0; i < data._objLst.Count; i++) // 오브젝트 리스트를 가져와서 하나마다 적어준다.
        {
            int now = data._objLst[i]._nowCount;
            int total = data._objLst[i]._totalCount;
            tempLst.Add(now + " / " + total);
        }

        _questContentEvt.Invoke(tempLst);
    }

    public void GetKillMonsterQuestContent(KilledMonsterQuestData data) // 퀘스트 매니저가 이 함수를 호출한다 => 언제? Content 즉, Count가 갱신될 때, 
    {
        /*
        List<string> tempLst = new List<string>();
        for (int i = 0; i < data._monsterLst.Count; i++) // 오브젝트 리스트를 가져와서 하나마다 적어준다.
        {
            int now = data._monsterLst[i]._nowCount;
            int total = data._monsterLst[i]._totalCount;
            tempLst.Add(now + " / " + total);
        }

        _questContentEvt.Invoke(tempLst);*/
    }

    #endregion

    #region Popup 관련로직
    public void SetPopupUI(GameObject popupUI) // PopupUI를 생성할 때, UIManager가 해당 PopupUI를 관리하는 popupUIStack에 집어넣음
    {
        _popupUIStack.Push(popupUI);
    }
    public void ClosePopupUI() // 가장 최근에 열린 PopupUI를 닫는다.
    {
        if (_popupUIStack.Count <= 0) // popup stack에 데이터가 없으면 리턴
            return;

        _popupUIStack.Pop().SetActive(false); // 해당 popupUI를 꺼낸 후, 비활성화 시킨다.
                                              // => 후에 Popup UI의 부모를 만들어서, ClosePopupUI를 호출하게 만들어야 겠다?
                                              // => 왜? 옵션같은 경우는, 수치를 바꾸면, 변경된 수치가 있습니다. 적용시키겠습니까? 라는 경고 UI를 호출시켜야 한다. => 즉! 각 popupUI마다 Close할 때 호출하는게 다르니, 공통적인 종료 함수를 만들어 주고, 여기에서 호출시키게 해야 한다.
    }
    public void AllClosePopupUI() // 현재 열린 모든 PopupUI를 닫아준다.
    {
        foreach(GameObject obj in _popupUIStack) // 등록되어 있는 모든 popupUI를 전부 비활성화 해 준다.
        {
            obj.SetActive(false);
        }
        _popupUIStack.Clear(); // popupStack 초기화
    }
    #endregion

    void SetCursor(SceneUIState state) // UIScene 상태에 따라 커서 조정
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
    private void OnDestroy() // Scene전환될 때, 
    {
        _sceneUILst.Clear(); // 어차피 오브젝트는 전부 파괴되었으므로, 데이터를 담고있는 자료구조만 Clear시켜준다.
        _popupUIStack.Clear();
        SetSceneUI(SceneUIState.Play); // UI켜준 채로 나갈 순 없으니까, PlaySceneUI로 해준다
    }
}
