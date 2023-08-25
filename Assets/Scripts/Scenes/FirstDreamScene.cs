using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterSpawnData // 佼什斗亜 社発吃 是帖人 舛左研 走艦壱 赤陥.
{
    public SpawnManager.MonsterType type;
    public Transform _pos;
}
public class FirstDreamScene : MonoBehaviour
{
    
    [SerializeField] List<DreamEvtTrigger> _triggers = new List<DreamEvtTrigger>(); // 戚坤闘 闘軒暗研 走観 神崎詮闘 級 => 板拭 硲窒吉 Trigger人 _triggers研 foreach研 紫遂馬食 硝限澗 trigger研 達精 陥製, 背雁馬澗 授託拭 限澗 佼什斗級聖 是帖拭 壕帖獣轍陥.

    [SerializeField] List<MonsterSpawnData> _spawnDataList_0 = new List<MonsterSpawnData>();
    [SerializeField] List<MonsterSpawnData> _spawnDataList_1 = new List<MonsterSpawnData>();
    [SerializeField] List<MonsterSpawnData> _spawnDataList_2 = new List<MonsterSpawnData>();

    private List<List<MonsterSpawnData>> _spawnList = new List<List<MonsterSpawnData>>(); // SpawnDataList税 List Dict拭 襲惟 DataList級聖 隔奄 是背 幻窮 痕呪.. => 掻淡 軒什闘昔汽... 希 疏精 号狛戚 蒸聖猿???

    private Dictionary<int, List<MonsterSpawnData>> _spawnDict = new Dictionary<int, List<MonsterSpawnData>>(); // _spawnList

    [SerializeField] QuestData _questData;

    bool _isBattle; // 壕堂 掻昔亜 焼観亜.

    [SerializeField] private GameObject _exitPortal;
    void Start()
    {
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.FirstDreamScene;

        InitSpawnList();
        InitDict();

        CameraManager._instance.SetFreeLookCam(); // 獣拙 獣 巴傾戚嬢 朝五虞稽 戚疑馬惟懐

        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Play); // せせ 戚杏 照背辞 益訓暗心革せせせせせ
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
    void InitSpawnList() // SpawnList段奄鉢
    {
        _spawnList.Add(_spawnDataList_0); // 馬蟹馬蟹梢 希背醤 馬澗惟 格巷 瑛諾陥... 希 疏精 号狛戚 蒸聖猿??
        _spawnList.Add(_spawnDataList_1);
        _spawnList.Add(_spawnDataList_2);
    }
    void InitDict() // Dict 段奄鉢
    {
        for(int i = 0; i < _spawnList.Count; i++)
        {
            _spawnDict.Add(i, _spawnList[i]);
        }
    }

    public void GetTriggerEvt(DreamEvtTrigger trigger, bool isLastBattle) // 巴傾戚嬢研 姶走廃 trigger研 昔切稽 穿含 閤製
    {
        for(int i = 0; i < _triggers.Count; i++) // Scene戚 淫軒馬澗 Trigger級聖 穿採 for庚聖 宜焼 溌昔馬食
        {
            if (_triggers[i] == trigger) // 昔切稽 閤精 trigger研 達生檎
            {
                SpawnManager._instance.SpawnMonster(_spawnDict[i]); // 益 授辞(護 腰属)拭 硝限精 佼什斗級聖 什肉背捜.
            }
        }
        StartBattle(isLastBattle); // 陥 社発梅生艦 穿燈 獣拙
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
    void StartBattle(bool isLastBattle) // Trigger研 閤紹生艦, 穿燈 獣拙戚陥! 虞壱 穿含幻 背捜
    {
        BattleManager._instance.StartBattle(isLastBattle);
    }
}
