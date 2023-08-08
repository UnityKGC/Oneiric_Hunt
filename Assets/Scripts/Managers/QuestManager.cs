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
    
    Dictionary<int, QuestData> _processQuestDict = new Dictionary<int, QuestData>(); // 현재 진행중인 퀘스트 Dict

    Dictionary<int, List<QuestData>> _questObjDict = new Dictionary<int, List<QuestData>>();

    /*
    Dictionary<int, List<KilledMonsterQuestData>> _monsterQuestDict = new Dictionary<int, List<KilledMonsterQuestData>>(); // 몬스터 퇴치 퀘스트 Dict => Key값으로 몬스터 ID를 지닌다.

    Dictionary<int, List<BringQuestData>> _bringQuestDict = new Dictionary<int, List<BringQuestData>>(); // 물건 가져오는 퀘스트 dict => Key값으로 오브젝트 ID를 지닌다.
    */

    List<QuestData> _processQeustLst = new List<QuestData>(); // 현재 진행중인 퀘스트? => 현재는 일단 그냥 만들어 놓기만 함, 후에 필요(사용)하면 이 주석 지울 예정
    

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
    
    
    public void StartQuest(QuestData questData) // 플레이어가 해당 퀘스트 시작함. => 퀘스트 시작은 Inspctor에서 기입한 데이터를 가져와야 하므로, QuestData를 가져온다.
    {
        if (questData._isStart || questData._isFinish) return; // 이미 시작된 퀘스트 혹은 이미 끝난 퀘스트라면 리턴

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
        
        // 해당 퀘스트의 타입을 확인 후, => 알맞는 퀘스트를 시작하게 만듬 => 일단 전부 다 같이 받도록 하자.
    }

    // count가 증가하면, 그 함수에서 
    // 파이브라인으로 만들어, 0번째 제목, 1번째 퀘스트 내용 이런식으로 string 리스트로 만든 후 리턴해주는 게 좋을라나?
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
    void InitMonsterQuestDict(KilledMonsterQuestData questData) // 몬스터 퇴치 퀘스트 Dict을 초기화
    {
        foreach(MonsterData monsterData in questData._monsterLst)
        {
            if (!_monsterQuestDict.ContainsKey(monsterData._monsterID)) // 만약, 해당 MonsterID에 해당하는 Key가 아직 없다면, 리스트 생성 후 등록
                _monsterQuestDict.Add(monsterData._monsterID, new List<KilledMonsterQuestData>() { questData });
            else // 이미 Key가 존재한다면
                _monsterQuestDict[monsterData._monsterID].Add(questData); // 해당 리스트에 등록
        }
    }
    void InitBringQuestDict(BringQuestData questData) // 물건 가져오는 퀘스트 Dict을 초기화
    {
        // 해당 퀘스트의 가져와야 하는 오브젝트 리스트를 foreach문을 이용해서 오브젝트 ID를 Key로 등록시킨다. => 그럼, 오브젝트 ID가 같은 퀘스트를 받으면 어떡하냐?? 일단 내 게임에는 그런일은 없다. 후에 그런일이 있다고 가정하면, 인벤토리에 아이템을 등록시키고, 인벤토리를 판단하게 만들거다.
        foreach (ObjectData objData in questData._objLst) 
        {
            if (!_bringQuestDict.ContainsKey(objData._objID)) // 만약, 해당 MonsterID에 해당하는 Key가 아직 없다면, 리스트 생성 후 등록
                _bringQuestDict.Add(objData._objID, new List<BringQuestData>() { questData });
            else // 이미 Key가 존재한다면
                _bringQuestDict[objData._objID].Add(questData); // 해당 리스트에 등록
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
                if (objData._objID != id || objData._isFull) continue; // objID가 맞지 않거나, 이미 완료 조건이 만족한 퀘스트라면 넘김

                objData._nowCount++;

                if (objData._totalCount <= objData._nowCount)
                    objData._isFull = true;
            }

            bool isAchieve = true; // 퀘스트 완료 조건 만족여부 => Default를 true로 준다.

            // objLst의 모든 _isFull이 true라면 진행하도록
            foreach (ObjectData objData in data._objLst)
            {
                if (!objData._isFull) // 단 하나라도 isFull이 false라면, 아직 해당 퀘스트가 완료되지 않았으므로, 멈춤
                {
                    isAchieve = false;
                    break;
                }
            }

            UIManager._instacne.UpdateQuestContent(data); // 퀘스트UI 갱신

            if (isAchieve)
            {
                data._isAchieve = true; // Object리스트의 isFull이 모두 true라면, 퀘스트 완료조건이 만족하므로, isAchieve를 true;
                if (data._questType == QuestType.KillMonster) // 만약 몬스터 퇴치 퀘스트라면
                {
                    data._isFinish = true; // 바로 퀘스트 끝내기
                    FinishQuest(data);
                }
            }

            if (dataLst.Count <= 0) // dataLst에 더 이상 데이터가 없다면 => 몬스터 퇴치 퀘스트만 있었는데 끝났다면 => foreach종료
                break;
        }
    }
    /*
    public void BringQuestTrigger(int objID) // 물건 가져오는 퀘스트를 진행할 때 호출 => 플레이어가 물건을 가졌을 때
    {
        List<BringQuestData> dataLst = null;

        if (!_bringQuestDict.ContainsKey(objID)) return; // 아이템이 Key로 등록되어 있지 않다면 => 해당 퀘스트가 없다면 리턴

        dataLst = _bringQuestDict[objID]; // 있으면 등록


        foreach (BringQuestData data in dataLst)
        {
            foreach(ObjectData objData in data._objLst)
            {
                if (objData._objID != objID || objData._isFull) continue; // objID가 맞지 않거나, 이미 완료 조건이 만족한 퀘스트라면 넘김
                
                objData._nowCount++;

                if (objData._totalCount <= objData._nowCount)
                    objData._isFull = true;
            }

            UIManager._instacne.GetBringQuestContent(data);

            bool isAchieve = true; // 퀘스트 완료 조건 만족여부 => Default를 true로 준다.

            // objLst의 모든 _isFull이 true라면 진행하도록
            foreach (ObjectData objData in data._objLst)
            {
                if (!objData._isFull) // 단 하나라도 isFull이 false라면, 아직 해당 퀘스트가 완료되지 않았으므로, 멈춤
                {
                    isAchieve = false;
                    break;
                }
            }

            if (isAchieve)
                data._isAchieve = true; // Object리스트의 isFull이 모두 true라면, 퀘스트 완료조건이 만족하므로, isAchieve를 true;
        }
    }
    
    public void MonsterQuestTrigger(int monsterID) // 몬스터 퇴치 퀘스트를 진행할 때 호출 => 몬스터를 퇴치하였을 때,
    {
        List<KilledMonsterQuestData> dataLst = null;

        if (!_monsterQuestDict.ContainsKey(monsterID)) // 몬스터 퀘스트 리스트를 가져왔는데, 가져온 값이 없다면 리턴
            return; 
            
        dataLst = _monsterQuestDict[monsterID]; // 있으면 몬스터 퇴치 퀘스트 Dict에 몬스터id를 key로 하는 value를 가져온다.

        foreach(KilledMonsterQuestData data in dataLst) // 있다면, 퀘스트 하나하나마다 몬스터 처치 횟수 늘리기 => 여기서 에러뜨던데?
        {
            foreach(MonsterData monsterData in data._monsterLst)
            {
                if (monsterData._monsterID != monsterID || monsterData._isFull) continue;

                monsterData._nowCount++;

                if(monsterData._totalCount <= monsterData._nowCount)
                    monsterData._isFull = true;
            }

            bool isAchieve = true; // 퀘스트 완료 조건 만족여부 => Default를 true로 준다.

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
                data._isFinish = data._isAchieve = true; // 몬스터 퇴치 퀘스트는 바로 끝낸다.
                FinishQuest(data);
            }

            if (dataLst.Count <= 0) // 위에서 Finish된 Quest가 제거되어, dataLst가 0이된다면, foreach문이 오류가 발생하므로 미리 break시킨다.
                break;
        }
    }
    */
    public void FinishQuest(QuestData questData) // 퀘스트 끝냄
    {
        if (!questData._isFinish) return; // isFinish가 false라면 아직 끝난게 아니니까 리턴

        if (_quests != null)
            _quests.Invoke(questData);

        UIManager._instacne.FinishQuest(questData); // 퀘스트 UI 갱신 => 끝냄

        foreach (ObjectData objData in questData._objLst) // 해당 퀘스트의 objLst들을 확인해서, id를 통해 해당 퀘스트를 제거한다. => 굳이 foreach를 써야할까? 어차피 하나의 id만으로 퀘스트 자체를 지울 수 있으니까...?
        {
            if (_questObjDict.TryGetValue(objData._objID, out List<QuestData> objLst))
            {
                objLst.Remove(questData);

                break;
            }
        }



        if (_processQuestDict.ContainsKey(questData._questID)) // 진행중인 퀘스트 Dict에 퀘스트가 존재한다면,
            _processQuestDict.Remove(questData._questID); // Dict에 해당 퀘스트를 지워준다.

        /*
        switch (questData._questType) // 퀘스트 타입에 따라서, 해당하는 Dict에 존재하는 퀘스트를 제거해준다.
        {
            case QuestType.BringObject:
                BringQuestData bd = (BringQuestData)questData;

                foreach(ObjectData objData in bd._objLst) // 해당 퀘스트의 objLst들을 확인해서, id를 통해 해당 퀘스트를 제거한다. => 굳이 foreach를 써야할까? 어차피 하나의 id만으로 퀘스트 자체를 지울 수 있으니까...?
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

                foreach (MonsterData monsterData in kd._monsterLst) // 해당 퀘스트의 objLst들을 확인해서, id를 통해 해당 퀘스트를 제거한다. => 굳이 foreach를 써야할까? 어차피 하나의 id만으로 퀘스트 자체를 지울 수 있으니까...?
                {
                    if (_monsterQuestDict.TryGetValue(monsterData._monsterID, out List<KilledMonsterQuestData> kdLst))
                    {
                        kdLst.Remove(kd);

                        if (kdLst.Count <= 0) // 해당 퀘스트를 지우고, Key(몬스터 ID)에 해당하는 퀘스트가 더 이상 없다면,
                            _monsterQuestDict.Remove(monsterData._monsterID);
                        
                        break;
                    }   
                }
                break;
        }

        if (_processQuestDict.ContainsKey(questData._questID)) // 진행중인 퀘스트 Dict에 퀘스트가 존재한다면,
            _processQuestDict.Remove(questData._questID); // Dict에 해당 퀘스트를 지워준다.

        */
    }

    // ID의 알맞는 퀘스트가 현재 진행중인지 확인
    public bool CheckQuest(int questID) {   return _processQuestDict.ContainsKey(questID);  }
    
    private void OnDestroy()
    {
        _quests = null; // 차피 한 Scene에는 한 종류의 퀘스트만 존재하므로 그냥 바로 파괴시킨다.
    }
}
