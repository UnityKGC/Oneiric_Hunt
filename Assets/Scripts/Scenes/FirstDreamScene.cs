using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterSpawnData // 몬스터가 소환될 위치와 정보를 지니고 있다.
{
    public SpawnManager.MonsterType type;
    public Transform _pos;
}
public class FirstDreamScene : MonoBehaviour
{
    
    [SerializeField] List<DreamEvtTrigger> _triggers = new List<DreamEvtTrigger>(); // 이벤트 트리거를 지닌 오브젝트 들 => 후에 호출된 Trigger와 _triggers를 foreach를 사용하여 알맞는 trigger를 찾은 다음, 해당하는 순차에 맞는 몬스터들을 위치에 배치시킨다.

    [SerializeField] List<MonsterSpawnData> _spawnDataList_0 = new List<MonsterSpawnData>();
    [SerializeField] List<MonsterSpawnData> _spawnDataList_1 = new List<MonsterSpawnData>();
    [SerializeField] List<MonsterSpawnData> _spawnDataList_2 = new List<MonsterSpawnData>();

    private List<List<MonsterSpawnData>> _spawnList = new List<List<MonsterSpawnData>>(); // SpawnDataList의 List Dict에 쉽게 DataList들을 넣기 위해 만든 변수.. => 중첩 리스트인데... 더 좋은 방법이 없을까???

    private Dictionary<int, List<MonsterSpawnData>> _spawnDict = new Dictionary<int, List<MonsterSpawnData>>(); // _spawnList

    [SerializeField] QuestData _questData;

    bool _isBattle; // 배틀 중인가 아닌가.

    [SerializeField] private GameObject _exitPortal;
    void Start()
    {
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.FirstDreamScene;

        InitSpawnList();
        InitDict();

        CameraManager._instance.SetFreeLookCam(); // 시작 시 플레이어 카메라로 이동하게끔

        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Play); // ㅋㅋ 이걸 안해서 그런거였네ㅋㅋㅋㅋㅋ
        GameManager._instance.Playstate = GameManager.PlayState.Dream_Normal;

        Invoke("StartQuest", 1f);
    }

    void StartQuest()
    {
        DialogueManager._instance.GetQuestDialogue(_questData, _questData._dialogueData[0]);
    }
    void Update()
    {
        
    }
    void InitSpawnList() // SpawnList초기화
    {
        _spawnList.Add(_spawnDataList_0); // 하나하나씩 더해야 하는게 너무 귀찮다... 더 좋은 방법이 없을까??
        _spawnList.Add(_spawnDataList_1);
        _spawnList.Add(_spawnDataList_2);
    }
    void InitDict() // Dict 초기화
    {
        for(int i = 0; i < _spawnList.Count; i++)
        {
            _spawnDict.Add(i, _spawnList[i]);
        }
    }

    public void GetTriggerEvt(DreamEvtTrigger trigger, bool isLastBattle) // 플레이어를 감지한 trigger를 인자로 전달 받음
    {
        for(int i = 0; i < _triggers.Count; i++) // Scene이 관리하는 Trigger들을 전부 for문을 돌아 확인하여
        {
            if (_triggers[i] == trigger) // 인자로 받은 trigger를 찾으면
            {
                SpawnManager._instance.SpawnMonster(_spawnDict[i]); // 그 순서(몇 번째)에 알맞은 몬스터들을 스폰해줌.
            }
        }
        StartBattle(isLastBattle); // 다 소환했으니 전투 시작
    }
    public void EnablePortal(SceneManagerEX.PortalType portalType)
    {
        switch(portalType)
        {
            case SceneManagerEX.PortalType.ExitPortal:
                _exitPortal.SetActive(true);
                break;
        }
    }
    void StartBattle(bool isLastBattle) // Trigger를 받았으니, 전투 시작이다! 라고 전달만 해줌
    {
        BattleManager._instance.StartBattle(isLastBattle);
    }
}
