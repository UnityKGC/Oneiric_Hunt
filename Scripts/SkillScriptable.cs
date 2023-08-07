using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillScriptable : ScriptableObject
{
    public string _skillName; // 스킬 이름

    public float _skillAmount; // 스킬 범위
    public float _damageAmount; // 스킬 데미지 범위 (SpaceCut같은 경우 끌어오는 범위와 데미지 범위가 다름)

    public float _durationTime; // 스킬 지속 시간
    public float _remainTime; // 남은 시간 => UI에서 잘 사용함
    public float _castTime; // 시전 시간 (castSpeed로 더 빨리 시전하게 할 수 있지만 나는 안함)
    public float _skillCoolTime; // 스킬 쿨 타임
    public float _damageDelay; // 지속 스킬인 경우, Delay초당 데미지를 주도록 만듬

    public float _damageValue; // 스킬 데미지 배율
    public float _damage; // 스킬 최종 데미지

    public float _moveSpd; // 스킬 이동 속도

    public float _buffDurationTime; // 버프 지속 시간

    public float _atkBuffValue; // 공격 버프 증가 비율
    public float _defBuffValue; // 방어 버프 증가 비율
    public float _movSpdBuffValue; // 이속 버프 증가 비율

    public float _atkDeBuffValue; // 공격 디버프 증가 비율
    public float _defDeBuffValue; // 방어 디버프 증가 비율
    public float _movSpdDeBuffValue; // 이속 버프 증가 비율

    public bool _isAble = true; // 스킬을 사용 가능 여부
}
