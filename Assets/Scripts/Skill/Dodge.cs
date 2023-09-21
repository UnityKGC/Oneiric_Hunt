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

        StartCoroutine(StartDodgeCo(playerRot));
    }
    IEnumerator StartDodgeCo(Quaternion playerRot)
    {
        float time = _scriptable._durationTime;
        while(time > 0f)
        {
            Vector3 dir = playerRot * Vector3.forward;
            transform.parent.position += dir * _moveSpd * Time.deltaTime; // �θ� �÷��̾�ϱ� �̷��� �ϱ� �ߴµ�.. �̰� �³�...? KGC
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
