using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamEvtTrigger : MonoBehaviour
{
    //[SerializeField] FirstDreamScene _scene;
    //[SerializeField] bool _isLast = false;

    //[SerializeField] QuestData _questData;
    void Start()
    {
        
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
            /*
            QuestManager._instance.StartQuest(_questData);

            _scene.GetTriggerEvt(this, _isLast);
            _coll.enabled = false; // �������� �ڽ��� Collider false��*/
        }
    }
}
