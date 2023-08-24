using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] List<QuestData> _questList = new List<QuestData>(); // �ش� NPC�� ���ϰ� �ִ� ����Ʈ ��� => 1. �̷��� NPC�� ����Ʈ�� ���ϰ� �ִ°� ������? 2. �ƴϸ�, ����Ʈ ID�� ���ϰ�, ����Ʈ �Ŵ����� ��� ����Ʈ�� ���ϰ� �ִ°� ������? => �ϴ� 1�� �����Ѵ�.
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if(Input.GetKeyDown(KeyCode.Space))
            {
                QuestData quest = null;
                DialogueData data = null;

                quest = GetQuestOrder();

                if (quest == null) return;

                // ����Ʈ ����, ����, ���ῡ ���� �����ϴ� ����Ʈ ���̾�αװ� �ٸ���.
                // �̰� ����Ʈ �Ŵ����� �Լ��� ����°� ������? ���ϰ��� DialogueData�ΰ���.
                if (!quest._isStart)
                    data = quest._dialogueData[(int)DialogueType.QuestStart];
                else if(!quest._isAchieve)
                    data = quest._dialogueData[(int)DialogueType.QuestProgress];
                else if(quest._isAchieve)
                    data = quest._dialogueData[(int)DialogueType.QuestEnd];
                else
                    data = quest._dialogueData[(int)DialogueType.Normal];

                DialogueManager._instance.GetQuestDialogue(quest, data);
            }
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
    
}
