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

    // 1. ChaseScene��� ����, ���ε��� ���ϰ� �ִ� Trigger�� ���� �Ŵ����� �������� ��
    // 2. �׳� Inspector���� �巡�� ������� �ٷ� �����ϱ�
    // �̰� Scene���� �����ؾ� �Ѵ�. => �ϴ� 2������ ����

    [SerializeField] List<TriggerData> _triggerDataList = new List<TriggerData>();

    [SerializeField] List<EvtTrigger> _evtTriggerList = new List<EvtTrigger>();

    Dictionary<EvtTrigger, TriggerData> _triggerDict = new Dictionary<EvtTrigger, TriggerData>();

    TriggerData _triggerData; // ���� �÷��̾ �������� _trigger�� Data

    QTEEvent _qteEvt; // ���� �������� _qteEvt
    CatchEvent _catchEvt; // ���� �������� catchEvt

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
