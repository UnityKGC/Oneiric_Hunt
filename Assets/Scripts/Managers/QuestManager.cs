using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class QuestManager : MonoBehaviour
{
    public static QuestManager _instance;

    public Action<QuestData> _quests = null;

    public Action<int> _objEffectEvt = null; // ���� TargetUIObj�� �̰� ����ϰ� ���� => ���ڷ� ���� ID�� ���� �θ��� ID�� ������, TargetArrowUI���� ���� ���ʶ�� ������
    public Action<int> _npcIDEvt = null; // NPC���Է� ���ư� ��� ȣ��.

    public Action<QuestMark, int> _questMarkEvt = null; // �÷��̾ ���𰡸� �Ͽ� ����Ʈ ��ũ�� �����ؾ��Ѵٸ�, => NPC���� ����Ʈ ��ũ �����ض�� ����.

    public Action<bool> _targetArrowEvt = null; // ��ǥ ��ġ�� ����Ű�� ���̵� UI

    Dictionary<int, QuestData> _processQuestDict = new Dictionary<int, QuestData>(); // ���� �������� ����Ʈ Dict

    Dictionary<int, List<QuestData>> _questObjDict = new Dictionary<int, List<QuestData>>();

    List<QuestData> _processQeustLst = new List<QuestData>(); // ���� �������� ����Ʈ? => ����� �ϴ� �׳� ����� ���⸸ ��, �Ŀ� �ʿ�(���)�ϸ� �� �ּ� ���� ����
    
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void StartQuest(int questID)
    {

    }

    public void StartQuest(QuestData questData) // �÷��̾ �ش� ����Ʈ ������. => ����Ʈ ������ Inspctor���� ������ �����͸� �����;� �ϹǷ�, QuestData�� �����´�.
    {
        if (questData._isStart || questData._isFinish) return; // �̹� ���۵� ����Ʈ Ȥ�� �̹� ���� ����Ʈ��� ����

        questData._isStart = true;

        InitQuestObjDict(questData);

        _processQuestDict.Add(questData._questID, questData);
        _processQeustLst.Add(questData);

        UIManager._instacne.StartQuest(questData);

        GetQuestPreced(questData); // ����Ʈ ���ຸ�� ȹ��

        // �ش� ����Ʈ�� Ÿ���� Ȯ�� ��, => �˸´� ����Ʈ�� �����ϰ� ���� => �ϴ� ���� �� ���� �޵��� ����.
    }

    // count�� �����ϸ�, �� �Լ����� 
    // ���̺�������� �����, 0��° ����, 1��° ����Ʈ ���� �̷������� string ����Ʈ�� ���� �� �������ִ� �� ������?
    void InitQuestObjDict(QuestData questData)
    {
        switch (questData._questType)
        {
            case QuestType.BringObject:
            case QuestType.FindClue:
            case QuestType.KillMonster:
            case QuestType.KillBossMonster:
                InitObjectQuest(questData);
                break;
            case QuestType.InteractionObject:
            case QuestType.GoToPosition:
                InitTriggerQuest(questData);
                break;
        }
        _targetArrowEvt?.Invoke(true);
    }
    void InitObjectQuest(QuestData questData)
    {
        foreach (ObjectData data in questData._objLst)
        {
            if (!_questObjDict.ContainsKey(data._objID))
            {
                _questObjDict.Add(data._objID, new List<QuestData>() { questData });

                _questMarkEvt?.Invoke(QuestMark.Finish, data._objID); // ������Ʈ�� �ٷ� ?��ũ Ȱ��ȭ
                _questMarkEvt?.Invoke(QuestMark.Preced, questData._npcID); // NPC�� ... ��ũ Ȱ��ȭ

                _objEffectEvt?.Invoke(data._objID);
            }
            else
                _questObjDict[data._objID].Add(questData);
        }

        if (questData._questType == QuestType.KillMonster)
            BattleManager._instance.StartBattle();
        else if(questData._questType == QuestType.KillBossMonster)
            BattleManager._instance.StartBattle(true); // ���� ���ʹ� �̷���.
    }
    void InitTriggerQuest(QuestData questData)
    {
        foreach (TriggerData data in questData._triggerDatas)
        {
            if (!_questObjDict.ContainsKey(data._objID))
            {
                _questObjDict.Add(data._objID, new List<QuestData>() { questData });

                _questMarkEvt?.Invoke(QuestMark.Finish, data._objID); // ������Ʈ�� �ٷ� ?��ũ Ȱ��ȭ
                _questMarkEvt?.Invoke(QuestMark.Preced, questData._npcID); // NPC�� ... ��ũ Ȱ��ȭ

                _objEffectEvt?.Invoke(data._objID);
            }
            else
                _questObjDict[data._objID].Add(questData);
        }
    }
    
    public bool QuestTrigger(int id) // id�� �ش��ϴ� ����Ʈ�� �÷��̾ �ް� ������, true����, ������ false����.
    {
        List<QuestData> dataLst = null;
        if (!_questObjDict.ContainsKey(id)) return false;

        dataLst = _questObjDict[id];

        foreach (QuestData data in dataLst)
        {
            switch (data._questType)
            {
                case QuestType.BringObject:
                case QuestType.FindClue:
                case QuestType.KillMonster:
                case QuestType.KillBossMonster:
                    ObjectQuest(dataLst, data, id);
                    break;
                case QuestType.InteractionObject:
                case QuestType.GoToPosition:
                    TriggerQuest(data, id);
                    break;
            }
            if (dataLst.Count <= 0) break; // �ѹ��� ���� dataLst�� �����Ͱ� ������ ���� => break���ϸ� �����߻�
        }
        return true;
    }
    void ObjectQuest(List<QuestData> dataLst, QuestData data, int id) // ����Ʈ �����Ϳ� ������Ʈ id�� Ȯ���Ͽ� triggerȮ��
    {
        foreach (ObjectData objData in data._objLst)
        {
            if (objData._objID != id || objData._isFull) continue; // objID�� ���� �ʰų�, �̹� �Ϸ� ������ ������ ����Ʈ��� �ѱ�

            objData._nowCount++;

            if (objData._totalCount <= objData._nowCount)
            {
                objData._isFull = true;
                _questMarkEvt?.Invoke(QuestMark.None, objData._objID); // ������Ʈ�� ������ �ʿ䰳���� �� ä������ ��Ȱ��ȭ
            }
                
        }

        bool isAchieve = true; // ����Ʈ �Ϸ� ���� �������� => Default�� true�� �ش�.

        // objLst�� ��� _isFull�� true��� �����ϵ���
        foreach (ObjectData objData in data._objLst)
        {
            if (!objData._isFull) // �� �ϳ��� isFull�� false���, ���� �ش� ����Ʈ�� �Ϸ���� �ʾ����Ƿ�, ����
            {
                isAchieve = false;
                break;
            }
        }

        UIManager._instacne.UpdateQuestContent(data); // ����ƮUI ����

        if (isAchieve)
        {
            data._isAchieve = true; // Object����Ʈ�� isFull�� ��� true���, ����Ʈ �Ϸ������� �����ϹǷ�, isAchieve�� true;
            
            _questMarkEvt?.Invoke(QuestMark.Finish, data._npcID); // ����Ʈ �Ϸ������� �����ϹǷ�, NPC���� ? ����Ʈ ��ũ ����

            _npcIDEvt.Invoke(data._npcID); // �Ϸᰡ���̸�, ����Ʈ�� ���� NPC���� NPCID�� �ѷ�, ������ ID�� �����ϸ�, ���̵����UI���� �����̶�� �˷��ش�

            if (data._questType == QuestType.KillMonster || data._questType == QuestType.KillBossMonster || data._questType == QuestType.InteractionObject) // ���� ���� ��ġ ����Ʈ���
            {
                data._isFinish = true; // �ٷ� ����Ʈ ������
                FinishQuest(data);
            }
        }
    }
    void TriggerQuest(QuestData data, int id)
    {
        foreach (TriggerData triData in data._triggerDatas)
        {
            if (triData._objID != id || triData._isFinish) continue;

            triData._isFinish = true;
            _questMarkEvt?.Invoke(QuestMark.None, triData._objID); // Trigger������Ʈ�� ������ ��Ȱ��ȭ
        }

        bool isAchieve = true; // ����Ʈ �Ϸ� ���� �������� => Default�� true�� �ش�.

        // objLst�� ��� _isFull�� true��� �����ϵ���
        foreach (TriggerData triData in data._triggerDatas)
        {
            if (!triData._isFinish) // �� �ϳ��� isFull�� false���, ���� �ش� ����Ʈ�� �Ϸ���� �ʾ����Ƿ�, ����
            {
                isAchieve = false;
                break;
            }
        }

        UIManager._instacne.UpdateQuestContent(data); // ����ƮUI ����

        if (isAchieve)
        {
            data._isAchieve = true; // Object����Ʈ�� isFull�� ��� true���, ����Ʈ �Ϸ������� �����ϹǷ�, isAchieve�� true;
            data._isFinish = true; // �ٷ� ����Ʈ ������
            FinishQuest(data);
        }
    }
    public void FinishQuest(QuestData questData) // ����Ʈ ����
    {
        if (!questData._isFinish) return; // isFinish�� false��� ���� ������ �ƴϴϱ� ����

        if (_quests != null)
            _quests.Invoke(questData);

        UIManager._instacne.FinishQuest(questData); // ����Ʈ UI ���� => ����

        _targetArrowEvt?.Invoke(false); // ���̵� ���� UI ��Ȱ��ȭ

        _questMarkEvt?.Invoke(QuestMark.None, questData._npcID); // ����Ʈ �Ϸ� ��, ��ũ ��Ȱ��ȭ => NPC���� => ������Ʈ���� QuestTrigger���� �̹� ������.

        switch (questData._questType)
        {
            case QuestType.BringObject:
            case QuestType.FindClue:
            case QuestType.KillMonster:
            case QuestType.KillBossMonster:
                FinishObjQuest(questData);
                break;
            case QuestType.InteractionObject:
            case QuestType.GoToPosition:
                FinishTriggerQuest(questData);
                break;
        }

        PluginManager._instance.GetToastMessage("����Ʈ �Ϸ�");
    }

    void FinishObjQuest(QuestData questData)
    {
        foreach (ObjectData objData in questData._objLst) // �ش� ����Ʈ�� objLst���� Ȯ���ؼ�, id�� ���� �ش� ����Ʈ�� �����Ѵ�. => ���� foreach�� ����ұ�? ������ �ϳ��� id������ ����Ʈ ��ü�� ���� �� �����ϱ�...?
        {
            if (_questObjDict.TryGetValue(objData._objID, out List<QuestData> objLst))
            {
                objLst.Remove(questData);
                break;
            }
        }
        
        if(questData._questType == QuestType.KillMonster)
            BattleManager._instance.EndBattle(); // ������ ��� ���� ����Ʈ�� �ٷ� �����Ƿ�, ���� ����

        // ����Ʈ ����
        GetQuestReward(questData);

        if (_processQuestDict.ContainsKey(questData._questID)) // �������� ����Ʈ Dict�� ����Ʈ�� �����Ѵٸ�,
            _processQuestDict.Remove(questData._questID); // Dict�� �ش� ����Ʈ�� �����ش�.


    }
    void FinishTriggerQuest(QuestData questData)
    {
        foreach (TriggerData triData in questData._triggerDatas) // �ش� ����Ʈ�� objLst���� Ȯ���ؼ�, id�� ���� �ش� ����Ʈ�� �����Ѵ�. => ���� foreach�� ����ұ�? ������ �ϳ��� id������ ����Ʈ ��ü�� ���� �� �����ϱ�...?
        {
            if (_questObjDict.TryGetValue(triData._objID, out List<QuestData> objLst))
            {
                objLst.Remove(questData);
                break;
            }
        }

        // ����Ʈ ����
        GetQuestReward(questData);

        if (_processQuestDict.ContainsKey(questData._questID)) // �������� ����Ʈ Dict�� ����Ʈ�� �����Ѵٸ�,
            _processQuestDict.Remove(questData._questID); // Dict�� �ش� ����Ʈ�� �����ش�.

        
    }

    // ID�� �˸´� ����Ʈ�� ���� ���������� Ȯ��
    public bool CheckQuest(int questID) {   return _processQuestDict.ContainsKey(questID);  }
    

    void GetQuestReward(QuestData questData)
    {
        RewardType type = questData._reward._type;

        if(type.HasFlag(RewardType.Exp))
        {

        }
        if (type.HasFlag(RewardType.Gold))
        {

        }
        if (type.HasFlag(RewardType.Object))
        {
            foreach(GameObject obj in questData._reward._objLst)
            {
                obj.SetActive(true);
            }
        }
        if (type.HasFlag(RewardType.DisableObj))
        {
            foreach (GameObject obj in questData._reward._disAbleObjLst)
            {
                obj.SetActive(false);
            }
        }
        if (type.HasFlag(RewardType.Collider))
        {
            questData._reward._coll.enabled = true;
        }
        
        if(type.HasFlag(RewardType.PlayType))
        {
            GameManager._instance.Playstate = questData._reward._playerState;
        }
        if (type.HasFlag(RewardType.Dialogue))
        {
            DialogueManager._instance.GetQuestDialogue(questData, questData._reward._dialogue);
        }
        if(type.HasFlag(RewardType.ChangeScene))
        {
            SceneManagerEX._instance.LoadScene(questData._reward._ToScene);
        }
    }

    void GetQuestPreced(QuestData questData)
    {
        PrecedType type = questData._preced._type;

        if (type.HasFlag(PrecedType.Exp))
        {

        }
        if (type.HasFlag(PrecedType.Gold))
        {

        }
        if (type.HasFlag(PrecedType.Object))
        {
            foreach (GameObject obj in questData._preced._objLst)
            {
                obj.SetActive(true);
            }
        }
        if (type.HasFlag(PrecedType.DisableObj))
        {
            foreach (GameObject obj in questData._preced._disAbleObjLst)
            {
                obj.SetActive(false);
            }
        }
        if (type.HasFlag(PrecedType.Collider))
        {
            questData._preced._coll.enabled = true;
        }
        
        if (type.HasFlag(PrecedType.PlayType))
        {
            GameManager._instance.Playstate = questData._preced._playerState;
        }
        if (type.HasFlag(PrecedType.Dialogue))
        {
            DialogueManager._instance.GetQuestDialogue(questData, questData._preced._dialogue);
        }
        if (type.HasFlag(PrecedType.ChangeScene))
        {
            SceneManagerEX._instance.LoadScene(questData._preced._ToScene);
        }
    }
    private void OnDestroy()
    {
        _quests = null; // ���� �� Scene���� �� ������ ����Ʈ�� �����ϹǷ� �׳� �ٷ� �ı���Ų��.

        /*
        _processQuestDict.Clear(); // �� ������ Scene�� ��ȯ�Ǹ� �� ������ �ǵ��� ���� �����Ƿ�, => �ٵ� �׾ �ٽ� �����Ѵٸ�??
        _questObjDict.Clear();
        _processQeustLst.Clear();
        */
    }
}
