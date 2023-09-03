using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUIObj : MonoBehaviour
{
    private Transform _playerTrans; // �÷��̾� Transform
    private int _parentObjID; // �θ� Obj ID
    private bool _isQuest = false; // ����Ʈ ���� ����
    public void Init(int id)
    {
        QuestManager._instance._objEffectEvt -= SetQuest;
        QuestManager._instance._objEffectEvt += SetQuest;

        QuestManager._instance._npcIDEvt -= SetNPC;
        QuestManager._instance._npcIDEvt += SetNPC;

        _playerTrans = GameManager._instance.Player.transform; // �÷��̾� Trans��� => Init�����ϸ� �÷��̾ null�� ��

        _parentObjID = id;
    }

    void Update()
    {
        
    }
    void SetNPC(int id)
    {
        if(_parentObjID == id)
        {
            _isQuest = true;
            TargetArrowUI.UpdateClosestTarget(gameObject);
        }
    }
    void SetQuest(int id) // ������ ���Ե� ����Ʈ�� �����ϸ� Update���۽���.
    {
        if (id == _parentObjID)
        {
            _isQuest = true;
            TargetArrowUI.UpdateClosestTarget(gameObject);
        }
    }
    private void OnDestroy()
    {
        QuestManager._instance._objEffectEvt -= SetQuest;
    }
}
