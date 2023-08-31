using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager _instance;

    public DialogueData _nowData; // ���� ��ȭ ������ => �ʿ�����? 1. ��ȭ�����ϴ� NPC����Ʈ 2. ��� ����Ʈ 3. NPC���� ���ϴ� ��� index 4. �������������? 

    public Action<DialogueData> _dialogueEvt = null;
    public Action _npcEvt = null; // ��ȭ���� NPC���� �˷���.
    public Action _clickNext = null; // ���� ��� ����ض�.

    QuestData _quest;
    void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        if (_nowData == null) return; // ���� ��ϵ� ��ȭ�� ������ �ٷ� ���϶���

        if(_clickNext != null && _nowData._isStart)
        {
            if(Input.GetKeyDown(KeyCode.Space) && !_nowData._isFinish)
            {
                _clickNext.Invoke();
            }
        }
    }
    
    public void GetQuestDialogue(QuestData quest, DialogueData data) // ����Ʈ�� �ް�, �� ����Ʈ�� ���¿� ���� �ٸ� ��縦 �޴´�.
    {
        _quest = quest;
        _nowData = data;
        if (_dialogueEvt != null)
        {
            _dialogueEvt.Invoke(data); // ������.
        }
    }

    public void EndDialogue() // ������ ��簡 ��µǰ� ��ȭ Ÿ�Կ� ���� ����Ʈ �������� �������� ������.
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
        _npcEvt?.Invoke(); // NPC���� ��ȭ�� �����ٰ� �˸�.
    }
}
