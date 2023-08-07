using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public int _monsterID; // 죽여야 하는 몬스터의 ID

    public int _totalCount; // 죽여야 하는 몬스터의 수
    public int _nowCount; // 플레이어가 현재 죽인 몬스터의 수

    public bool _isFull; // 플레이어가 몬스터를 다 죽였는지
}

[System.Serializable]
public class KilledMonsterQuestData : QuestData
{
    public List<MonsterData> _monsterLst; // 가져와야 하는 오브젝트 리스트
}
