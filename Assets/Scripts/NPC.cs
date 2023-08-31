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
    
    NPCState _state = NPCState.Normal;
    [SerializeField] List<QuestData> _questList = new List<QuestData>(); // �ش� NPC�� ���ϰ� �ִ� ����Ʈ ��� => 1. �̷��� NPC�� ����Ʈ�� ���ϰ� �ִ°� ������? 2. �ƴϸ�, ����Ʈ ID�� ���ϰ�, ����Ʈ �Ŵ����� ��� ����Ʈ�� ���ϰ� �ִ°� ������? => �ϴ� 1�� �����Ѵ�.
    
    bool _isTalkAble; // �÷��̾ ������ ��ȭ������ �����ߴ���

    [SerializeField] bool _isSpace = false;
    [SerializeField] bool _simpleSpace = false;
    void Start()
    {
        DialogueManager._instance._npcEvt -= EndDialogue;
        DialogueManager._instance._npcEvt += EndDialogue;
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
        if (_isTalkAble) // ��ȭ�����ϰ�, ��ȭ���� �ƴ϶��,
        {
            _isSpace = Input.GetKeyDown(KeyCode.Space);
            _simpleSpace = SimpleInput.GetButtonDown("Space");
            
            if (_isSpace || _simpleSpace)
            {
                QuestData quest = null;
                DialogueData data = null;

                quest = GetQuestOrder();

                if (quest == null) return;

                // ����Ʈ ����, ����, ���ῡ ���� �����ϴ� ����Ʈ ���̾�αװ� �ٸ���.
                // �̰� ����Ʈ �Ŵ����� �Լ��� ����°� ������? ���ϰ��� DialogueData�ΰ���.

                if (!quest._isStart)
                    data = quest._dialogueData[(int)DialogueType.QuestStart];
                else if (!quest._isAchieve)
                    data = quest._dialogueData[(int)DialogueType.QuestProgress];
                else if (quest._isAchieve)
                    data = quest._dialogueData[(int)DialogueType.QuestEnd];
                else
                    data = quest._dialogueData[(int)DialogueType.Normal];

                DialogueManager._instance.GetQuestDialogue(quest, data);

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
        Input.ResetInputAxes(); // ������ Input������[��) Space]�� �������������� ������, �ʱ�ȭ �����ش�.
    }
}
