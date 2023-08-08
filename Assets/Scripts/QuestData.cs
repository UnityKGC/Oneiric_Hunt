using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QuestType
{
    None,
    BringObject, // ���� ��������
    FindClue, // �ܼ� ã��
    KillMonster, // ���� ��ġ
    Max,
}

[System.Serializable]
public class QuestData
{
    public int _questID; // ����Ʈ ID

    public QuestType _questType; // ����Ʈ Ÿ��

    public string _questName; // ����Ʈ �̸�

    public string _questContentText;

    public List<string> _questStartTextList; // ����Ʈ ��� ����Ʈ => ��� ����� ���� ����

    public List<string> _questEndTextList; // ����Ʈ ��� ����Ʈ => ��� ����� ���� ����

    public List<ObjectData> _objLst; // �����ؾ� �� ����Ʈ�� ������Ʈ ����Ʈ

    public QuestRewards _reward; // ����Ʈ ����

    public bool _isStart; // ����Ʈ�� �����ߴ���,

    public bool _isAchieve; // ����Ʈ �޼� ������ �����ߴ���, 

    public bool _isFinish; // ����Ʈ�� ���´���,

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
public class QuestRewards
{
    public int _gold;
    public float _exp;
}
