using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : MonoBehaviour
{
    SkillScriptable _scriptable;

    float _startTime; // 시작시간
    float _remainingTime; // 남은시간

    float _duringTime; // 지속시간

    float _buffDuringTime;

    float _upAtkValue; // 올라야 할 공격스탯 배율
    float _upDefValue; // 올라야 할 공격스탯 배율
    float _upMovSpdValue; // 올라야 할 공격스탯 배율

    public void Init(SkillScriptable scriptable)
    {
        _scriptable = scriptable;

        _scriptable._isAble = false;

        _buffDuringTime = _scriptable._buffDurationTime;

        _upAtkValue = _scriptable._atkBuffValue;
        _upDefValue = _scriptable._defBuffValue;
        _upMovSpdValue = _scriptable._movSpdBuffValue;

        _duringTime = _scriptable._durationTime;
    }
    void Start()
    {
        _startTime = Time.time;

        GameObject player = GameManager._instance.Player;

        BuffManager._instance.StartAtkBuff(player, _upAtkValue, _buffDuringTime);
        BuffManager._instance.StartDefBuff(player, _upDefValue, _buffDuringTime);
        BuffManager._instance.StartMovSpdBuff(player, _upMovSpdValue, _buffDuringTime);
    }

    void Update()
    {
        _remainingTime = _duringTime - (Time.time - _startTime);
        if (_remainingTime >= 0f) // 남은 시간이 남았다면,
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
