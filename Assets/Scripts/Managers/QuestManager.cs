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
        
        // �ش� ����Ʈ�� Ÿ���� Ȯ�� ��, => �˸´� ����Ʈ�� �����ϰ� ���� => �ϴ� ���� �� ���� �޵��� ����.
    }

    // count�� �����ϸ�, �� �Լ����� 
    // ���̺�������� �����, 0��° ����, 1��° ����Ʈ ���� �̷������� string ����Ʈ�� ���� �� �������ִ� �� ������?
    void InitQuestObjDict(QuestData objData)
    {
        foreach(ObjectData data in objData._objLst)
        {
            if (!_questObjDict.ContainsKey(data._objID))
                _questObjDict.Add(data._objID, new List<QuestData>() { objData });
            else
                _questObjDict[data._objID].Add(objData);
        }
    }
    
    public void QuestTrigger(int id)
    {
        List<QuestData> dataLst = null;
        if (!_questObjDict.ContainsKey(id)) return;

        dataLst = _questObjDict[id];

        foreach (QuestData data in dataLst)
        {
            foreach (ObjectData objData in data._objLst)
            {
                if (objData._objID != id || objData._isFull) continue; // objID�� ���� �ʰų�, �̹� �Ϸ� ������ ������ ����Ʈ��� �ѱ�

                objData._nowCount++;

                if (objData._totalCount <= objData._nowCount)
                    objData._isFull = true;
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
                if (data._questType == QuestType.KillMonster) // ���� ���� ��ġ ����Ʈ���
                {
                    data._isFinish = true; // �ٷ� ����Ʈ ������
                    FinishQuest(data);
                }
            }

            if (dataLst.Count <= 0) // dataLst�� �� �̻� �����Ͱ� ���ٸ� => ���� ��ġ ����Ʈ�� �־��µ� �����ٸ� => foreach����
                break;
        }
    }
    public void FinishQuest(QuestData questData) // ����Ʈ ����
    {
        if (!questData._isFinish) return; // isFinish�� false��� ���� ������ �ƴϴϱ� ����

        if (_quests != null)
            _quests.Invoke(questData);

        UIManager._instacne.FinishQuest(questData); // ����Ʈ UI ���� => ����

        foreach (ObjectData objData in questData._objLst) // �ش� ����Ʈ�� objLst���� Ȯ���ؼ�, id�� ���� �ش� ����Ʈ�� �����Ѵ�. => ���� foreach�� ����ұ�? ������ �ϳ��� id������ ����Ʈ ��ü�� ���� �� �����ϱ�...?
        {
            if (_questObjDict.TryGetValue(objData._objID, out List<QuestData> objLst))
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
            questData._reward._obj.SetActive(true);
        }
        if (type.HasFlag(RewardType.Collider))
        {
            questData._reward._coll.enabled = true;
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
