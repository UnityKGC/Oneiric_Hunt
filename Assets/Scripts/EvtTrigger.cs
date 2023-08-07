using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvtTrigger : MonoBehaviour
{
    private CatchPolice _police;

    private Collider _coll;

    void Start()
    {
        _coll = GetComponent<Collider>();

        _police = ChaseTriggerManager._instance.GetCatchPolice(this);
    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ChaseTriggerManager._instance.StartQTEEvent(this);

            //QTEManager._instance.StartEvent(_qteEvt); // QTE 이벤트 시작
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(_police.State == CatchPolice.CatchPoliceState.Fight)
        {
            ChaseTriggerManager._instance.StartCatchEvent(this);
            //CatchManager._instance.StartCatchEvt(_catchEvt);
            _coll.enabled = false;
        }
        else if (_police.State == CatchPolice.CatchPoliceState.Die)
            _coll.enabled = false;
    }
}
