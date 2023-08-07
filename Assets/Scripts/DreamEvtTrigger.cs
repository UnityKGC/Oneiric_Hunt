using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamEvtTrigger : MonoBehaviour
{
    [SerializeField] FirstDreamScene _scene;
    [SerializeField] bool _isLast = false;
    Collider _coll;

    [SerializeField] KilledMonsterQuestData _questData;
    void Start()
    {
        _coll = GetComponent<Collider>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // 이벤트 실행
            // 자신이 지니고 있는 Scene에게(또는 무슨 매니저에게?) 플레이어가 이 위치에 왔다고 알린다.

            QuestManager._instance.StartQuest(_questData);

            //QuestManager._instance._quests -= EndQuest;
            //QuestManager._instance._quests += EndQuest;

            _scene.GetTriggerEvt(this, _isLast);
            _coll.enabled = false; // 들어왔으면 자신의 Collider false로
        }
    }
    
    /*
    void EndQuest(QuestData data)
    {
        if(_questData == data) // 끝났다고 전달받은 QuestData가 본인의 QuestData라면,
        {
            QuestManager._instance.FinishQuest(_questData);

            QuestManager._instance._quests -= EndQuest; // 퀘스트 해제
            Debug.Log("끼요오옷");
        }
    }
    */
}
