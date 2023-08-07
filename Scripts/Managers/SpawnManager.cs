using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum MonsterType
    {
        Monster,
        Boss,
    }
    public static SpawnManager _instance;

    [SerializeField]
    List<GameObject> _monsterList = new List<GameObject>();

    List<GameObject> _nowMonster = new List<GameObject>();

    [SerializeField] private int _killedMonsterCount;

    bool _isLastSpawn;
    private void Awake()
    {
        _instance = this;
    }
    
    public void SpawnMonster(List<MonsterSpawnData> spawnData) // 위치리스트를 받아, 해당 위치에 누굴 소환시킬지에 대한 정보도 받은 후, 해당 몬스터를 소환한다.
    {
        foreach(MonsterSpawnData data in spawnData)
        {
            _nowMonster.Add(Instantiate(_monsterList[(int)data.type], data._pos.position, Quaternion.identity));
        }
    }
    public void KilledMonster() // 몬스터가 죽을 때 이 함수를 호출
    {
        _killedMonsterCount++;
        if(_killedMonsterCount == _nowMonster.Count)
        {
            AllKilledMonster();
            BattleManager._instance.EndBattle();
        }
    }

    public void AllKilledMonster() // 몬스터를 다 죽이면. 변수 초기화 시킴
    {
        _nowMonster.Clear();
        _killedMonsterCount = 0;
    }
}
