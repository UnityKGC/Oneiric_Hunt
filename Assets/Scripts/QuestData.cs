using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QuestType
{
    None = -1,
    BringObject, // ���� ��������
    FindClue, // �ܼ� ã��
    KillMonster, // ���� ��ġ
    Max,
}


[System.Serializable]
public class ObjectData
{
    public int _objID; // �����ؾ� �� objID

    public string _objName; // ������ �Ŵ��� ���� ������ _objID�� ���� ������Ʈ ������ �������� �ؾ��ϳ�

    public int _totalCount; // �����ؾ� �� ��ü ����
    public int _nowCount; // �÷��̾ ������ ���� ����

    public bool _isFull; // �÷��̾ ��� �� �ߴ���
}

[System.Serializable]
public class QuestData
{
    public int _questID; // ����Ʈ ID

    public QuestType _questType; // ����Ʈ Ÿ��

    public string _questName; // ����Ʈ �̸�

    public string _questContentText;

    public List<DialogueData> _dialogueData; // �� ����Ʈ�� ���̾�α� ���

    public List<ObjectData> _objLst; // �����ؾ� �� ����Ʈ�� ������Ʈ ����Ʈ

    public QuestRewards _reward; // ����Ʈ ����

    public bool _isStart; // ����Ʈ�� �����ߴ���,

    public bool _isAchieve; // ����Ʈ �޼� ������ �����ߴ���, 

    public bool _isFinish; // ����Ʈ�� ���´���,

}

[Flags]
public enum RewardType
{
    None = -1,
    Exp = 1 << 0,
    Gold = 1 << 1,
    Object = 1 << 2,
    Collider = 1 << 3,
}

[System.Serializable]
public class QuestRewards
{
    public RewardType _type;
    public GameObject _obj; // ����Ʈ �Ϸ� �� Ư�� ������Ʈ Ȱ��ȭ
    public Collider _coll; // ����Ʈ �Ϸ� �� �ݶ��̴� Ȱ��ȭ => �̰� �ӽ� => �÷��̾� ������ �� ���� �ʿ�
    public int _gold;
    public float _exp;
}
