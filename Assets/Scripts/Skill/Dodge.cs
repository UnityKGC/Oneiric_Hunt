using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    SkillScriptable _scriptable;

    float _moveSpd = 15f;
    private WaitForEndOfFrame _returnTime = new WaitForEndOfFrame();
    int _mask = 1 << 13;

    Transform _player;
    public void Init(SkillScriptable scriptable, Vector3 playerPos, Quaternion playerRot)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _player = transform.parent;

        StartCoroutine(StartDodgeCo(playerRot));
    }
    IEnumerator StartDodgeCo(Quaternion playerRot)
    {
        float time = _scriptable._durationTime;
        while(time > 0f)
        {
            
            Vector3 dir = playerRot * Vector3.forward;
            Vector3 RayDir = new Vector3(dir.x, 1.5f, dir.z);

            Debug.DrawRay(_player.position, RayDir, Color.blue);

            if (Physics.Raycast(_player.position, RayDir, _mask))
            {
                //_player.position += dir * 0 * Time.deltaTime;
            }
            else
            {
                _player.position += dir * _moveSpd * Time.deltaTime; // 부모가 플레이어니까 이렇게 하긴 했는데.. 이게 맞나...? KGC
            }
            time -= Time.deltaTime;
            yield return _returnTime;
        }

        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        SkillManager._instance.EndSkill();
    }
}
