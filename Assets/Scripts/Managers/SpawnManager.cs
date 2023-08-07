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
    
    public void SpawnMonster(List<MonsterSpawnData> spawnData) // ��ġ����Ʈ�� �޾�, �ش� ��ġ�� ���� ��ȯ��ų���� ���� ������ ���� ��, �ش� ���͸� ��ȯ�Ѵ�.
    {
        foreach(MonsterSpawnData data in spawnData)
        {
            _nowMonster.Add(Instantiate(_monsterList[(int)data.type], data._pos.position, Quaternion.identity));
        }
    }
    public void KilledMonster() // ���Ͱ� ���� �� �� �Լ��� ȣ��
    {
        _killedMonsterCount++;
        if(_killedMonsterCount == _nowMonster.Count)
        {
            AllKilledMonster();
            BattleManager._instance.EndBattle();
        }
    }

    public void AllKilledMonster() // ���͸� �� ���̸�. ���� �ʱ�ȭ ��Ŵ
    {
        _nowMonster.Clear();
        _killedMonsterCount = 0;
    }
}
