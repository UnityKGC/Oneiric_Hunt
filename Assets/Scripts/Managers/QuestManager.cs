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

    public Action<int> _objEffectEvt = null; // 현재 TargetUIObj가 이걸 사용하고 있음 => 인자로 받은 ID와 본인 부모의 ID가 같으면, TargetArrowUI에게 본인 차례라고 전달함
    public Action<int> _npcIDEvt = null; // NPC에게로 돌아갈 경우 호출.

    public Action<QuestMark, int> _questMarkEvt = null; // 플레이어가 무언가를 하여 퀘스트 마크를 변경해야한다면, => NPC에게 퀘스트 마크 변경해라고 전달.

    public Action<bool> _targetArrowEvt = null; // 목표 위치를 가르키는 가이드 UI

    Dictionary<int, QuestData> _processQuestDict = new Dictionary<int, QuestData>(); // 현재 진행중인 퀘스트 Dict

    Dictionary<int, List<QuestData>> _questObjDict = new Dictionary<int, List<QuestData>>();

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
    
    public void StartQuest(int questID)
    {

    }

    public void StartQuest(QuestData questData) // 플레이어가 해당 퀘스트 시작함. => 퀘스트 시작은 Inspctor에서 기입한 데이터를 가져와야 하므로, QuestData를 가져온다.
    {
        if (questData._isStart || questData._isFinish) return; // 이미 시작된 퀘스트 혹은 이미 끝난 퀘스트라면 리턴

        questData._isStart = true;

        InitQuestObjDict(questData);

        _processQuestDict.Add(questData._questID, questData);
        _processQeustLst.Add(questData);

        UIManager._instacne.StartQuest(questData);

        GetQuestPreced(questData); // 퀘스트 선행보상 획득

        // 해당 퀘스트의 타입을 확인 후, => 알맞는 퀘스트를 시작하게 만듬 => 일단 전부 다 같이 받도록 하자.
    }

    // count가 증가하면, 그 함수에서 
    // 파이브라인으로 만들어, 0번째 제목, 1번째 퀘스트 내용 이런식으로 string 리스트로 만든 후 리턴해주는 게 좋을라나?
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

                _questMarkEvt?.Invoke(QuestMark.Finish, data._objID); // 오브젝트는 바로 ?마크 활성화
                _questMarkEvt?.Invoke(QuestMark.Preced, questData._npcID); // NPC는 ... 마크 활성화

                _objEffectEvt?.Invoke(data._objID);
            }
            else
                _questObjDict[data._objID].Add(questData);
        }

        if (questData._questType == QuestType.KillMonster)
            BattleManager._instance.StartBattle();
        else if(questData._questType == QuestType.KillBossMonster)
            BattleManager._instance.StartBattle(true); // 보스 몬스터는 이렇게.
    }
    void InitTriggerQuest(QuestData questData)
    {
        foreach (TriggerData data in questData._triggerDatas)
        {
            if (!_questObjDict.ContainsKey(data._objID))
            {
                _questObjDict.Add(data._objID, new List<QuestData>() { questData });

                _questMarkEvt?.Invoke(QuestMark.Finish, data._objID); // 오브젝트는 바로 ?마크 활성화
                _questMarkEvt?.Invoke(QuestMark.Preced, questData._npcID); // NPC는 ... 마크 활성화

                _objEffectEvt?.Invoke(data._objID);
            }
            else
                _questObjDict[data._objID].Add(questData);
        }
    }
    
    public bool QuestTrigger(int id) // id가 해당하는 퀘스트를 플레이어가 받고 있으면, true리턴, 없으면 false리턴.
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
            if (dataLst.Count <= 0) break; // 한바퀴 돌고 dataLst에 데이터가 없으면 멈춤 => break안하면 에러발생
        }
        return true;
    }
    void ObjectQuest(List<QuestData> dataLst, QuestData data, int id) // 퀘스트 데이터와 오브젝트 id를 확인하여 trigger확인
    {
        foreach (ObjectData objData in data._objLst)
        {
            if (objData._objID != id || objData._isFull) continue; // objID가 맞지 않거나, 이미 완료 조건이 만족한 퀘스트라면 넘김

            objData._nowCount++;

            if (objData._totalCount <= objData._nowCount)
            {
                objData._isFull = true;
                _questMarkEvt?.Invoke(QuestMark.None, objData._objID); // 오브젝트는 얻으면 필요개수를 다 채웠으면 비활성화
            }
                
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
            
            _questMarkEvt?.Invoke(QuestMark.Finish, data._npcID); // 퀘스트 완료조건이 만족하므로, NPC에게 ? 퀘스트 마크 세팅

            _npcIDEvt.Invoke(data._npcID); // 완료가능이면, 퀘스트를 받은 NPC에게 NPCID를 뿌려, 본인의 ID와 동일하면, 가이드라인UI에게 본인이라고 알려준다

            if (data._questType == QuestType.KillMonster || data._questType == QuestType.KillBossMonster || data._questType == QuestType.InteractionObject) // 만약 몬스터 퇴치 퀘스트라면
            {
                data._isFinish = true; // 바로 퀘스트 끝내기
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
            _questMarkEvt?.Invoke(QuestMark.None, triData._objID); // Trigger오브젝트는 얻으면 비활성화
        }

        bool isAchieve = true; // 퀘스트 완료 조건 만족여부 => Default를 true로 준다.

        // objLst의 모든 _isFull이 true라면 진행하도록
        foreach (TriggerData triData in data._triggerDatas)
        {
            if (!triData._isFinish) // 단 하나라도 isFull이 false라면, 아직 해당 퀘스트가 완료되지 않았으므로, 멈춤
            {
                isAchieve = false;
                break;
            }
        }

        UIManager._instacne.UpdateQuestContent(data); // 퀘스트UI 갱신

        if (isAchieve)
        {
            data._isAchieve = true; // Object리스트의 isFull이 모두 true라면, 퀘스트 완료조건이 만족하므로, isAchieve를 true;
            data._isFinish = true; // 바로 퀘스트 끝내기
            FinishQuest(data);
        }
    }
    public void FinishQuest(QuestData questData) // 퀘스트 끝냄
    {
        if (!questData._isFinish) return; // isFinish가 false라면 아직 끝난게 아니니까 리턴

        if (_quests != null)
            _quests.Invoke(questData);

        UIManager._instacne.FinishQuest(questData); // 퀘스트 UI 갱신 => 끝냄

        _targetArrowEvt?.Invoke(false); // 가이드 라인 UI 비활성화

        _questMarkEvt?.Invoke(QuestMark.None, questData._npcID); // 퀘스트 완료 시, 마크 비활성화 => NPC전용 => 오브젝트들은 QuestTrigger에서 이미 없어짐.

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

        PluginManager._instance.GetToastMessage("퀘스트 완료");
    }

    void FinishObjQuest(QuestData questData)
    {
        foreach (ObjectData objData in questData._objLst) // 해당 퀘스트의 objLst들을 확인해서, id를 통해 해당 퀘스트를 제거한다. => 굳이 foreach를 써야할까? 어차피 하나의 id만으로 퀘스트 자체를 지울 수 있으니까...?
        {
            if (_questObjDict.TryGetValue(objData._objID, out List<QuestData> objLst))
            {
                objLst.Remove(questData);
                break;
            }
        }
        
        if(questData._questType == QuestType.KillMonster)
            BattleManager._instance.EndBattle(); // 어차피 모든 전투 퀘스트는 바로 끝내므로, 전투 종료

        // 퀘스트 보상
        GetQuestReward(questData);

        if (_processQuestDict.ContainsKey(questData._questID)) // 진행중인 퀘스트 Dict에 퀘스트가 존재한다면,
            _processQuestDict.Remove(questData._questID); // Dict에 해당 퀘스트를 지워준다.


    }
    void FinishTriggerQuest(QuestData questData)
    {
        foreach (TriggerData triData in questData._triggerDatas) // 해당 퀘스트의 objLst들을 확인해서, id를 통해 해당 퀘스트를 제거한다. => 굳이 foreach를 써야할까? 어차피 하나의 id만으로 퀘스트 자체를 지울 수 있으니까...?
        {
            if (_questObjDict.TryGetValue(triData._objID, out List<QuestData> objLst))
            {
                objLst.Remove(questData);
                break;
            }
        }

        // 퀘스트 보상
        GetQuestReward(questData);

        if (_processQuestDict.ContainsKey(questData._questID)) // 진행중인 퀘스트 Dict에 퀘스트가 존재한다면,
            _processQuestDict.Remove(questData._questID); // Dict에 해당 퀘스트를 지워준다.

        
    }

    // ID의 알맞는 퀘스트가 현재 진행중인지 확인
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
        _quests = null; // 차피 한 Scene에는 한 종류의 퀘스트만 존재하므로 그냥 바로 파괴시킨다.

        /*
        _processQuestDict.Clear(); // 내 게임은 Scene이 전환되면 그 전으로 되돌아 가지 않으므로, => 근데 죽어서 다시 시작한다면??
        _questObjDict.Clear();
        _processQeustLst.Clear();
        */
    }
}
