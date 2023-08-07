using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    SkillScriptable _scriptable;
    Collider coll;
    public void Init(SkillScriptable scriptable)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        coll = GetComponentInParent<Collider>();

        //coll.enabled = false;

        BuffManager._instance.StartMovSpdBuff(transform.parent.gameObject, _scriptable._movSpdBuffValue, _scriptable._buffDurationTime);

        StartCoroutine(StartDodgeCo());
    }
    IEnumerator StartDodgeCo()
    {
        yield return new WaitForSeconds(_scriptable._durationTime);

        //coll.enabled = true;

        Destroy(gameObject);
    }
}
