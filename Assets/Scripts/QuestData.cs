using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QuestType
{
    None,
    BringObject, // 물건 가져오기
    FindClue, // 단서 찾기
    KillMonster, // 몬스터 퇴치
    Max,
}

[System.Serializable]
public class QuestData
{
    public int _questID; // 퀘스트 ID

    public QuestType _questType; // 퀘스트 타입

    public string _questName; // 퀘스트 이름

    public string _questContentText;

    public List<string> _questStartTextList; // 퀘스트 대사 리스트 => 대사 만들면 적용 예정

    public List<string> _questEndTextList; // 퀘스트 대사 리스트 => 대사 만들면 적용 예정

    public List<ObjectData> _objLst; // 수행해야 할 퀘스트의 오브젝트 리스트

    public QuestRewards _reward; // 퀘스트 보상

    public bool _isStart; // 퀘스트를 시작했는지,

    public bool _isAchieve; // 퀘스트 달성 조건을 만족했는지, 

    public bool _isFinish; // 퀘스트를 끝냈는지,

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
public class QuestRewards
{
    public int _gold;
    public float _exp;
}
