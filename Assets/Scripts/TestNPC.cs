using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPC : MonoBehaviour
{
    
    [SerializeField] List<QuestData> _questList = new List<QuestData>(); // 해당 NPC가 지니고 있는 퀘스트 목록 => 1. 이렇게 NPC가 퀘스트를 지니고 있는게 맞을까? 2. 아니면, 퀘스트 ID만 지니고, 퀘스트 매니저가 모든 퀘스트를 지니고 있는게 맞을가? => 일단 1로 진행한다.

    [SerializeField] List<DialogueData> _dialogueLst = new List<DialogueData>(); // NPC가 지니고 있는 대사 목록

    [SerializeField] Collider _doorCol; // 임시로?
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(!_questList[0]._isStart) // 퀘스트가 시작도 안했으면
                {
                    QuestData quest = _questList[0];

                    DialogueManager._instance.GetDialogueLine(_dialogueLst[0], quest);

                    //QuestManager._instance.StartQuest(_questList[0]); // 퀘스트 시작 => 지금은 다이얼로그에서 관리

                }
                else if(_questList[0]._isAchieve && !_questList[0]._isFinish)
                {
                    QuestData quest = _questList[0];
                    quest._isFinish = true;

                    DialogueManager._instance.GetDialogueLine(_dialogueLst[1], quest);

                    //QuestManager._instance.FinishQuest(_questList[0]); // 퀘스트 시작 => 지금은 다이얼로그에서 관리

                    _doorCol.enabled = true; // TestNPC만 할 수 있는 전용 보상 => 문 열기
                    // 퀘스트 끝
                }
            }
        }
    }
}
