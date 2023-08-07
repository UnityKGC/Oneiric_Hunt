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
            // �̺�Ʈ ����
            // �ڽ��� ���ϰ� �ִ� Scene����(�Ǵ� ���� �Ŵ�������?) �÷��̾ �� ��ġ�� �Դٰ� �˸���.

            QuestManager._instance.StartQuest(_questData);

            //QuestManager._instance._quests -= EndQuest;
            //QuestManager._instance._quests += EndQuest;

            _scene.GetTriggerEvt(this, _isLast);
            _coll.enabled = false; // �������� �ڽ��� Collider false��
        }
    }
    
    /*
    void EndQuest(QuestData data)
    {
        if(_questData == data) // �����ٰ� ���޹��� QuestData�� ������ QuestData���,
        {
            QuestManager._instance.FinishQuest(_questData);

            QuestManager._instance._quests -= EndQuest; // ����Ʈ ����
            Debug.Log("�������");
        }
    }
    */
}
