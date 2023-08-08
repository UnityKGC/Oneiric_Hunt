using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPC : MonoBehaviour
{
    
    [SerializeField] List<QuestData> _questList = new List<QuestData>(); // �ش� NPC�� ���ϰ� �ִ� ����Ʈ ��� => 1. �̷��� NPC�� ����Ʈ�� ���ϰ� �ִ°� ������? 2. �ƴϸ�, ����Ʈ ID�� ���ϰ�, ����Ʈ �Ŵ����� ��� ����Ʈ�� ���ϰ� �ִ°� ������? => �ϴ� 1�� �����Ѵ�.

    [SerializeField] List<DialogueData> _dialogueLst = new List<DialogueData>(); // NPC�� ���ϰ� �ִ� ��� ���

    [SerializeField] Collider _doorCol; // �ӽ÷�?
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
                if(!_questList[0]._isStart) // ����Ʈ�� ���۵� ��������
                {
                    QuestData quest = _questList[0];

                    DialogueManager._instance.GetDialogueLine(_dialogueLst[0], quest);

                    //QuestManager._instance.StartQuest(_questList[0]); // ����Ʈ ���� => ������ ���̾�α׿��� ����

                }
                else if(_questList[0]._isAchieve && !_questList[0]._isFinish)
                {
                    QuestData quest = _questList[0];
                    quest._isFinish = true;

                    DialogueManager._instance.GetDialogueLine(_dialogueLst[1], quest);

                    //QuestManager._instance.FinishQuest(_questList[0]); // ����Ʈ ���� => ������ ���̾�α׿��� ����

                    _doorCol.enabled = true; // TestNPC�� �� �� �ִ� ���� ���� => �� ����
                    // ����Ʈ ��
                }
            }
        }
    }
}
