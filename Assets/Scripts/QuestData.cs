using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QuestType
{
    None = -1,
    BringObject, // 물건 가져오기
    FindClue, // 단서 찾기
    KillMonster, // 몬스터 퇴치
    KillBossMonster,
    InteractionObject, // 상호작용 하기.(NPC와 대화하기, 오브젝트 들고, 밀기, 등등등)
    GoToPosition, // 해당 위치로 이동하기.
    Max,
}

[System.Serializable]
public class TriggerData
{
    public int _objID; // 수행해야 할 objID

    public string _objName; // 원래는 매니저 같은 곳에서 _objID를 토대로 오브젝트 정보를 가져오게 해야하나

    public bool _isFinish; // 해당 오브젝트에 Trigger했는지,
}
[System.Serializable]
public class ObjectData
{
    public int _objID; // 수행해야 할 objID

    public string _objName; // 원래는 매니저 같은 곳에서 _objID를 토대로 오브젝트 정보를 가져오게 해야하나

    public int _totalCount; // 수행해야 할 전체 개수
    public int _nowCount; // 플레이어가 수행한 현재 개수

    public bool _isFull; // 플레이어가 모두 다 했는지
}

[System.Serializable]
public class QuestData // 후에, ObjectData나 TriggerData는 QuestData를 상속받게 하는것도 괜찮을 듯?
{
    public int _questID; // 퀘스트 ID

    public QuestType _questType; // 퀘스트 타입

    public string _questName; // 퀘스트 이름

    public string _questContentText;

    public List<DialogueData> _dialogueData; // 이 퀘스트의 다이얼로그 대사

    public List<ObjectData> _objLst; // 수행해야 할 퀘스트의 오브젝트 리스트

    public List<TriggerData> _triggerDatas; // 특별한 조건없이 클리어 되는 퀘스트 데이터.

    public QuestPreceds _preced; // 퀘스트 시작 시, 선행 보상.
    public QuestRewards _reward; // 퀘스트 보상

    public bool _isStart; // 퀘스트를 시작했는지,

    public bool _isAchieve; // 퀘스트 달성 조건을 만족했는지, 

    public bool _isFinish; // 퀘스트를 끝냈는지,

}

[Flags]
public enum RewardType
{
    None = -1,
    Exp = 1 << 0,
    Gold = 1 << 1,
    Object = 1 << 2,
    Collider = 1 << 3,
    Dialogue = 1 << 4,
    PlayType = 1<< 5,
    DisableObj = 1 << 6,
    ChangeScene = 1 << 7,
}

[System.Serializable]
public class QuestRewards
{
    public RewardType _type;
    public List<GameObject> _objLst; // 퀘스트 완료 시 특정 오브젝트 활성화
    public List<GameObject> _disAbleObjLst;
    public Collider _coll; // 퀘스트 완료 시 콜라이더 활성화 => 이건 임시 => 플레이어 집에서 문 열때 필요
    public DialogueData _dialogue;
    public SceneManagerEX.SceneType _ToScene; // 이동하고자 하는 Scene
    public GameManager.PlayState _playerState;
    public int _gold;
    public float _exp;

}

[Flags]
public enum PrecedType
{
    None = -1,
    Exp = 1 << 0,
    Gold = 1 << 1,
    Object = 1 << 2,
    Collider = 1 << 3,
    Dialogue = 1 << 4,
    PlayType = 1 << 5,
    DisableObj = 1 << 6,
    ChangeScene = 1 << 7,
}

[System.Serializable]
public class QuestPreceds
{
    public PrecedType _type;
    public List<GameObject> _objLst; // 퀘스트 완료 시 특정 오브젝트 활성화
    public List<GameObject> _disAbleObjLst;
    public Collider _coll; // 퀘스트 완료 시 콜라이더 활성화 => 이건 임시 => 플레이어 집에서 문 열때 필요
    public DialogueData _dialogue; // 퀘스트 시작 시, 다이얼로그가 필요할 때
    public SceneManagerEX.SceneType _ToScene; // 이동하고자 하는 Scene
    public GameManager.PlayState _playerState;
    public int _gold;
    public float _exp;
    
}