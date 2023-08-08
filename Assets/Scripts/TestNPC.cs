using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPC : MonoBehaviour
{
    [SerializeField] List<QuestData> _questList = new List<QuestData>(); // �ش� NPC�� ���ϰ� �ִ� ����Ʈ ��� => 1. �̷��� NPC�� ����Ʈ�� ���ϰ� �ִ°� ������? 2. �ƴϸ�, ����Ʈ ID�� ���ϰ�, ����Ʈ �Ŵ����� ��� ����Ʈ�� ���ϰ� �ִ°� ������? => �ϴ� 1�� �����Ѵ�.

    [SerializeField] Collider _doorCol; // �ӽ÷�?
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // ����Ʈ�� �����߰�, �Ϸ����ǵ� ����������, 1. ���⼭ _isAchieve�� Ȯ���ұ�? 2. �ƴϸ� �ƿ� �Ŵ����� AchieveCheck��� �Լ��� ����(���ڷ� ����Ʈ ID), �ش� ����Ʈ�� �Ϸ������� �����ߴ��� Ȯ���ұ�?
    // �ƴϴ� �ϴ� 1�� �ϰ�, �����丵 �� ��, 2�� ��ġ�ų� ���� => ��, �׳� �巡�� ������� �� ���� ����Ʈ�� ����� ����. => NPC�� ����Ʈ�� ���ϰ� �ְ�, �Ŵ����� NPC�� ���ϰ� �ִ� ����Ʈ �����ϴ°� �������ִ� ��������
    // ����Ʈ ���� ��ü(NPC��)�� ���� ����Ʈ �����͸� �����ְ�, ����Ʈ �Ϸ� ������ ����ƮID��, ����Ʈ �Ŵ��� ���� �̿��� ����.

        // => 2�� ���ڴ� �ƹ��� ����, �׷��� ���� �ϴ� _questList���� ���ĺ���. => _questList�� QuestData�� �ִ°� �ƴ�, �Ŵ����� ��� �� �����ϵ���,
        // => �׷��ٸ�... ������ ����ȭ �ؼ�, ���� ����� �����ϰ� �ִµ�, ���� �ڵ� ������ �̸� �ش� ����Ʈ�����͸� �ν��Ͻ�ȭ �ؼ� �����͸� ���� ��, �ش� ����Ʈ�� ���������� ������ �ϴµ�,
        // 1. ��� ���� ����Ʈ �����͸� �ʱ�ȭ ��ų��? => �̰ͺ��� �ذ��ؾ� �ڴ�. �� �˾ƺ��� �� ��
        // 2. ���� �ʱ�ȭ�� ����Ʈ �����͸� ����Ʈ �Ŵ����� ���ϰ� ������?


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(!_questList[0]._isStart) // ����Ʈ�� ���۵� ��������
                {
                    QuestData quest = _questList[0];
                    for (int i = 0; i < quest._questStartTextList.Count; i++)
                    {
                        Debug.Log(quest._questStartTextList[i]);
                    }
                    QuestManager._instance.StartQuest(_questList[0]); // ����Ʈ ����

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

                    _doorCol.enabled = true; // TestNPC�� �� �� �ִ� ���� ���� => �� ����
                    // ����Ʈ ��
                }
            }
        }
    }
}
