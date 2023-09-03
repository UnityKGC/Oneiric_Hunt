using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUIObj : MonoBehaviour
{
    private Transform _playerTrans; // 플레이어 Transform
    private int _parentObjID; // 부모 Obj ID
    private bool _isQuest = false; // 퀘스트 시작 여부
    public void Init(int id)
    {
        QuestManager._instance._objEffectEvt -= SetQuest;
        QuestManager._instance._objEffectEvt += SetQuest;

        QuestManager._instance._npcIDEvt -= SetNPC;
        QuestManager._instance._npcIDEvt += SetNPC;

        _playerTrans = GameManager._instance.Player.transform; // 플레이어 Trans등록 => Init에서하면 플레이어가 null로 뜸

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
    void SetQuest(int id) // 본인이 포함된 퀘스트가 시작하면 Update동작시작.
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
