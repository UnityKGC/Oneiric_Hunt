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
    [SerializeField] List<QuestData> _questList = new List<QuestData>(); // 해당 NPC가 지니고 있는 퀘스트 목록 => 1. 이렇게 NPC가 퀘스트를 지니고 있는게 맞을까? 2. 아니면, 퀘스트 ID만 지니고, 퀘스트 매니저가 모든 퀘스트를 지니고 있는게 맞을가? => 일단 1로 진행한다.
    
    bool _isTalkAble; // 플레이어가 본인의 대화범위에 도달했는지

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
        if (_isTalkAble) // 대화가능하고, 대화중이 아니라면,
        {
            _isSpace = Input.GetKeyDown(KeyCode.Space);
            _simpleSpace = SimpleInput.GetButtonDown("Space");
            
            if (_isSpace || _simpleSpace)
            {
                QuestData quest = null;
                DialogueData data = null;

                quest = GetQuestOrder();

                if (quest == null) return;

                // 퀘스트 시작, 진행, 종료에 따라 전달하는 퀘스트 다이얼로그가 다르다.
                // 이건 퀘스트 매니저에 함수로 만드는게 좋을듯? 리턴값이 DialogueData인거지.

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

    QuestData GetQuestOrder() // 현재 퀘스트의 차례를 확인 즉, 0부터 끝까지 확인해서, isFinish가 true가 아니라면, 그 친구를 리턴시킴.
    {
        for(int i = 0; i < _questList.Count; i++)
        {
            if (!_questList[i]._isFinish)
                return _questList[i];
        }
        return null; // 전부 완료라면 null 리턴.
    }
    void EndDialogue()
    {
        _state = NPCState.Normal;
        Input.ResetInputAxes(); // 이전에 Input값들이[예) Space]가 눌러져있을수도 있으니, 초기화 시켜준다.
    }
}
