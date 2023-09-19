using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    SkillScriptable _scriptable;

    float _moveSpd = 15f;
    public void Init(SkillScriptable scriptable, Vector3 playerPos, Quaternion playerRot)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        //BuffManager._instance.StartMovSpdBuff(transform.parent.gameObject, _scriptable._movSpdBuffValue, _scriptable._buffDurationTime);

        SoundManager._instance.PlaySkillSound(Skills.Dodge, 0.3f, 1.0f, false, transform);

        StartCoroutine(StartDodgeCo(playerRot));
    }
    IEnumerator StartDodgeCo(Quaternion playerRot)
    {
        float time = _scriptable._durationTime;
        while(time > 0f)
        {
            Vector3 dir = playerRot * Vector3.forward;
            transform.parent.position += dir * _moveSpd * Time.deltaTime; // 부모가 플레이어니까 이렇게 하긴 했는데.. 이게 맞나...? KGC
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        SkillManager._instance.EndSkill();
    }
}
