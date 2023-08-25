using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterSpawnData // ���Ͱ� ��ȯ�� ��ġ�� ������ ���ϰ� �ִ�.
{
    public SpawnManager.MonsterType type;
    public Transform _pos;
}
public class FirstDreamScene : MonoBehaviour
{
    
    [SerializeField] List<DreamEvtTrigger> _triggers = new List<DreamEvtTrigger>(); // �̺�Ʈ Ʈ���Ÿ� ���� ������Ʈ �� => �Ŀ� ȣ��� Trigger�� _triggers�� foreach�� ����Ͽ� �˸´� trigger�� ã�� ����, �ش��ϴ� ������ �´� ���͵��� ��ġ�� ��ġ��Ų��.

    [SerializeField] List<MonsterSpawnData> _spawnDataList_0 = new List<MonsterSpawnData>();
    [SerializeField] List<MonsterSpawnData> _spawnDataList_1 = new List<MonsterSpawnData>();
    [SerializeField] List<MonsterSpawnData> _spawnDataList_2 = new List<MonsterSpawnData>();

    private List<List<MonsterSpawnData>> _spawnList = new List<List<MonsterSpawnData>>(); // SpawnDataList�� List Dict�� ���� DataList���� �ֱ� ���� ���� ����.. => ��ø ����Ʈ�ε�... �� ���� ����� ������???

    private Dictionary<int, List<MonsterSpawnData>> _spawnDict = new Dictionary<int, List<MonsterSpawnData>>(); // _spawnList

    [SerializeField] QuestData _questData;

    bool _isBattle; // ��Ʋ ���ΰ� �ƴѰ�.

    [SerializeField] private GameObject _exitPortal;
    void Start()
    {
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.FirstDreamScene;

        InitSpawnList();
        InitDict();

        CameraManager._instance.SetFreeLookCam(); // ���� �� �÷��̾� ī�޶�� �̵��ϰԲ�

        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Play); // ���� �̰� ���ؼ� �׷��ſ��פ���������
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
    void InitSpawnList() // SpawnList�ʱ�ȭ
    {
        _spawnList.Add(_spawnDataList_0); // �ϳ��ϳ��� ���ؾ� �ϴ°� �ʹ� ������... �� ���� ����� ������??
        _spawnList.Add(_spawnDataList_1);
        _spawnList.Add(_spawnDataList_2);
    }
    void InitDict() // Dict �ʱ�ȭ
    {
        for(int i = 0; i < _spawnList.Count; i++)
        {
            _spawnDict.Add(i, _spawnList[i]);
        }
    }

    public void GetTriggerEvt(DreamEvtTrigger trigger, bool isLastBattle) // �÷��̾ ������ trigger�� ���ڷ� ���� ����
    {
        for(int i = 0; i < _triggers.Count; i++) // Scene�� �����ϴ� Trigger���� ���� for���� ���� Ȯ���Ͽ�
        {
            if (_triggers[i] == trigger) // ���ڷ� ���� trigger�� ã����
            {
                SpawnManager._instance.SpawnMonster(_spawnDict[i]); // �� ����(�� ��°)�� �˸��� ���͵��� ��������.
            }
        }
        StartBattle(isLastBattle); // �� ��ȯ������ ���� ����
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
    void StartBattle(bool isLastBattle) // Trigger�� �޾�����, ���� �����̴�! ��� ���޸� ����
    {
        BattleManager._instance.StartBattle(isLastBattle);
    }
}
