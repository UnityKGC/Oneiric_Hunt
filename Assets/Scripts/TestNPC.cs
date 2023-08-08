using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPC : MonoBehaviour
{
    [SerializeField] List<QuestData> _questList = new List<QuestData>(); // 해당 NPC가 지니고 있는 퀘스트 목록 => 1. 이렇게 NPC가 퀘스트를 지니고 있는게 맞을까? 2. 아니면, 퀘스트 ID만 지니고, 퀘스트 매니저가 모든 퀘스트를 지니고 있는게 맞을가? => 일단 1로 진행한다.

    [SerializeField] Collider _doorCol; // 임시로?
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 퀘스트가 시작했고, 완료조건도 만족했으면, 1. 여기서 _isAchieve를 확인할까? 2. 아니면 아예 매니저에 AchieveCheck라는 함수를 만들어서(인자로 퀘스트 ID), 해당 퀘스트의 완료조건이 만족했는지 확인할까?
    // 아니다 일단 1로 하고, 리팩토링 할 때, 2로 고치거나 하자 => 즉, 그냥 드래그 드롭으로 다 내가 퀘스트를 만들어 주자. => NPC가 퀘스트를 지니고 있고, 매니저는 NPC가 지니고 있는 퀘스트 관리하는걸 지원해주는 형식으로
    // 퀘스트 시작 객체(NPC등)에 직접 퀘스트 데이터를 적어주고, 퀘스트 완료 조건은 퀘스트ID와, 퀘스트 매니저 등을 이용해 조작.

        // => 2가 좋겠다 아무리 봐도, 그러기 위해 일단 _questList부터 고쳐보자. => _questList는 QuestData가 있는게 아닌, 매니저가 모든 걸 관리하도록,
        // => 그렇다면... 지금은 직렬화 해서, 내가 수기로 조정하고 있는데, 이제 코드 내에서 미리 해당 퀘스트데이터를 인스턴스화 해서 데이터를 만든 후, 해당 퀘스트를 가져오도록 만들어야 하는데,
        // 1. 어디서 언제 퀘스트 데이터를 초기화 시킬까? => 이것부터 해결해야 겠다. 좀 알아봐야 할 듯
        // 2. 언제 초기화된 퀘스트 데이터를 퀘스트 매니저가 지니고 있을까?


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(!_questList[0]._isStart) // 퀘스트가 시작도 안했으면
                {
                    QuestData quest = _questList[0];
                    for (int i = 0; i < quest._questStartTextList.Count; i++)
                    {
                        Debug.Log(quest._questStartTextList[i]);
                    }
                    QuestManager._instance.StartQuest(_questList[0]); // 퀘스트 시작

                }
                else if(_questList[0]._isAchieve && !_questList[0]._isFinish)
                {
                    QuestData quest = _questList[0];
                    quest._isFinish = true;

                    for (int i = 0; i < quest._questEndTextList.Count; i++)
                    {
                        Debug.Log(quest._questEndTextList[i]);
                    }

                    QuestManager._instance.FinishQuest(_questList[0]);

                    _doorCol.enabled = true; // TestNPC만 할 수 있는 전용 보상 => 문 열기
                    // 퀘스트 끝
                }
            }
        }
    }
}
