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

    /*
    Dictionary<int, List<KilledMonsterQuestData>> _monsterQuestDict = new Dictionary<int, List<KilledMonsterQuestData>>(); // ���� ��ġ ����Ʈ Dict => Key������ ���� ID�� ���Ѵ�.

    Dictionary<int, List<BringQuestData>> _bringQuestDict = new Dictionary<int, List<BringQuestData>>(); // ���� �������� ����Ʈ dict => Key������ ������Ʈ ID�� ���Ѵ�.
    */

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
    
    
    public void StartQuest(QuestData questData) // �÷��̾ �ش� ����Ʈ ������. => ����Ʈ ������ Inspctor���� ������ �����͸� �����;� �ϹǷ�, QuestData�� �����´�.
    {
        if (questData._isStart || questData._isFinish) return; // �̹� ���۵� ����Ʈ Ȥ�� �̹� ���� ����Ʈ��� ����

        questData._isStart = true;

        InitQuestObjDict(questData);

        /*
        switch(questData._questType)
        {
            case QuestType.BringObject:
                InitBringQuestDict(questData as BringQuestData);
                break;

            case QuestType.FindClue:

                break;

            case QuestType.KillMonster:
                InitMonsterQuestDict(questData as KilledMonsterQuestData);
                break;
        }
        */
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
    /*
    void InitMonsterQuestDict(KilledMonsterQuestData questData) // ���� ��ġ ����Ʈ Dict�� �ʱ�ȭ
    {
        foreach(MonsterData monsterData in questData._monsterLst)
        {
            if (!_monsterQuestDict.ContainsKey(monsterData._monsterID)) // ����, �ش� MonsterID�� �ش��ϴ� Key�� ���� ���ٸ�, ����Ʈ ���� �� ���
                _monsterQuestDict.Add(monsterData._monsterID, new List<KilledMonsterQuestData>() { questData });
            else // �̹� Key�� �����Ѵٸ�
                _monsterQuestDict[monsterData._monsterID].Add(questData); // �ش� ����Ʈ�� ���
        }
    }
    void InitBringQuestDict(BringQuestData questData) // ���� �������� ����Ʈ Dict�� �ʱ�ȭ
    {
        // �ش� ����Ʈ�� �����;� �ϴ� ������Ʈ ����Ʈ�� foreach���� �̿��ؼ� ������Ʈ ID�� Key�� ��Ͻ�Ų��. => �׷�, ������Ʈ ID�� ���� ����Ʈ�� ������ ��ϳ�?? �ϴ� �� ���ӿ��� �׷����� ����. �Ŀ� �׷����� �ִٰ� �����ϸ�, �κ��丮�� �������� ��Ͻ�Ű��, �κ��丮�� �Ǵ��ϰ� ����Ŵ�.
        foreach (ObjectData objData in questData._objLst) 
        {
            if (!_bringQuestDict.ContainsKey(objData._objID)) // ����, �ش� MonsterID�� �ش��ϴ� Key�� ���� ���ٸ�, ����Ʈ ���� �� ���
                _bringQuestDict.Add(objData._objID, new List<BringQuestData>() { questData });
            else // �̹� Key�� �����Ѵٸ�
                _bringQuestDict[objData._objID].Add(questData); // �ش� ����Ʈ�� ���
        }
    }
    */
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
    /*
    public void BringQuestTrigger(int objID) // ���� �������� ����Ʈ�� ������ �� ȣ�� => �÷��̾ ������ ������ ��
    {
        List<BringQuestData> dataLst = null;

        if (!_bringQuestDict.ContainsKey(objID)) return; // �������� Key�� ��ϵǾ� ���� �ʴٸ� => �ش� ����Ʈ�� ���ٸ� ����

        dataLst = _bringQuestDict[objID]; // ������ ���


        foreach (BringQuestData data in dataLst)
        {
            foreach(ObjectData objData in data._objLst)
            {
                if (objData._objID != objID || objData._isFull) continue; // objID�� ���� �ʰų�, �̹� �Ϸ� ������ ������ ����Ʈ��� �ѱ�
                
                objData._nowCount++;

                if (objData._totalCount <= objData._nowCount)
                    objData._isFull = true;
            }

            UIManager._instacne.GetBringQuestContent(data);

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

            if (isAchieve)
                data._isAchieve = true; // Object����Ʈ�� isFull�� ��� true���, ����Ʈ �Ϸ������� �����ϹǷ�, isAchieve�� true;
        }
    }
    
    public void MonsterQuestTrigger(int monsterID) // ���� ��ġ ����Ʈ�� ������ �� ȣ�� => ���͸� ��ġ�Ͽ��� ��,
    {
        List<KilledMonsterQuestData> dataLst = null;

        if (!_monsterQuestDict.ContainsKey(monsterID)) // ���� ����Ʈ ����Ʈ�� �����Դµ�, ������ ���� ���ٸ� ����
            return; 
            
        dataLst = _monsterQuestDict[monsterID]; // ������ ���� ��ġ ����Ʈ Dict�� ����id�� key�� �ϴ� value�� �����´�.

        foreach(KilledMonsterQuestData data in dataLst) // �ִٸ�, ����Ʈ �ϳ��ϳ����� ���� óġ Ƚ�� �ø��� => ���⼭ �����ߴ���?
        {
            foreach(MonsterData monsterData in data._monsterLst)
            {
                if (monsterData._monsterID != monsterID || monsterData._isFull) continue;

                monsterData._nowCount++;

                if(monsterData._totalCount <= monsterData._nowCount)
                    monsterData._isFull = true;
            }

            bool isAchieve = true; // ����Ʈ �Ϸ� ���� �������� => Default�� true�� �ش�.

            foreach(MonsterData monsterData in data._monsterLst)
            {
                if(!monsterData._isFull)
                {
                    isAchieve = false;
                    break;
                }
            }

            if (isAchieve)
            {
                data._isFinish = data._isAchieve = true; // ���� ��ġ ����Ʈ�� �ٷ� ������.
                FinishQuest(data);
            }

            if (dataLst.Count <= 0) // ������ Finish�� Quest�� ���ŵǾ�, dataLst�� 0�̵ȴٸ�, foreach���� ������ �߻��ϹǷ� �̸� break��Ų��.
                break;
        }
    }
    */
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



        if (_processQuestDict.ContainsKey(questData._questID)) // �������� ����Ʈ Dict�� ����Ʈ�� �����Ѵٸ�,
            _processQuestDict.Remove(questData._questID); // Dict�� �ش� ����Ʈ�� �����ش�.

        /*
        switch (questData._questType) // ����Ʈ Ÿ�Կ� ����, �ش��ϴ� Dict�� �����ϴ� ����Ʈ�� �������ش�.
        {
            case QuestType.BringObject:
                BringQuestData bd = (BringQuestData)questData;

                foreach(ObjectData objData in bd._objLst) // �ش� ����Ʈ�� objLst���� Ȯ���ؼ�, id�� ���� �ش� ����Ʈ�� �����Ѵ�. => ���� foreach�� ����ұ�? ������ �ϳ��� id������ ����Ʈ ��ü�� ���� �� �����ϱ�...?
                {
                    if (_bringQuestDict.TryGetValue(objData._objID, out List<BringQuestData> bdLst))
                    {
                        bdLst.Remove(bd);

                        break;
                    }
                }
                break;

            case QuestType.FindClue:

                break;

            case QuestType.KillMonster:
                KilledMonsterQuestData kd = (KilledMonsterQuestData)questData;

                foreach (MonsterData monsterData in kd._monsterLst) // �ش� ����Ʈ�� objLst���� Ȯ���ؼ�, id�� ���� �ش� ����Ʈ�� �����Ѵ�. => ���� foreach�� ����ұ�? ������ �ϳ��� id������ ����Ʈ ��ü�� ���� �� �����ϱ�...?
                {
                    if (_monsterQuestDict.TryGetValue(monsterData._monsterID, out List<KilledMonsterQuestData> kdLst))
                    {
                        kdLst.Remove(kd);

                        if (kdLst.Count <= 0) // �ش� ����Ʈ�� �����, Key(���� ID)�� �ش��ϴ� ����Ʈ�� �� �̻� ���ٸ�,
                            _monsterQuestDict.Remove(monsterData._monsterID);
                        
                        break;
                    }   
                }
                break;
        }

        if (_processQuestDict.ContainsKey(questData._questID)) // �������� ����Ʈ Dict�� ����Ʈ�� �����Ѵٸ�,
            _processQuestDict.Remove(questData._questID); // Dict�� �ش� ����Ʈ�� �����ش�.

        */
    }

    // ID�� �˸´� ����Ʈ�� ���� ���������� Ȯ��
    public bool CheckQuest(int questID) {   return _processQuestDict.ContainsKey(questID);  }
    
    private void OnDestroy()
    {
        _quests = null; // ���� �� Scene���� �� ������ ����Ʈ�� �����ϹǷ� �׳� �ٷ� �ı���Ų��.
    }
}
