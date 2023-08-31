using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager _instance;

    public DialogueData _nowData; // 현재 대화 데이터 => 필요정보? 1. 대화참여하는 NPC리스트 2. 대사 리스트 3. NPC마다 지니는 대사 index 4. 마지막대사인지? 

    public Action<DialogueData> _dialogueEvt = null;
    public Action _npcEvt = null; // 대화중인 NPC에게 알려줌.
    public Action _clickNext = null; // 다음 대사 출력해라.

    QuestData _quest;
    void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        if (_nowData == null) return; // 현재 등록된 대화가 없으면 바로 리턴때림

        if(_clickNext != null && _nowData._isStart)
        {
            if(Input.GetKeyDown(KeyCode.Space) && !_nowData._isFinish)
            {
                _clickNext.Invoke();
            }
        }
    }
    
    public void GetQuestDialogue(QuestData quest, DialogueData data) // 퀘스트를 받고, 그 퀘스트의 상태에 따라 다른 대사를 받는다.
    {
        _quest = quest;
        _nowData = data;
        if (_dialogueEvt != null)
        {
            _dialogueEvt.Invoke(data); // 보낸다.
        }
    }

    public void EndDialogue() // 마지막 대사가 출력되고 대화 타입에 따라 퀘스트 시작인지 끝인지를 전달함.
    {
        switch (_nowData._dialogueType)
        {
            case DialogueType.QuestStart:
                QuestManager._instance.StartQuest(_quest);
                break;

            case DialogueType.QuestProgress:
                _nowData._isStart = _nowData._isFinish = false;
                _nowData._index = 0;
                break;

            case DialogueType.QuestEnd:
                _quest._isFinish = true;
                QuestManager._instance.FinishQuest(_quest);
                break;
            case DialogueType.ChangeScene:
                SceneManagerEX._instance.LoadScene(SceneManagerEX.SceneType.FirstDreamScene);
                break;
        }
        _nowData = null;
        _npcEvt?.Invoke(); // NPC에게 대화가 끝났다고 알림.
    }
}
