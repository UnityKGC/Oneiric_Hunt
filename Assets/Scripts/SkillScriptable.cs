using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillScriptable : ScriptableObject
{
    public string _skillName; // ��ų �̸�

    public float _skillAmount; // ��ų ����
    public float _damageAmount; // ��ų ������ ���� (SpaceCut���� ��� ������� ������ ������ ������ �ٸ�)

    public float _durationTime; // ��ų ���� �ð�
    public float _remainTime; // ���� �ð� => UI���� �� �����
    public float _castTime; // ���� �ð� (castSpeed�� �� ���� �����ϰ� �� �� ������ ���� ����)
    public float _skillCoolTime; // ��ų �� Ÿ��
    public float _damageDelay; // ���� ��ų�� ���, Delay�ʴ� �������� �ֵ��� ����

    public float _damageValue; // ��ų ������ ����
    public float _damage; // ��ų ���� ������

    public float _moveSpd; // ��ų �̵� �ӵ�

    public float _buffDurationTime; // ���� ���� �ð�

    public float _atkBuffValue; // ���� ���� ���� ����
    public float _defBuffValue; // ��� ���� ���� ����
    public float _movSpdBuffValue; // �̼� ���� ���� ����

    public float _atkDeBuffValue; // ���� ����� ���� ����
    public float _defDeBuffValue; // ��� ����� ���� ����
    public float _movSpdDeBuffValue; // �̼� ���� ���� ����

    public bool _isAble = true; // ��ų�� ��� ���� ����
}
