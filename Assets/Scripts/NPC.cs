using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    enum NPCState
    {
        None = -1,
        Normal,
        Talk,
    }

    [SerializeField] NPCState _state = NPCState.Normal;

    [SerializeField] List<QuestData> _questList = new List<QuestData>(); // �ش� NPC�� ���ϰ� �ִ� ����Ʈ ��� => 1. �̷��� NPC�� ����Ʈ�� ���ϰ� �ִ°� ������? 2. �ƴϸ�, ����Ʈ ID�� ���ϰ�, ����Ʈ �Ŵ����� ��� ����Ʈ�� ���ϰ� �ִ°� ������? => �ϴ� 1�� �����Ѵ�.
    [SerializeField] private QuestMarkUI _questMarkUI; // ����Ʈ ��ũ
    [SerializeField] private TargetUIObj _targetObj;

    QuestData _quest = null; // �÷��̾ �ް� �ִ� ����Ʈ, null�̸� �ް� ���� ����

    
    bool _isTalkAble; // �÷��̾ ������ ��ȭ������ �����ߴ���
    bool _activeMark; // ����Ʈ ��ũ�� Ȱ��ȭ �Ǿ� �ִ��� Ȯ��

    [SerializeField] int _npcID;
    [SerializeField] bool _isSpace = false;
    [SerializeField] bool _simpleSpace = false;
    void Start()
    {
        DialogueManager._instance._npcEvt -= EndDialogue;
        DialogueManager._instance._npcEvt += EndDialogue;

        QuestManager._instance._questMarkEvt -= SetQuestMark;
        QuestManager._instance._questMarkEvt += SetQuestMark;

        if (CheckQuest()) // �޾߾� �ϴ� ����Ʈ�� �ִٸ�s
            _questMarkUI.SetQuestMark(QuestMark.Start); // ! ��ũ Ȱ��ȭ

        _targetObj.Init(_npcID);
    }
    void Update()
    {
        switch(_state)
        {
            case NPCState.Normal:
                UpdateNormal();
                break;
            case NPCState.Talk:
                UpdateTalk();
                break;
        }
    }
    void UpdateNormal()
    {
        if(!_activeMark) // ����Ʈ ��ũ�� �����ִٸ�,
        {
            if(CheckQuest()) // ����Ʈ�� ���� �����ִٸ�.
            {
                if(_quest == null) // ����Ʈ�� �ް� ���� �ʴٸ�,
                {
                    _questMarkUI.SetQuestMark(QuestMark.Start); // ���� ��ũ Ȱ��ȭ
                    _activeMark = true;
                }
            }
        }

        if (_isTalkAble) // ��ȭ�����ϰ�, ��ȭ���� �ƴ϶��,
        {
            _isSpace = Input.GetKeyDown(KeyCode.Space);
            _simpleSpace = SimpleInput.GetButtonDown("Space");
            
            if (_isSpace || _simpleSpace)
            {
                _quest = null;
                DialogueData data = null;

                _quest = GetQuestOrder();

                if (_quest == null) return;

                // ����Ʈ ����, ����, ���ῡ ���� �����ϴ� ����Ʈ ���̾�αװ� �ٸ���.
                // �̰� ����Ʈ �Ŵ����� �Լ��� ����°� ������? ���ϰ��� DialogueData�ΰ���.

                if (!_quest._isStart)
                    data = _quest._dialogueData[(int)DialogueType.QuestStart];
                else if (!_quest._isAchieve)
                    data = _quest._dialogueData[(int)DialogueType.QuestProgress];
                else if (_quest._isAchieve)
                    data = _quest._dialogueData[(int)DialogueType.QuestEnd];
                else
                    data = _quest._dialogueData[(int)DialogueType.Normal];

                DialogueManager._instance.GetQuestDialogue(_quest, data);

                Input.ResetInputAxes();

                _state = NPCState.Talk;
            }
        }
    }

    void UpdateTalk()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isTalkAble = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isTalkAble = false;
        }
    }
    void SetQuestMark(QuestMark mark, QuestData quest) // ����Ʈ�Ŵ����κ��� ���޹�����,
    {
        if (_quest != quest) return; // ������ ����ϴ� ����Ʈ�� �ƴ϶�� ����

        _questMarkUI.SetQuestMark(mark);

        if(mark == QuestMark.None) // None�� ���޹޾����� => ����Ʈ�� �Ϸ�������, ���� ���ϰ� �ִ� quest�� null��, ��ũȰ��ȭ���δ�false�� ��ȯ
        {
            _quest = null;
            _activeMark = false;
        }
    }
    bool CheckQuest()
    {
        foreach (QuestData data in _questList)
        {
            if (!data._isFinish) // ���� �Ϸ����� ���� ����Ʈ�� ������ true
            {
                return true;
            }
        }
        return false; // �� �Ϸ������� false
    }

    QuestData GetQuestOrder() // ���� ����Ʈ�� ���ʸ� Ȯ�� ��, 0���� ������ Ȯ���ؼ�, isFinish�� true�� �ƴ϶��, �� ģ���� ���Ͻ�Ŵ.
    {
        for(int i = 0; i < _questList.Count; i++)
        {
            if (!_questList[i]._isFinish)
                return _questList[i];
        }
        return null; // ���� �Ϸ��� null ����.
    }

    void EndDialogue()
    {
        _state = NPCState.Normal;
    }
}
