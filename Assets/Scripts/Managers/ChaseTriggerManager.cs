using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaseTriggerManager : MonoBehaviour
{
    public static ChaseTriggerManager _instance;
    [System.Serializable]
    public struct TriggerData
    {
        public CatchPolice _police;
        public QTEEvent _qteEvt;
        public CatchEvent _catchEvt;
    }

    // 1. ChaseScene들로 부터, 본인들이 지니고 있는 Trigger를 전부 매니저가 가져오는 것
    // 2. 그냥 Inspector에서 드래그 드롭으로 바로 지정하기
    // 이건 Scene마다 지정해야 한다. => 일단 2번으로 진행

    [SerializeField] List<TriggerData> _triggerDataList = new List<TriggerData>();

    [SerializeField] List<EvtTrigger> _evtTriggerList = new List<EvtTrigger>();

    Dictionary<EvtTrigger, TriggerData> _triggerDict = new Dictionary<EvtTrigger, TriggerData>();

    TriggerData _triggerData; // 현재 플레이어가 진행중인 _trigger의 Data

    QTEEvent _qteEvt; // 현재 진행중인 _qteEvt
    CatchEvent _catchEvt; // 현재 진행중인 catchEvt

    void Awake()
    {
        _instance = this;
    }
    void InitDict()
    {
        for (int i = 0; i < _evtTriggerList.Count; i++)
        {
            _triggerDict.Add(_evtTriggerList[i], _triggerDataList[i]);
        }

    }
    void Start()
    {
        InitDict();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQTEEvent(EvtTrigger trigger)
    {
        if (!_triggerDict.ContainsKey(trigger))
            return;

        _triggerData = _triggerDict[trigger];

        QTEManager._instance.StartEvent(_triggerData._qteEvt);
    }
    public void StartCatchEvent(EvtTrigger trigger)
    {
        if (!_triggerDict.ContainsKey(trigger))
            return;

        _triggerData = _triggerDict[trigger];

        CatchManager._instance.StartCatchEvt(_triggerData._catchEvt);
    }
    public CatchPolice GetCatchPolice(EvtTrigger trigger)
    {
        if (!_triggerDict.ContainsKey(trigger))
            return null;

        return _triggerDict[trigger]._police;
    }
    
    private void OnDestroy()
    {
        
    }
}
