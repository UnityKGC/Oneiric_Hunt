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

        _playerTrans = GameManager._instance.Player.transform; // �÷��̾� Trans��� => Init�����ϸ� �÷��̾ null�� ��

        _parentObjID = id;
    }

    void Update()
    {
        if(_isQuest)
        {
            float dis = Vector3.Distance(_playerTrans.position, gameObject.transform.position);
            TargetArrowUI.UpdateClosestTarget(gameObject, dis);
        }
    }
    void SetQuest(int id) // ������ ���Ե� ����Ʈ�� �����ϸ� Update���۽���.
    {
        if (id == _parentObjID)
        {
            
            _isQuest = true;
        }
    }
    private void OnDestroy()
    {
        QuestManager._instance._objEffectEvt -= SetQuest;
    }
}
